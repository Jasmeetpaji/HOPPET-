using UnityEngine;
public class GameButtonAudio : MonoBehaviour
{
    public static GameButtonAudio instance;
    [Header("Button Sound")]
    public AudioSource buttonAudioSource;
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
    }
    public void PlayButtonClick()
    {
        if (buttonAudioSource != null)
        {
            buttonAudioSource.PlayOneShot(
                buttonAudioSource.clip
            );
        }
    }
}