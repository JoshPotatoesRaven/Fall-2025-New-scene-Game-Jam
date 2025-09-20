using UnityEngine;
using UnityEngine.UI;

public class HealthBar3Piece : MonoBehaviour
{
    public RectTransform leftCap;
    public RectTransform middleFill;
    public RectTransform rightCap;

    [Header("Health")]
    public float maxHealth = 100f;
    [Range(0f, 100f)] public float currentHealth = 100f;

    private float fullMiddleWidth;
    private float baseMiddleX;
    private float baseRightX;

    void Start()
    {
        // Cache the full width at 100% health
        fullMiddleWidth = middleFill.rect.width;

        // Store the starting local positions (assuming bar is full in Editor)
        baseMiddleX = middleFill.anchoredPosition.x;
        baseRightX  = rightCap.anchoredPosition.x;

        // Ensure pivots/anchors are left-based
        ForceLeftPivot(leftCap);
        ForceLeftPivot(middleFill);
        ForceLeftPivot(rightCap);
    }

    void Update()
    {
        UpdateBar();
    }

    void ForceLeftPivot(RectTransform rt)
    {
        rt.pivot = new Vector2(0f, rt.pivot.y);
        rt.anchorMin = new Vector2(0f, rt.anchorMin.y);
        rt.anchorMax = new Vector2(0f, rt.anchorMax.y);
    }

    void SetWidth(RectTransform rt, float width)
    {
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Max(0f, width));
    }

    void UpdateBar()
    {
        float pct = maxHealth > 0f ? currentHealth / maxHealth : 0f;

        // Resize middle
        float newMidW = fullMiddleWidth * pct;
        SetWidth(middleFill, newMidW);

        // Keep middle in its original spot
        middleFill.anchoredPosition = new Vector2(baseMiddleX, middleFill.anchoredPosition.y);

        // Right cap slides in as health decreases
        float shift = fullMiddleWidth - newMidW;
        rightCap.anchoredPosition = new Vector2(baseRightX - shift, rightCap.anchoredPosition.y);
    }
}
