using UnityEngine;

public class BookmarkManager : MonoBehaviour, IResettable
{
    [SerializeField] private Bookmark[] bookmarks;
    [SerializeField] private float animationSpeed = 400f; // Pixels/sec
    [SerializeField] private float visibleXPosition = 40f;
    [SerializeField] private float hiddenXPosition = -40f;

    private bool isAnimating;

    private void Start()
    {
        ResetToFirst();
    }

    /// <summary>
    /// select X1 at start
    /// </summary>
    public void ResetToFirst()
    {
        foreach (var bookmark in bookmarks)
        {
            bookmark.Selected = false;
        }

        bookmarks[0].Selected = true;

        // instantly set position(no anim)
        foreach (var bookmark in bookmarks)
        {
            Vector2 targetPos = bookmark.Selected ?
                new Vector2(visibleXPosition, bookmark.rectTransform.anchoredPosition.y) :
                new Vector2(hiddenXPosition, bookmark.rectTransform.anchoredPosition.y);

            bookmark.rectTransform.anchoredPosition = targetPos;
        }
    }

    public void OnBackButtonReset()
    {
        ResetToFirst();
    }

    private void Update()
    {
        if (!isAnimating) return;

        bool animationFinished = true;

        foreach (var bookmark in bookmarks)
        {
            // Define target pos
            float targetX = bookmark.Selected ? visibleXPosition : hiddenXPosition;
            Vector2 currentPos = bookmark.rectTransform.anchoredPosition;

            // if reached target -- skip
            if (Mathf.Approximately(currentPos.x, targetX)) continue;

            // smooth movement
            float direction = Mathf.Sign(targetX - currentPos.x);
            float newX = currentPos.x + direction * animationSpeed * Time.deltaTime;

            // check if over target pos
            if ((direction > 0 && newX > targetX) || (direction < 0 && newX < targetX))
            {
                newX = targetX;
            }

            bookmark.rectTransform.anchoredPosition = new Vector2(newX, currentPos.y);

            if (!Mathf.Approximately(newX, targetX))
            {
                animationFinished = false;
            }
        }

        if (animationFinished)
        {
            isAnimating = false;
        }
    }

    public void Select(int bookmarkID)
    {
        if (bookmarkID < 0 || bookmarkID >= bookmarks.Length) return;

        foreach (var bookmark in bookmarks)
        {
            bookmark.Selected = false;
        }

        bookmarks[bookmarkID].Selected = true;
        isAnimating = true;
    }
}
