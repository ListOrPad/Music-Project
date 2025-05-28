let isAudioInitialized = false;
let mediaSource = null;

mergeInto(LibraryManager.library, {
    InitializeAudioSystem: function(audioDataPtr, dataSize) {
        if (!window.isAudioInitialized) return;
        try {
            if (window.audioContext && window.audioContext.state === 'suspended') {
                window.audioContext.resume();
            }

            // Очистка предыдущих ресурсов
            if (window.mediaSource) {
                URL.revokeObjectURL(window.mediaSource);
                window.mediaSource = null;
            }
            if (window.audioElement) {
                window.audioElement.pause();
                window.audioElement.src = '';
                window.audioElement.remove();
            }

             // Получаем сырые аудиоданные из Unity
            let audioData = HEAPU8.subarray(audioDataPtr, audioDataPtr + dataSize);
            let blob = new Blob([audioData], {type: 'audio/mpeg'});
            window.mediaSource = URL.createObjectURL(blob);

            window.audioElement = new Audio(window.mediaSource);
            window.audioElement.crossOrigin = "anonymous";
            
            //connect to analyser
            let source = window.audioContext.createMediaElementSource(window.audioElement);
            source.connect(window.analyser);
        } 
        catch (error) {
            console.error("Audio init error:", error);
        }
    },

    EnableAudioSystem: function() {
        if (!window.audioContext) {
            window.audioContext = new (window.AudioContext || window.webkitAudioContext)();
            window.analyser = window.audioContext.createAnalyser();
            window.analyser.fftSize = 512;

            // Автовосстановление при тапе/клике
            document.addEventListener('click', function() {
                if (window.audioContext.state === 'suspended') {
                    window.audioContext.resume();
                }
            });
        }
        window.isAudioInitialized = true;
    },

   GetSpectrumData: function() {
        if (!window.analyser) {
            return 0;
        }

        let frequencyData = new Uint8Array(window.analyser.frequencyBinCount);
        window.analyser.getByteFrequencyData(frequencyData);
        
        let buffer = _malloc(frequencyData.length);
        HEAPU8.set(frequencyData, buffer);
        
        return buffer;
    },

    ControlAudio: function(action) {
    let cmd = UTF8ToString(action);
        try {
            switch(cmd) {
                case 'play':
                    if (window.audioElement.paused) {
                        window.audioElement.play();
                    }
                    break;
                    
                case 'pause':
                    if (!window.audioElement.paused) {
                        window.audioElement.pause();
                    }
                    break;
                    
                case 'stop':
                    window.audioElement.pause();
                    window.audioElement.currentTime = 0;
                    break;
            }
        } catch (e) {
            console.error("Audio control error:", e);
        }
    }
});