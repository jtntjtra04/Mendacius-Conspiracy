using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    Canvas canvas;
    private CanvasGroup canvas_group;
    private RectTransform rect_transform;
    private Image image;

    // References
    public GameObject drag_icon;

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.color = new Color32(255, 255, 255, 170);
        drag_icon.SetActive(false);
        canvas_group.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect_transform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.color = new Color32(255, 255, 255, 255);
        canvas_group.blocksRaycasts = true;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        drag_icon.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        drag_icon.SetActive(false);
    }
    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
    private void Start()
    {
        rect_transform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        canvas_group = GetComponent<CanvasGroup>();
        FrontDeskReference frontdesk_reference = FindAnyObjectByType<FrontDeskReference>();
        canvas = frontdesk_reference.front_desk;
        drag_icon.SetActive(false);
    }
}