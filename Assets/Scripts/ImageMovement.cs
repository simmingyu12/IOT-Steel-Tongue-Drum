using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageMovement : MonoBehaviour
{
    public RectTransform imageRectTransform; // 이미지의 RectTransform 참조

    void Update()
    {
        // 이미지를 새로운 위치로 이동
        Vector3 newPosition = imageRectTransform.anchoredPosition;
        newPosition.x += 1.0f; // x 방향으로 이동
        imageRectTransform.anchoredPosition = newPosition;
    }
}
