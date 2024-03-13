using UnityEngine;
using UnityEngine.UI;

public class ResolutionAdjuster : MonoBehaviour
{
    public CanvasScaler canvasScaler;

    void Start()
    {
        AdjustCanvasScale();
    }

    void AdjustCanvasScale()
    {
        // 기준이 되는 해상도
        float targetWidth = 1920f;
        float targetHeight = 1080f;

        // 현재 해상도
        float currentWidth = Screen.width;
        float currentHeight = Screen.height;

        // 기준 해상도 대비 현재 해상도의 비율 계산
        float widthRatio = currentWidth / targetWidth;
        float heightRatio = currentHeight / targetHeight;

        // 더 작은 비율을 선택하여 CanvasScaler에 적용
        float minRatio = Mathf.Min(widthRatio, heightRatio);
        canvasScaler.scaleFactor = minRatio;
    }
}