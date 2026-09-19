using UnityEngine;
using TMPro;
public class CarrotManager : MonoBehaviour
{
    public static CarrotManager instance;
    [Header("UI")]
    public TMP_Text carrotText;
    private int carrots;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        carrots = PlayerPrefs.GetInt("Carrots", 0);
        UpdateCarrotText();
    }
    public void AddCarrot()
    {
        carrots++;
        PlayerPrefs.SetInt("Carrots", carrots);
        PlayerPrefs.Save();
        UpdateCarrotText();
        Debug.Log("Carrots: " + carrots);
    }
    public int GetCarrots()
    {
        return carrots;
    }
    void UpdateCarrotText()
    {
        if (carrotText != null)
        {
            carrotText.text = "CARROTS: " + carrots;
        }
    }
}