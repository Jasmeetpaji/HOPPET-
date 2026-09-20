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
    [Header("Original Skins")]
    public Sprite defaultSkin;
    public Sprite ninjaSkin;
    public Sprite kingSkin;
    public Sprite cyberSkin;
    [Header("Shop Skins")]
    public Sprite ds1Skin;
    public Sprite ds2Skin;
    public Sprite ds3Skin;
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
            cyberSkin,
            ds1Skin,
            ds2Skin,
            ds3Skin
        };
        skinNames = new string[]
        {
            "DEFAULT",
            "NINJA",
            "KING",
            "CYBER",
            "DS1",
            "DS2",
            "DS3"
        };
        string equippedSkin =
            PlayerPrefs.GetString("EquippedSkin", "DEFAULT");
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
        if (selectedSkin == "DS1" &&
            PlayerPrefs.GetInt("Skin1Unlocked", 0) == 0)
        {
            Debug.Log("DS1 is locked!");
            return;
        }
        if (selectedSkin == "DS2" &&
            PlayerPrefs.GetInt("Skin2Unlocked", 0) == 0)
        {
            Debug.Log("DS2 is locked!");
            return;
        }
        if (selectedSkin == "DS3" &&
            PlayerPrefs.GetInt("Skin3Unlocked", 0) == 0)
        {
            Debug.Log("DS3 is locked!");
            return;
        }
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
            string selectedSkin = skinNames[currentSkinIndex];
            bool unlocked = true;
            if (selectedSkin == "DS1")
            {
                unlocked = PlayerPrefs.GetInt("Skin1Unlocked", 0) == 1;
            }
            else if (selectedSkin == "DS2")
            {
                unlocked = PlayerPrefs.GetInt("Skin2Unlocked", 0) == 1;
            }
            else if (selectedSkin == "DS3")
            {
                unlocked = PlayerPrefs.GetInt("Skin3Unlocked", 0) == 1;
            }
            if (!unlocked)
            {
                equipButtonText.text = "LOCKED";
            }
            else
            {
                string equippedSkin =
                    PlayerPrefs.GetString("EquippedSkin", "DEFAULT");

                if (equippedSkin.ToUpper() == selectedSkin)
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
}