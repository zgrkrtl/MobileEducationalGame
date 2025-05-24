using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour,IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {
    
    [SerializeField] private Canvas canvas;
    [SerializeField] private InfoDisplay infoDisplay;
    
    
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Transform originalParent;
    private Vector2 originalAnchoredPosition;
    private ElementInstance draggedElement;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        draggedElement = GetComponent<ElementInstance>();
    }
    
    
    public void OnBeginDrag(PointerEventData eventData) 
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        
        originalParent = transform.parent;
        originalAnchoredPosition = rectTransform.anchoredPosition;

        transform.SetParent(canvas.transform, true);
        infoDisplay.DisplayInfo(draggedElement.Data);
    }

    public void OnDrag(PointerEventData eventData) 
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
       
       if (transform.parent == canvas.transform)
       {
           transform.SetParent(originalParent, false);
           rectTransform.anchoredPosition = originalAnchoredPosition;
       }
       
       infoDisplay.EndDisplayInfo();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

}
