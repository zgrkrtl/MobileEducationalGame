    using System;
    using System.Collections;
    using TMPro;
    using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject gameplayMenuCanvas;
    [SerializeField] private LevelsManager levelsManager;
    [SerializeField] private GameObject warningMessage;
    [SerializeField] private float fadeDuration = 1f;
    private void Awake()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        gameplayMenuCanvas.SetActive(false);
    }

    public void ShowGameplayMenu()
    {
        mainMenuCanvas.SetActive(false);
        gameplayMenuCanvas.SetActive(true);
    }

    public void LoadLevel(int level)
    {
        if (levelsManager.IsUnlocked(level))
        {
            ShowGameplayMenu();
            levelsManager.InitializeLevel(level);
        }
        else
        {
            StartCoroutine(ShowWarningMessage());
        }
    }


private IEnumerator ShowWarningMessage()
{
    warningMessage.SetActive(true);

    Image image = warningMessage.GetComponent<Image>();
    TextMeshProUGUI text = warningMessage.GetComponentInChildren<TextMeshProUGUI>();

    Color imageColor = image.color;
    Color textColor = text.color;

    fadeDuration = 1f;
    float elapsed = 0f;

    while (elapsed < fadeDuration)
    {
        elapsed += Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);

        image.color = new Color(imageColor.r, imageColor.g, imageColor.b, alpha);
        text.color = new Color(textColor.r, textColor.g, textColor.b, alpha);

        yield return null;
    }

    image.color = new Color(imageColor.r, imageColor.g, imageColor.b, 0f);
    text.color = new Color(textColor.r, textColor.g, textColor.b, 0f);

    warningMessage.SetActive(false);
}


}
