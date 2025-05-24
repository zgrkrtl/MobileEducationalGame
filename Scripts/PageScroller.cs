using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PageScroller : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private int totalPages = 3;

    private int currentPageIndex = 0;
    private bool isDragging = false;

    private void Awake()
    {
        currentPageIndex = 0;
        ScrollToPageSmooth(currentPageIndex);
    }

    public void ScrollNext()
    {
        if (currentPageIndex < totalPages - 1)
        {
            currentPageIndex++;
            ScrollToPageSmooth(currentPageIndex);
        }
    }

    public void ScrollPrevious()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            ScrollToPageSmooth(currentPageIndex);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        SnapToNearestPage();
    }

    private void SnapToNearestPage()
    {
        float pos = scrollRect.horizontalNormalizedPosition;
        int nearestPage = Mathf.RoundToInt(pos * (totalPages - 1));
        nearestPage = Mathf.Clamp(nearestPage, 0, totalPages - 1);

        if (nearestPage != currentPageIndex)
            currentPageIndex = nearestPage;

        ScrollToPageSmooth(currentPageIndex);
    }

    private void ScrollToPageSmooth(int pageIndex)
    {
        float targetPos = (float)pageIndex / (totalPages - 1);
        StopAllCoroutines();
        StartCoroutine(SmoothScroll(targetPos));
    }

    private IEnumerator SmoothScroll(float target)
    {
        float duration = 0.25f;
        float elapsed = 0f;
        float start = scrollRect.horizontalNormalizedPosition;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / duration);
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(start, target, t);
            yield return null;
        }

        scrollRect.horizontalNormalizedPosition = target;
    }
}
