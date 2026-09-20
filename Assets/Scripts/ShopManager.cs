using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
public class ShopManager : MonoBehaviour
{
    [Header("Shop UI")]
    public TMP_Text carrotText;
    [Header("Buy Buttons")]
    public Button skin1BuyButton;
    public Button skin2BuyButton;
    public Button skin3BuyButton;
    [Header("Buy Button Text")]
    public TMP_Text skin1ButtonText;
    public TMP_Text skin2ButtonText;
    public TMP_Text skin3ButtonText;
    [Header("Not Enough Carrots")]
    public TMP_Text notEnoughCarrotsText;
    [Header("Prices")]
    public int skin1Price = 2000;
    public int skin2Price = 2000;
    public int skin3Price = 2000;
    private int carrots;
    private Coroutine warningCoroutine;
    void Start()
    {
        carrots = PlayerPrefs.GetInt("Carrots", 0);
        if (notEnoughCarrotsText != null)
        {
            Color color = notEnoughCarrotsText.color;
            color.a = 0f;
            notEnoughCarrotsText.color = color;
        }
        UpdateCarrotText();
        UpdateButtonTexts();
    }
    public void BuySkin1()
    {
        BuySkin("Skin1Unlocked", skin1Price);
    }
    public void BuySkin2()
    {
        BuySkin("Skin2Unlocked", skin2Price);
    }
    public void BuySkin3()
    {
        BuySkin("Skin3Unlocked", skin3Price);
    }
    void BuySkin(string unlockKey, int price)
    {
        carrots = PlayerPrefs.GetInt("Carrots", 0);
        if (PlayerPrefs.GetInt(unlockKey, 0) == 1)
        {
            Debug.Log("This skin is already unlocked!");
            UpdateButtonTexts();
            return;
        }
        if (carrots < price)
        {
            Debug.Log("Not enough carrots!");
            ShowNotEnoughCarrots();
            return;
        }
        carrots -= price;
        PlayerPrefs.SetInt("Carrots", carrots);
        PlayerPrefs.SetInt(unlockKey, 1);
        PlayerPrefs.Save();
        UpdateCarrotText();
        UpdateButtonTexts();
        Debug.Log("Skin purchased!");
    }
    void UpdateCarrotText()
    {
        if (carrotText != null)
        {
            carrotText.text = "CARROTS: " + carrots;
        }
    }
    void UpdateButtonTexts()
    {
        if (skin1ButtonText != null)
        {
            if (PlayerPrefs.GetInt("Skin1Unlocked", 0) == 1)
                skin1ButtonText.text = "OWNED";
            else
                skin1ButtonText.text = "BUY";
        }
        if (skin2ButtonText != null)
        {
            if (PlayerPrefs.GetInt("Skin2Unlocked", 0) == 1)
                skin2ButtonText.text = "OWNED";
            else
                skin2ButtonText.text = "BUY";
        }
        if (skin3ButtonText != null)
        {
            if (PlayerPrefs.GetInt("Skin3Unlocked", 0) == 1)
                skin3ButtonText.text = "OWNED";
            else
                skin3ButtonText.text = "BUY";
        }
    }
    void ShowNotEnoughCarrots()
    {
        if (notEnoughCarrotsText == null)
            return;
        if (warningCoroutine != null)
        {
            StopCoroutine(warningCoroutine);
        }
        warningCoroutine = StartCoroutine(NotEnoughCarrotsAnimation());
    }
    IEnumerator NotEnoughCarrotsAnimation()
    {
        Color color = notEnoughCarrotsText.color;
        color.a = 0f;
        notEnoughCarrotsText.color = color;
        float timer = 0f;
        while (timer < 0.25f)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, timer / 0.25f);
            notEnoughCarrotsText.color = color;
            yield return null;
        }
        color.a = 1f;
        notEnoughCarrotsText.color = color;
        yield return new WaitForSeconds(0.8f);
        timer = 0f;
        while (timer < 0.5f)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, timer / 0.5f);
            notEnoughCarrotsText.color = color;
            yield return null;
        }
        color.a = 0f;
        notEnoughCarrotsText.color = color;
        warningCoroutine = null;
    }
}