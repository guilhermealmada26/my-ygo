using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource soundEffects;
    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] bool disableBckMusic;

    public static SoundManager Instance { private set; get; }

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySfx(AudioClip clip)
    {
        if (clip != null)
            soundEffects.PlayOneShot(clip);
    }

    public void PlayBckMusic(AudioClip clip)
    {
        backgroundMusic.clip = clip;
        PlayBckMusic();
    }

    public void PlayBckMusic()
    {
        if (disableBckMusic)
            return;
        backgroundMusic.Play();
    }

    public void StopBckMusic()
    {
        backgroundMusic.Pause();
    }
}