using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Level/LevelData")]
public class LevelData : ScriptableObject
{
    public List<ElementData> elementsInLevel;
}
