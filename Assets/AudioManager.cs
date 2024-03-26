using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource soundEffectSource;
    public AudioSource musicSource;

    private float soundVolume = 1f;
    private float musicVolume = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroy duplicate AudioManager instances
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from the sceneLoaded event
    }

    // Method called when a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Stop the current AudioManager instance if it's not the same as the instance of the new scene
        if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Implement logic to smoothly transition music for the new scene
        AudioClip musicClip = GetMusicForScene(scene.name);
        if (musicClip != null)
        {
            StartCoroutine(CrossfadeMusic(musicClip));
        }
    }

    // Coroutine for smoothly transitioning music with crossfade effect
    private IEnumerator CrossfadeMusic(AudioClip newMusicClip)
    {
        float crossfadeDuration = 1.0f; // Adjust as needed
        float timer = 0f;

        // Fade out the current music
        while (timer < crossfadeDuration)
        {
            musicSource.volume = Mathf.Lerp(musicVolume, 0f, timer / crossfadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        
        // Stop the current music
        musicSource.Stop();

        // Set up and play the new music
        musicSource.clip = newMusicClip;
        musicSource.volume = 0f; // Start with volume 0
        musicSource.loop = true; // Set loop to true
        musicSource.Play();

        // Fade in the new music
        timer = 0f;
        while (timer < crossfadeDuration)
        {
            musicSource.volume = Mathf.Lerp(0f, musicVolume, timer / crossfadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure volume is at the desired level
        musicSource.volume = musicVolume;
    }

    // Method to play sound effects
    public void PlaySound(AudioClip soundClip)
    {
        soundEffectSource.PlayOneShot(soundClip, soundVolume);
    }

    // Method to play music
    public void PlayMusic(AudioClip musicClip)
    {
        musicSource.clip = musicClip;
        musicSource.volume = musicVolume;
        musicSource.loop = true; // Set loop to true
        musicSource.Play();
    }

    // Method to set sound volume
    public void SetSoundVolume(float volume)
    {
        soundVolume = Mathf.Clamp01(volume);
    }

    // Method to set music volume
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        musicSource.volume = musicVolume;
    }

    // Method to get the appropriate music clip for a scene (customize as needed)
    private AudioClip GetMusicForScene(string sceneName)
    {
        switch (sceneName)
        {
            case "level1":
                return Resources.Load<AudioClip>("Music/level1");
            case "scifi":
                return Resources.Load<AudioClip>("Music/scifi");
            case "synthesizer":
                return Resources.Load<AudioClip>("Music/synthesizer");
            case "horsestyle":
                return Resources.Load<AudioClip>("Music/horsestyle");
            default:
                return null; // Return null if no music clip is found
        }
    }
}
