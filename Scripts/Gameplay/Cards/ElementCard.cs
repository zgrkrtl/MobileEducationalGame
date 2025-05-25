using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class ElementCard : MonoBehaviour
{
    [SerializeField] private GameObject frontFace;
    [SerializeField] private GameObject backFace;
    [SerializeField] private Image elementImage;
    [SerializeField] private float flipDuration = 0.5f;
    
    private bool isFlipping = false;
    private bool isFrontShowing = false;
    
    private ElementData elementData;
    private TextMeshProUGUI elementText;

    public void Init(ElementData data)
    {
        elementData = data;
        elementText = frontFace.GetComponentInChildren<TextMeshProUGUI>();
        
        elementText.text = elementData.Description;
        elementImage.sprite = data.Icon;
    }
    
    public void Flip()
    {
        if (!isFlipping)
            StartCoroutine(FlipCard());
    }

    private IEnumerator FlipCard()
    {
        isFlipping = true;

        float time = 0f;
        float halfDuration = flipDuration / 2f;
        Quaternion startRot = transform.rotation;
        Quaternion midRot = Quaternion.Euler(0f, 90f, 0f);
        Quaternion endRot = Quaternion.Euler(0f, 0f, 0f);

        while (time < halfDuration)
        {
            time += Time.deltaTime;
            float t = time / halfDuration;
            transform.rotation = Quaternion.Lerp(startRot, midRot, t);
            yield return null;
        }

        // Swap faces
        frontFace.SetActive(!isFrontShowing);
        backFace.SetActive(isFrontShowing);
        isFrontShowing = !isFrontShowing;

        time = 0f;
        while (time < halfDuration)
        {
            time += Time.deltaTime;
            float t = time / halfDuration;
            transform.rotation = Quaternion.Lerp(midRot, endRot, t);
            yield return null;
        }

        transform.rotation = endRot;
        isFlipping = false;
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject,1f);
    }
}