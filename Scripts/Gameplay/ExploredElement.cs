using TMPro;
using UnityEngine;

public class ExploredElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI elementName;

    public void Init(ElementData data)
    {
        elementName.text = data.ElementName;
    }
}
