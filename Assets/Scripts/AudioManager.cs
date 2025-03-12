using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------------- Audio Sources --------------")] 
    [SerializeField] private AudioSource backgroundSoundsSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("-------------- Audio Clips --------------")]
    public AudioClip backgroundMusic;
    public AudioClip backgroundSounds;
    public AudioClip assembleLineSound;
    public AudioClip startGameSound;
    public AudioClip gameOverSound;
    public AudioClip powerUpSound;
    public AudioClip trashHitSound;

    private float basePitch = 1f;

    private void Start()
    {
        backgroundSoundsSource.clip = backgroundSounds;
        backgroundSoundsSource.loop = true;
        backgroundSoundsSource.volume = 0.8f;
        backgroundSoundsSource.pitch = basePitch; // Set initial pitch
        backgroundSoundsSource.Play();
        Debug.Log("Background sounds played");
    }

    public void IncreaseMusicPitch()
    {
        basePitch += 0.1f; // Increase pitch by 0.1
        backgroundSoundsSource.pitch = basePitch;
        Debug.Log("Music pitch increased to: " + basePitch);
    }
}