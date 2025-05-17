mergeInto(LibraryManager.library, {
    InitializeAudioSystem: function(audioPathPtr) {
        const audioPath = UTF8ToString(audioPathPtr);
        try {
            if (!window.audioContext) {
                window.audioContext = new (window.AudioContext || window.webkitAudioContext)();
                
                // Динамическое создание аудиоэлемента
                window.audioElement = new Audio(audioPath);
                window.audioElement.crossOrigin = "anonymous";
                
                // Инициализация анализатора
                window.analyser = window.audioContext.createAnalyser();
                window.analyser.fftSize = 512;
                window.analyser.smoothingTimeConstant = 0.3;

                const source = window.audioContext.createMediaElementSource(window.audioElement);
                source.connect(window.analyser);
                window.analyser.connect(window.audioContext.destination);
            }
        } catch (error) {
            console.error("Audio init error:", error);
        }
    },

   GetSpectrumData: function() {
        if (!window.analyser) {
            return 0;
        }

        var frequencyData = new Uint8Array(window.analyser.frequencyBinCount);
        window.analyser.getByteFrequencyData(frequencyData);
        
        var buffer = _malloc(frequencyData.length);
        HEAPU8.set(frequencyData, buffer);
        
        return buffer;
    },

    ControlAudio: function(action) {
    const cmd = UTF8ToString(action);
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

mergeInto(LibraryManager.library, {
    InitializeAudioAnalyzer__sig: 'vi',
    InitializeAudioAnalyzer: function() {},
    
    GetSpectrumData__sig: 'i',
    GetSpectrumData: function() {},
    
    ControlAudio__sig: 'vi',
    ControlAudio: function() {}
}, {noRuntime: true});