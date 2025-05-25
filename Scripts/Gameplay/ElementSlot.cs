using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ElementSlot : MonoBehaviour, IDropHandler
{
    public static event Action OnElementDropped;
    public static event Action<ElementSlot> OnElementCleared;
    
    private RectTransform rectTransform;
    public bool isEmpty = true;

    private ElementInstance currentElementInstance;
    public ElementInstance ElementInstance
    {
        get => currentElementInstance;
        set
        {
            currentElementInstance = value;

            if (currentElementInstance == null)
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
                instance.CurrentSlot.currentElementInstance = null;
            }

            instance.CurrentSlot = this;
            currentElementInstance = instance;

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
        currentElementInstance = elementInstance;
        currentElementInstance.CurrentSlot = this;

        currentElementInstance.transform.SetParent(transform);

        RectTransform instanceRect = currentElementInstance.GetComponent<RectTransform>();
        instanceRect.anchoredPosition = Vector2.zero;
        
        isEmpty = false;
    }
    
    public void ClearSlot()
    {
        if (currentElementInstance != null)
        {
            Destroy(currentElementInstance.gameObject);
        }
        ElementInstance = null;
        isEmpty = true;    }

}
