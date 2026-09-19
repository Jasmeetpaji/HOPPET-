using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour
{
    [Header("Inventory UI")]
    public Image skinPreview;
    public TMP_Text itemName;
    public TMP_Text equipButtonText;
    [Header("Navigation Buttons")]
    public Button previousButton;
    public Button nextButton;
    [Header("Skins")]
    public Sprite defaultSkin;
    public Sprite ninjaSkin;
    public Sprite kingSkin;
    public Sprite cyberSkin;
    private Sprite[] skins;
    private string[] skinNames;
    private int currentSkinIndex = 0;
    void Start()
    {
        skins = new Sprite[]
        {
            defaultSkin,
            ninjaSkin,
            kingSkin,
            cyberSkin
        };
        skinNames = new string[]
        {
            "DEFAULT",
            "NINJA",
            "KING",
            "CYBER"
        };
        string equippedSkin = PlayerPrefs.GetString("EquippedSkin", "DEFAULT");
        currentSkinIndex = 0;
        for (int i = 0; i < skinNames.Length; i++)
        {
            if (skinNames[i] == equippedSkin.ToUpper())
            {
                currentSkinIndex = i;
                break;
            }
        }
        UpdateInventory();
    }
    public void NextSkin()
    {
        currentSkinIndex++;
        if (currentSkinIndex >= skins.Length)
        {
            currentSkinIndex = 0;
        }
        UpdateInventory();
    }
    public void PreviousSkin()
    {
        currentSkinIndex--;
        if (currentSkinIndex < 0)
        {
            currentSkinIndex = skins.Length - 1;
        }
        UpdateInventory();
    }
    public void EquipCurrentSkin()
    {
        string selectedSkin = skinNames[currentSkinIndex];
        PlayerPrefs.SetString("EquippedSkin", selectedSkin);
        PlayerPrefs.Save();
        UpdateInventory();
        Debug.Log(selectedSkin + " skin equipped!");
    }
    void UpdateInventory()
    {
        if (skinPreview != null)
        {
            skinPreview.sprite = skins[currentSkinIndex];
        }
        if (itemName != null)
        {
            itemName.text = skinNames[currentSkinIndex];
        }
        if (equipButtonText != null)
        {
            string equippedSkin =
                PlayerPrefs.GetString("EquippedSkin", "DEFAULT");
            if (equippedSkin.ToUpper() == skinNames[currentSkinIndex])
            {
                equipButtonText.text = "EQUIPPED";
            }
            else
            {
                equipButtonText.text = "EQUIP";
            }
        }
    }
}