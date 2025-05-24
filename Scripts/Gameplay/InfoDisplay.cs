using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoDisplay : MonoBehaviour
{
 [SerializeField] private TextMeshProUGUI infoText;
 [SerializeField] private Image elementImage;
 [SerializeField] private Sprite defaultImage;
 [SerializeField] private ElementCombinationManager elementCombinationManager;
 
 public void DisplayInfo(ElementData data)
 {
  elementImage.sprite = data.Icon;
  infoText.text = data.Description;
  infoText.color = Color.black;
 }

 public void EndDisplayInfo()
 {
  if (elementCombinationManager.combinationExist) return;
  
  elementImage.sprite = defaultImage;
  infoText.text = "Information goes here";
  infoText.color = Color.magenta;
 }
 
}
    