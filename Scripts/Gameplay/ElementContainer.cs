using System.Collections.Generic;
using UnityEngine;

public class ElementContainer : MonoBehaviour
{
    [SerializeField] private ElementInstance[] elements;
    [SerializeField] private ElementSlot[] elementSlotList;

    public void LoadLevelElements(List<ElementData> elementDataList)
    {
        
        for (int i = 0; i < elementDataList.Count; i++)
        {
            elements[i].Init(elementDataList[i]);
            elementSlotList[i].SetElementPosition(elements[i]);
            elements[i].gameObject.SetActive(true);
        }
    }
}