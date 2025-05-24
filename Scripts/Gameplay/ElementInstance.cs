using System;
using UnityEngine;
using UnityEngine.UI;

public class ElementInstance : MonoBehaviour
{
    [SerializeField] private ElementData data;
    [SerializeField] private Image iconImage;
    public ElementSlot CurrentSlot { get; set; } 
    public ElementData Data => data;
    public Image IconImage => iconImage;
    private void Awake()
    {
        iconImage = GetComponent<Image>();
    }
    
    public void Init(ElementData elementData) {
        data = elementData;
        iconImage.sprite = data.Icon;
    }

}
