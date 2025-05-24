using UnityEngine;

public class ElementContainer : MonoBehaviour
{
    [SerializeField] private ElementInstance[] elements;
    [SerializeField] private ElementSlot[] elementSlotList;

    public void LoadLevelElements(ElementData[] elementDataList)
    {
        for (int i = 0; i < elements.Length; i++)
        {
            if (i < elementDataList.Length)
            {
                elements[i].Init(elementDataList[i]);
                elementSlotList[i].SetElementPosition(elements[i]);
                elements[i].gameObject.SetActive(true);
            }
            else
            {
                // Deactivate extra UI slots if not used in this level
                elements[i].gameObject.SetActive(false);
            }
        }
    }
}