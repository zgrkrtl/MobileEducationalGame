using System;
using TMPro;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject lockIcon;
    [SerializeField] private TextMeshProUGUI levelNumber;
    [SerializeField] private LevelData levelData;
    [SerializeField] public int level;
    [SerializeField] public bool isUnlocked;
    
    private void OnEnable()
    {
        isUnlocked = false;
        
        if (level == 0) isUnlocked = true;
    
        levelNumber.text = (level + 1).ToString();
        lockIcon.SetActive(!isUnlocked);
    }

    public LevelData GetLevelData()
    {
        return levelData;
    }

    public void SetLockIcon(bool value)
    {
        lockIcon.SetActive(value);
    }
}
