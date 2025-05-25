using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int level;
    public List<ElementData> elementsInLevel;
}