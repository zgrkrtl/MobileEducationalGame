using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] private Level[] levels;
    [SerializeField] private ElementContainer elementContainer;
    [SerializeField] private List<ElementCombinations> elementCombinationsList;
    [SerializeField] private ElementCombinationManager elementCombinationManager;
    public int currentLevel;
    
    private SaveData saveData;
    private void Start()
    {
        saveData = SaveManager.Load();
        InitializeLevelData(saveData.level);
    }

    public void InitializeLevel(int level)
    {
        List<ElementCombo> combinations = elementCombinationsList[level].ElementCombos;
        elementCombinationManager.Combinations = combinations;
        
        LevelData currentLevelData = levels[level].GetLevelData();
        elementContainer.LoadLevelElements(currentLevelData.elementsInLevel);
    }

    public bool IsUnlocked(int level)
    {
        return levels[level].isUnlocked;
    }

    public void InitializeLevelData(int latestUnlockedLevel)
    {
        currentLevel = latestUnlockedLevel;
        for (int i = 0; i < latestUnlockedLevel+1; i++)
        {
            levels[i].isUnlocked = true;
            levels[i].SetLockIcon(false);
        }
    }
}