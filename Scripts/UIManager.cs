    using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject gameplayMenuCanvas;
    [SerializeField] private LevelsManager levelsManager;
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
        ShowGameplayMenu();
        levelsManager.InitializeLevel(level);
    }
}
