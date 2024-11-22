using UnityEngine;

public class BookmarkManager : MonoBehaviour
{
    [SerializeField] private Bookmark[] bookmarks;

    private bool bookmarkChanged;
    private int updateCycles = 0;
    private Vector3 extensionSpeed = new Vector3(5, 0);

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
        bookmarkChanged = true;
    }

    private void Update()
    {
        if (bookmarkChanged)
        {
            MoveBookmark();
            updateCycles++;
        }
        if (updateCycles == 12)
        {
            bookmarkChanged = false;
            updateCycles = 0;
        }
    }

    public void Select(int bookmarkID)
    {
        foreach (var bookmark in bookmarks)
        {
            bookmark.Selected = false;
        }

        bookmarks[bookmarkID].Selected = true;
        bookmarkChanged = true;
    }


    /// <summary>
    ///  move logic for bookmarks on select and deselect
    /// </summary>
    private void MoveBookmark()
    {
        foreach (var bookmark in bookmarks)
        {
            if (bookmark.Selected)
            {
                if (bookmark.Transform.position.x < 75)
                    bookmark.Transform.position += extensionSpeed;
            }
            else
            {
                if (bookmark.Transform.position.x > 15)
                {
                    bookmark.Transform.position -= extensionSpeed;
                }
            }
        }
    }
}
