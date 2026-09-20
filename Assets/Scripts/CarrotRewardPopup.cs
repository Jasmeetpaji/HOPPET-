using UnityEngine;
using TMPro;
using System.Collections;
public class CarrotRewardPopup : MonoBehaviour
{
    public TMP_Text rewardText;
    public float animationTime = 1.2f;
    public float moveUpAmount = 100f;
    private RectTransform rectTransform;
    private Vector2 startPosition;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
        Color color = rewardText.color;
        color.a = 0f;
        rewardText.color = color;
    }
    public void ShowReward(int amount)
    {
        StopAllCoroutines();
        rewardText.text = "+" + amount + " CARROTS!";
        rectTransform.anchoredPosition = startPosition;
        Color color = rewardText.color;
        color.a = 1f;
        rewardText.color = color;
        StartCoroutine(AnimateReward());
    }
    IEnumerator AnimateReward()
    {
        Vector2 endPosition =
            startPosition + Vector2.up * moveUpAmount;
        float timer = 0f;
        while (timer < animationTime)
        {
            timer += Time.deltaTime;
            float progress = timer / animationTime;
            rectTransform.anchoredPosition =
                Vector2.Lerp(startPosition, endPosition, progress);
            Color color = rewardText.color;
            color.a = Mathf.Lerp(1f, 0f, progress);
            rewardText.color = color;
            yield return null;
        }
        Color finalColor = rewardText.color;
        finalColor.a = 0f;
        rewardText.color = finalColor;
        rectTransform.anchoredPosition = startPosition;
    }
}