using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject shopPanel;
    public GameObject inventoryPanel;
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void OpenShop()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(true);
        }
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
        }
    }
    public void OpenInventory()
    {
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(true);
        }
        if (shopPanel != null)
        {
            shopPanel.SetActive(false);
        }
    }
    public void ClosePanels()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(false);
        }
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
        }
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game");

        Application.Quit();
    }
}