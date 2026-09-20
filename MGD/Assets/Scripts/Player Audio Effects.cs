using UnityEngine;

public class PlayerAudioEffects : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip jump, gravity;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayJumpSound()
    {
        if(jump != null)
            audioSource.PlayOneShot(jump);
    }

    public void PlayGravityWarpSound()
    {
        if(gravity != null);
            audioSource.PlayOneShot(gravity);
    }
}