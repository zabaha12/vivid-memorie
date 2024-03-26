using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip jumpSound;
    public AudioClip runSound;
    public AudioClip kickSound;
    public AudioClip hitSound;

    private AudioSource audioSrc;
    private bool isRunning = false;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();

        // Load audio clips
        jumpSound = Resources.Load<AudioClip>("jump");
        runSound = Resources.Load<AudioClip>("run");
        kickSound = Resources.Load<AudioClip>("kick");
        hitSound = Resources.Load<AudioClip>("hit");
    }

    // Method to play jump sound
    public void PlayJumpSound()
    {
        audioSrc.PlayOneShot(jumpSound);
    }

    // Method to play run sound
    public void PlayRunSound()
    {
        if (!isRunning)
        {
            audioSrc.PlayOneShot(runSound);
            isRunning = true;
        }
    }

    // Method to stop run sound
    public void StopRunSound()
    {
        isRunning = false;
    }

    // Method to play kick sound
    public void PlayKickSound()
    {
        audioSrc.PlayOneShot(kickSound);
    }

    // Method to play hit sound
    public void PlayHitSound()
    {
        audioSrc.PlayOneShot(hitSound);
    }
}
