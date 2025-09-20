using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{

    public RectTransform leftCap;
    public RectTransform middleFill;
    public RectTransform rightCap;

    public float maxHealth = 100f;

    [SerializeField]
    private float currentHealth;

    private float fullMiddleWidth;
    void Start()
    {
        fullMiddleWidth = middleFill.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBar();
    }
    
     private void UpdateBar()
    {
        float percent = currentHealth / maxHealth;

        // Resize middle part (shrinks only from the right, because pivot.x = 0)
        float newWidth = fullMiddleWidth * percent;
        middleFill.sizeDelta = new Vector2(newWidth, middleFill.sizeDelta.y);

        // Move right cap to the end of the middle bar
        float leftCapWidth = leftCap.sizeDelta.x;
        rightCap.anchoredPosition = new Vector2(leftCapWidth + newWidth, rightCap.anchoredPosition.y);
    }
}
