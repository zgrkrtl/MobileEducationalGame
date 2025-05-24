using UnityEngine;
using System.Collections;

public class ElementCard : MonoBehaviour
{
    [SerializeField] private GameObject frontFace;
    [SerializeField] private GameObject backFace;
    [SerializeField] private float flipDuration = 0.5f;

    private bool isFlipping = false;
    private bool isFrontShowing = false;

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

        // First half: rotate to 90° (invisible)
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

        // Second half: rotate from 90° to 0°
        time = 0f;
        while (time < halfDuration)
        {
            time += Time.deltaTime;
            float t = time / halfDuration;
            transform.rotation = Quaternion.Lerp(midRot, endRot, t);
            yield return null;
        }

        // Ensure exact final rotation
        transform.rotation = endRot;
        isFlipping = false;
    }
}