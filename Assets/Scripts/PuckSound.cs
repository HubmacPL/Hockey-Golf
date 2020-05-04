using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckSound : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private AudioSource puckAudioSource;

    [Header("Source shoot sounds: ")]
    public AudioSource shotSoundsAudioSource;

    [Header("Clips: ")]
    public AudioClip puckSlide;
    public AudioClip puckShot;
    public AudioClip[] puckBounceSounds;

    [Header("Bouncing Sound Settings: ")]
    public float vMaxBounceVolume = 1.0f;
    public float vMinBounceVolume = 0.2f;
    public float vBouncFuncQaParametr = 1.0f;

    [Header("Shot Sound Setting: ")]
    public float vShotFuncQaParametr = 1.0f;
    public float vMaxShotVolume = 1.0f;
    public float vMinShotVolume = 0.2f;

    [Header("Slide Sound Setting: ")]
    public float vSlideFuncQaParametr = 1.0f;
    public float sMaxPitchValue = 1.2f;
    public float sMinPitchValue = 0.8f;
    public float sMaxVolumeValue = 0.8f;
    public float sMinVolumeValue = 0.7f;





    private float sourceVolumeDefault;
    private float sourcePitchDefault;

    private float shotSourceVolumeDefault;
    private float shotSourcePitchDefualt;


    private void Start()
    {
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        puckAudioSource = gameObject.GetComponent<AudioSource>();
        puckAudioSource.Stop();

        sourcePitchDefault = puckAudioSource.pitch;
        sourceVolumeDefault = puckAudioSource.volume;

        shotSourceVolumeDefault = shotSoundsAudioSource.volume;
        shotSourcePitchDefualt = shotSoundsAudioSource.pitch;
    }

    private void Update()
    {
        PlaySlideSound();
    }

    private void PlaySlideSound()
    {
        Vector2 velocity2d = new Vector2(playerRigidbody.velocity.x, playerRigidbody.velocity.z);

        if (velocity2d.magnitude >= 0.1f)
        {
            float volumeFactor = vSlideFuncQaParametr*(velocity2d.magnitude * velocity2d.magnitude);
            float pitchFactor = 0.04f * (velocity2d.magnitude * velocity2d.magnitude) + 0 * velocity2d.magnitude + sMinPitchValue;

            puckAudioSource.volume = volumeFactor;
            puckAudioSource.pitch = pitchFactor;

            puckAudioSource.volume = Mathf.Clamp(puckAudioSource.volume, 0, sMaxVolumeValue);
            Mathf.Clamp(puckAudioSource.pitch, sMinPitchValue, sMaxPitchValue);

            if (!puckAudioSource.isPlaying)
            {
                puckAudioSource.clip = puckSlide;
                puckAudioSource.Play();
            }
        }
        else
        {
            puckAudioSource.clip = null;
            puckAudioSource.Stop();
            puckAudioSource.volume = sourceVolumeDefault;
            puckAudioSource.pitch = sourcePitchDefault;
        }
    }
    public void BouncingSound()
    {
        StartCoroutine(BouncingCorutine());
    }
    public void ShotSound(float force)
    {
        StartCoroutine(ShotCorutine(force));
    }

    IEnumerator BouncingCorutine()
    {
        Vector2 velocity2D = new Vector2(playerRigidbody.velocity.x, playerRigidbody.velocity.z);

        if (velocity2D.magnitude >= 0.05f)
        {
            float volumeFactor = vBouncFuncQaParametr * (velocity2D.magnitude * velocity2D.magnitude) + 0 * velocity2D.magnitude + vMinBounceVolume;
            shotSoundsAudioSource.volume = volumeFactor;
            shotSoundsAudioSource.volume = Mathf.Clamp(shotSoundsAudioSource.volume, 0, vMaxBounceVolume);

            //Debug.Log(volumeFactor);

            int r = Random.Range(0, puckBounceSounds.Length - 1);
            shotSoundsAudioSource.PlayOneShot(puckBounceSounds[r]);
        }
        yield return new WaitForSeconds(0.1f);
        shotSoundsAudioSource.volume = shotSourceVolumeDefault;
        shotSoundsAudioSource.pitch = shotSourcePitchDefualt;
    }
    IEnumerator ShotCorutine(float force)
    {
        float volumeFactor = vShotFuncQaParametr * (force * force) + 0 * force + vMinShotVolume;
        shotSoundsAudioSource.volume = volumeFactor;
        shotSoundsAudioSource.volume = Mathf.Clamp(shotSoundsAudioSource.volume, 0, vMaxShotVolume);
        //Debug.Log(volumeFactor);

        shotSoundsAudioSource.PlayOneShot(puckShot);
        yield return new WaitForSeconds(0.1f);
        shotSoundsAudioSource.volume = shotSourceVolumeDefault;
        shotSoundsAudioSource.pitch = shotSourcePitchDefualt;
    }
}
