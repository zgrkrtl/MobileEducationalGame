using UnityEngine;

[CreateAssetMenu(menuName = "Elements/New Element")]
public class ElementData : ScriptableObject
{
  [SerializeField] private string elementName;
  [SerializeField] private Sprite icon;
  [SerializeField] private string description;
  public Sprite Icon => icon;
  public string Description => description;
  public string ElementName => elementName;
}
