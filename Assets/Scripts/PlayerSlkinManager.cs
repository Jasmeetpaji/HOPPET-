using UnityEngine;
public class PlayerSkinManager : MonoBehaviour
{
    public SpriteRenderer playerSpriteRenderer;
    public Sprite defaultSkin;
    public Sprite ninjaSkin;
    public Sprite kingSkin;
    public Sprite cyberSkin;
    void Start()
    {
        ApplyEquippedSkin();
    }
    void ApplyEquippedSkin()
    {
        string equippedSkin = PlayerPrefs.GetString("EquippedSkin", "DEFAULT");
        Debug.Log("Equipped skin: " + equippedSkin);
        if (equippedSkin == "NINJA")
        {
            playerSpriteRenderer.sprite = ninjaSkin;
        }
        else if (equippedSkin == "KING")
        {
            playerSpriteRenderer.sprite = kingSkin;
        }
        else if (equippedSkin == "CYBER")
        {
            playerSpriteRenderer.sprite = cyberSkin;
        }
        else
        {
            playerSpriteRenderer.sprite = defaultSkin;
        }
    }
}