using UnityEngine;
using UnityEngine.UI;

public class ToggleKnob : MonoBehaviour
{
    public Toggle toggle;
    public RectTransform knob; // Assign the knob in Inspector
    public Vector2 offPosition = new Vector2(-38, 0);
    public Vector2 onPosition = new Vector2(38, 0);
    public float speed = 0.2f;

    void Start()
    {
        if (toggle == null)
            toggle = GetComponent<Toggle>();

        toggle.onValueChanged.AddListener(AnimateKnob);
        UpdateKnobPosition(toggle.isOn);
    }

    void AnimateKnob(bool isOn)
    {
        StopAllCoroutines();
        StartCoroutine(SmoothMove(knob.anchoredPosition, isOn ? onPosition : offPosition));
    }

    System.Collections.IEnumerator SmoothMove(Vector2 startPos, Vector2 targetPos)
    {
        float elapsed = 0;
        while (elapsed < speed)
        {
            knob.anchoredPosition = Vector2.Lerp(startPos, targetPos, elapsed / speed);
            elapsed += Time.deltaTime;
            yield return null;
        }
        knob.anchoredPosition = targetPos;
    }

    void UpdateKnobPosition(bool isOn)
    {
        knob.anchoredPosition = isOn ? onPosition : offPosition;
    }

    void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(AnimateKnob);
    }
}
