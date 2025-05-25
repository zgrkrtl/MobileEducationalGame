using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CardsManager : MonoBehaviour
{
    [SerializeField] private RectTransform topDeckLocation;
    [SerializeField] private RectTransform appendCardLocation;
    [SerializeField] private ElementCard cardPrefab;
    [SerializeField] private ExploredElement exploredCardPrefab;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject cardContainer;
    [SerializeField] private GameObject exploredCardsContainer;
    [SerializeField] private float toMiddleDuration = 1.0f;
    [SerializeField] private float middleToTopRightDuration = 1.0f;
    [SerializeField] private float durationInTheMiddle = 3.0f;
    
    [SerializeField] private ElementCombinationManager elementCombinationManager;
    [SerializeField] private LevelsManager levelsManager;

    private List<ElementData> exploredElements;

    private void OnEnable()
    {
        exploredElements = SaveManager.Load().elementsInLevel;

        foreach (ElementData element in exploredElements)
        {
            ExploredElement exploredElement = Instantiate(exploredCardPrefab, Vector3.zero, Quaternion.identity,exploredCardsContainer.transform);
            exploredElement.Init(element);
        }
    }

    public void ExploreCard(ElementData data)
    {
        // if exists do nothing
        if (exploredElements.Contains(data)) return;
        
        // else case start procedure of exploring new element
        ElementCard newCard = Instantiate(cardPrefab, topDeckLocation.position, Quaternion.identity,cardContainer.transform);
        newCard.Init(data);
        exploredElements.Add(data);
        
        var saveData = new SaveData
        {
            elementsInLevel = exploredElements
        };
        SaveManager.Save(saveData);
        
        StartCoroutine(AppendCard(newCard,data));
    }

    private IEnumerator AppendCard(ElementCard card, ElementData data)
    {
        RectTransform cardRect = card.GetComponent<RectTransform>();

        cardRect.SetParent(canvas.transform, true); 

        Vector2 centerPosition = Vector2.zero;
        float elapsed = 0f;

        Vector2 startPos = cardRect.anchoredPosition;

        while (elapsed < toMiddleDuration)
        {
            float t = elapsed / toMiddleDuration;
            cardRect.anchoredPosition = Vector2.Lerp(startPos, centerPosition, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cardRect.anchoredPosition = centerPosition;
        card.Flip();
        
        yield return new WaitForSeconds(durationInTheMiddle);
        
        elapsed = 0f;
        while (elapsed < middleToTopRightDuration)
        {
            float t = elapsed / middleToTopRightDuration;
            cardRect.anchoredPosition = Vector2.Lerp(centerPosition, appendCardLocation.position, t);
            cardRect.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        ExploredElement exploredElement = Instantiate(exploredCardPrefab, Vector3.zero, Quaternion.identity,exploredCardsContainer.transform);
        exploredElement.Init(data);
        card.DestroyGameObject();

        // end current level and go next
        
        if (elementCombinationManager.combinations.Count == exploredElements.Count)
        {
            levelsManager.currentLevel++;
            var saveData = new SaveData
            {
                level = levelsManager.currentLevel,
                elementsInLevel = new List<ElementData>()
            };
            SaveManager.Save(saveData);
            levelsManager.InitializeLevel(levelsManager.currentLevel);
            elementCombinationManager.ClearSlots();
        }
    }

    
}
