using UnityEngine;

public class PlayerAudioEffects : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip jump, jump2, gravity;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayJumpSound()
    {
        if(jump != null)
            audioSource.PlayOneShot(jump);
    }

        public void PlayJumpSoundAlt()
    {
        if(jump != null)
            audioSource.PlayOneShot(jump2);
    }

    public void PlayGravityWarpSound()
    {
        if(gravity != null);
            audioSource.PlayOneShot(gravity);
    }
}