using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ElementCombo {
    public ElementData elementA;
    public ElementData elementB;
    public ElementData result;
}

public class ElementCombinationManager : MonoBehaviour
{
    [SerializeField] private ElementSlot elementSlot1;
    [SerializeField] private ElementSlot elementSlot2;
    [SerializeField] private ElementSlot resultElementSlot;
    [SerializeField] private ElementInstance resultElementInstance;
    [SerializeField] private Sprite emptySlotSprite;
    [SerializeField] private InfoDisplay infoDisplay;
    [SerializeField] private CardsManager cardsManager;
    
    
    public List<ElementCombo> combinations;

    public List<ElementCombo> Combinations
    {
        get => combinations;
        set => combinations = value;
    }

    private ElementData dataA;
    private ElementData dataB;
    public bool combinationExist = false;

    private void OnEnable()
    {
        ElementSlot.OnElementDropped += OnElementDropped;
        ElementSlot.OnElementCleared += OnElementCleared;
    }
    

    private void OnDisable()
    {
        ElementSlot.OnElementDropped -= OnElementDropped;
        ElementSlot.OnElementCleared -= OnElementCleared;

    }
    
    private void OnElementDropped()
    {
        dataA = elementSlot1?.ElementInstance?.Data;
        dataB = elementSlot2?.ElementInstance?.Data;

        if (dataA == null || dataB == null)
        {
            resultElementInstance.IconImage.sprite = emptySlotSprite;
            combinationExist = false;
            return;
        }

        ElementData result = TryCombine(dataA, dataB);
        if (result != null)
        {
            resultElementInstance.Init(result);
            resultElementSlot.ElementInstance = resultElementInstance;
            resultElementSlot.SetElementPosition(resultElementInstance);
            infoDisplay.DisplayInfo(result);
            combinationExist = true;
            cardsManager.ExploreCard(result);
            
        }
        else
        {
            resultElementInstance.IconImage.sprite = emptySlotSprite;
            combinationExist = false;
        }
    }

    private void OnElementCleared(ElementSlot slot)
    {
        if (slot == elementSlot1)
            dataA = null;
        if (slot == elementSlot2)
            dataB = null;

        slot.isEmpty = true;
        resultElementInstance.IconImage.sprite = emptySlotSprite;
        combinationExist = false;
    }

    public ElementData TryCombine(ElementData a, ElementData b)
    {
        if (a == null || b == null)
        {
            resultElementInstance.IconImage.sprite = emptySlotSprite;
            return null;
        }

        ElementCombo combo = combinations.FirstOrDefault(c =>
            (c.elementA == a && c.elementB == b) ||
            (c.elementA == b && c.elementB == a));

        return combo?.result;
    }

    public void ClearSlots()
    {
        elementSlot1.ClearSlot();
        elementSlot2.ClearSlot();
    }
}
