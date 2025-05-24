using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] private LevelData[] levels;
    [SerializeField] private ElementContainer elementContainer;

    public void InitializeLevel(int level)
    {
        if (level >= 0 && level < levels.Length)
        {
            LevelData currentLevelData = levels[level];
            elementContainer.LoadLevelElements(currentLevelData.elementsInLevel);
        }
        else
        {
            Debug.LogWarning("Invalid level index!");
        }
    }
}