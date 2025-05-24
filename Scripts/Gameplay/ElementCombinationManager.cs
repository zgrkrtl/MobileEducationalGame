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

    public List<ElementCombo> combinations;

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
            Debug.LogWarning("One or both element slots are empty. Cannot combine.");
            resultElementInstance.IconImage.sprite = emptySlotSprite;
            combinationExist = false;
            return;
        }

        ElementData result = TryCombine(dataA, dataB);
        if (result != null)
        {
            Debug.Log("Result: " + result.ElementName);
            resultElementInstance.Init(result);
            resultElementSlot.ElementInstance = resultElementInstance;
            resultElementSlot.SetElementPosition(resultElementInstance);
            infoDisplay.DisplayInfo(result);
            combinationExist = true;
        }
        else
        {
            resultElementInstance.IconImage.sprite = emptySlotSprite;
            Debug.Log("No valid combination found.");
            combinationExist = false;

        }
    }

    private void OnElementCleared(ElementSlot slot)
    {
        if (slot == elementSlot1)
            dataA = null;
        if (slot == elementSlot2)
            dataB = null;

        resultElementInstance.IconImage.sprite = emptySlotSprite;
        combinationExist = false;
        resultElementSlot?.ClearSlot();
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
}
