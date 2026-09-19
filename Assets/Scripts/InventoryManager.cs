using UnityEngine;
using TMPro;
public class InventoryManager : MonoBehaviour
{
    [Header("Inventory UI")]
    public GameObject itemSlot;
    public TMP_Text itemName;
    public TMP_Text equipButtonText;
    private bool defaultSkinEquipped = true;
    void Start()
    {
        UpdateInventory();
    }
    public void EquipDefaultSkin()
    {
        defaultSkinEquipped = true;

        UpdateInventory();

        Debug.Log("Default skin equipped!");
    }
    void UpdateInventory()
    {
        if (equipButtonText != null)
        {
            if (defaultSkinEquipped)
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