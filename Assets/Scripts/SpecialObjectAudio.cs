using UnityEngine;
public class SpecialObjectAudio : MonoBehaviour
{
    public static SpecialObjectAudio instance;
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip specialObjectSound;
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
    public void PlaySpecialObjectSound()
    {
        if (audioSource == null)
        {
            Debug.LogError("SpecialObjectAudio: Audio Source is not assigned!");
            return;
        }
        if (specialObjectSound == null)
        {
            Debug.LogError("SpecialObjectAudio: Special Object Sound is not assigned!");
            return;
        }
        audioSource.PlayOneShot(specialObjectSound);
    }
}