using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ElementSlot : MonoBehaviour, IDropHandler
{
    public static event Action OnElementDropped;
    public static event Action<ElementSlot> OnElementCleared;
    
    private RectTransform rectTransform;
    public bool isEmpty = true;

    private ElementInstance currenteElementInstance;
    public ElementInstance ElementInstance
    {
        get => currenteElementInstance;
        set
        {
            currenteElementInstance = value;

            if (currenteElementInstance == null)
                OnElementCleared?.Invoke(this);
        }
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!isEmpty) return;
        
        if (eventData.pointerDrag != null)
        {
            ElementInstance instance = eventData.pointerDrag.GetComponent<ElementInstance>();

            if (instance.CurrentSlot != null)
            {
                instance.CurrentSlot.isEmpty = true;
                instance.CurrentSlot.currenteElementInstance = null;
            }

            instance.CurrentSlot = this;
            currenteElementInstance = instance;

            Transform droppedTransform = instance.transform;
            droppedTransform.SetParent(transform);

            RectTransform droppedRect = droppedTransform.GetComponent<RectTransform>();
            droppedRect.anchoredPosition = Vector2.zero;

            droppedTransform.SetSiblingIndex(transform.GetSiblingIndex());

            isEmpty = false;
            OnElementDropped?.Invoke();
        }
    }

    public void SetElementPosition(ElementInstance elementInstance)
    {
        currenteElementInstance = elementInstance;
        currenteElementInstance.CurrentSlot = this;

        currenteElementInstance.transform.SetParent(transform);

        RectTransform instanceRect = currenteElementInstance.GetComponent<RectTransform>();
        instanceRect.anchoredPosition = Vector2.zero;
        
        isEmpty = false;
    }
    
    public void ClearSlot()
    {
        ElementInstance = null;
    }

}
