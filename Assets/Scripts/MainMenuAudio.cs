using UnityEngine;
public class MainMenuAudio : MonoBehaviour
{
    [Header("Sound Effects")]
    public AudioSource sfxSource;
    public AudioClip buttonClickSound;
    public void PlayButtonClick()
    {
        if (sfxSource != null && buttonClickSound != null)
        {
            sfxSource.PlayOneShot(buttonClickSound);
        }
    }
}