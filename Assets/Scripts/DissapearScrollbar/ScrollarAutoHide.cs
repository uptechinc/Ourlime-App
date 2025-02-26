/*using UnityEngine;
using UnityEngine.UI;

public class ScrollbarAutoHide : MonoBehaviour
{
    public ScrollRect scrollRect;
    public CanvasGroup scrollbarCanvasGroup;
    private float fadeTime = 0.5f;
    private float timer;

    void Start()
    {
        if (scrollbarCanvasGroup == null)
            scrollbarCanvasGroup = GetComponent<CanvasGroup>();

        scrollbarCanvasGroup.alpha = 0; // Hide scrollbar at start
    }

    void Update()
    {
        if (Mathf.Abs(scrollRect.velocity.y) > 0.1f) // Check if scrolling
        {
            scrollbarCanvasGroup.alpha = 1; // Show scrollbar
            timer = fadeTime; // Reset timer
        }
        else if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            scrollbarCanvasGroup.alpha = Mathf.Lerp(scrollbarCanvasGroup.alpha, 0, Time.deltaTime * 5); // Fade out
        }
    }
}*/
/*using UnityEngine;
using UnityEngine.UI;

public class ScrollbarAutoHide : MonoBehaviour
{
    public ScrollRect scrollRect;
    public CanvasGroup scrollbarCanvasGroup;
    private float fadeTime = 0.5f;
    private float timer;

    void Start()
    {
        if (scrollbarCanvasGroup == null)
            scrollbarCanvasGroup = GetComponent<CanvasGroup>();

        scrollbarCanvasGroup.alpha = 1; // Ensure it's visible at start for testing
    }

    void Update()
    {
        if (scrollRect == null || scrollbarCanvasGroup == null)
        {
            Debug.LogWarning("ScrollbarAutoHide: ScrollRect or CanvasGroup is not assigned!");
            return;
        }

        float scrollSpeed = Mathf.Abs(scrollRect.velocity.y);
        Debug.Log("Scroll Speed: " + scrollSpeed);

        if (scrollSpeed > 0.1f) // Check if scrolling
        {
            scrollbarCanvasGroup.alpha = 1; // Show scrollbar
            timer = fadeTime; // Reset fade timer
        }
        else if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            scrollbarCanvasGroup.alpha = Mathf.Lerp(scrollbarCanvasGroup.alpha, 0, Time.deltaTime * 5); // Fade out smoothly
        }
    }
}*/
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarAutoHide : MonoBehaviour
{
    public ScrollRect scrollRect;
    public CanvasGroup scrollbarCanvasGroup;
    private float fadeTime = 1.5f;
    private float timer;
    private bool isScrolling;

    void Start()
    {
        if (scrollbarCanvasGroup == null)
            scrollbarCanvasGroup = GetComponent<CanvasGroup>();

        scrollbarCanvasGroup.alpha = 0; // Hide scrollbar initially
    }

    void Update()
    {
        // Detect manual scrolling (dragging) OR scroll wheel input
        if (Input.GetAxis("Mouse ScrollWheel") != 0 || Input.GetMouseButton(0))
        {
            ShowScrollbar();
        }

        if (isScrolling)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                HideScrollbar();
            }
        }
    }

    void ShowScrollbar()
    {
        isScrolling = true;
        scrollbarCanvasGroup.alpha = 1; // Show scrollbar
        timer = fadeTime; // Reset fade timer
    }

    void HideScrollbar()
    {
        isScrolling = false;
        scrollbarCanvasGroup.alpha = 0; // Hide scrollbar
    }
}


