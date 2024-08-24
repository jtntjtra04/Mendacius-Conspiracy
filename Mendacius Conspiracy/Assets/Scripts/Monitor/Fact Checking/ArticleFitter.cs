using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArticleFitter : MonoBehaviour
{
    public Image image;

    void Start()
    {
        AdjustRectTransform();
    }

    void AdjustRectTransform()
    {
        if (image != null && image.sprite != null)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            // Get the original size of the image
            float imageWidth = image.sprite.rect.width;
            float imageHeight = image.sprite.rect.height;

            // Calculate aspect ratio
            float aspectRatio = imageWidth / imageHeight;

            // Get current width or height and adjust the other dimension to match the aspect ratio
            float rectWidth = rectTransform.rect.width;
            float rectHeight = rectWidth / aspectRatio;

            // Set the new size for RectTransform
            rectTransform.sizeDelta = new Vector2(rectWidth, rectHeight);
        }
    }
    void Update()
    {
        AdjustRectTransform();
    }
}
