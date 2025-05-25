using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Elements/New Element Combinations")]
public class ElementCombinations : ScriptableObject
{
    [SerializeField]  List<ElementCombo> elementCombos;

    public List<ElementCombo> ElementCombos
    {
        get => elementCombos;
        set => elementCombos = value;
    }
}
