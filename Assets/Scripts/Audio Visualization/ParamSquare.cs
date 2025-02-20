//using UnityEngine;

//public class ParamSquare : MonoBehaviour
//{
//    [SerializeField] private int band;
//    [SerializeField] private float startScale, scaleMultiplier;
//    [SerializeField] private bool useBuffer;

//    private void Update()
//    {
//        float targetScale = useBuffer
//            ? (AudioVisualizer.bandBuffer[band] * scaleMultiplier) + startScale
//            : (AudioVisualizer.freqBand[band] * scaleMultiplier) + startScale;

//        // Плавное изменение масштаба с учетом deltaTime
//        transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(transform.localScale.x, targetScale), Time.deltaTime * 10f); //fixed on 30f later
//    }
//}