using UnityEngine;
using UnityEngine.Audio;

public class SoundSwitcher : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup Mixer;
    [SerializeField] private GameObject imageSound1;
    [SerializeField] private GameObject imageSound2;
    [SerializeField] private GameObject imageNoise1;
    [SerializeField] private GameObject imageNoise2;

    private void Awake()
    {
        if (SoundData.sound == false)
        {
            Mixer.audioMixer.SetFloat("Music", -80);
            imageSound1.SetActive(false);
            imageSound2.SetActive(true);
        }
        else
        {
            Mixer.audioMixer.SetFloat("Music", 0);
            imageSound1.SetActive(true);
            imageSound2.SetActive(false);
        }

        if (SoundData.noises == false)
        {
            Mixer.audioMixer.SetFloat("Nises", -80);
            imageNoise1.SetActive(false);
            imageNoise2.SetActive(true);
        }
        else
        {
            Mixer.audioMixer.SetFloat("Nises", 0);
            imageNoise1.SetActive(true);
            imageNoise2.SetActive(false);
        }
    }

    public void EnableSound()
    {
        if (imageSound1.activeInHierarchy)
        {
            imageSound1.SetActive(false);
            imageSound2.SetActive(true);
            SoundData.sound = false;
        }
        else
        {
            imageSound1.SetActive(true);
            imageSound2.SetActive(false);
            SoundData.sound = true;
        }

        if (SoundData.sound == false)
        {
            Debug.Log("DADDADA");
            Mixer.audioMixer.SetFloat("Music", -80);
        }
        else
        {
            Mixer.audioMixer.SetFloat("Music", 0);
        }
    }

    public void EnableNoises()
    {
        if (imageNoise1.activeInHierarchy)
        {
            imageNoise1.SetActive(false);
            imageNoise2.SetActive(true);
            SoundData.noises = false;
        }
        else
        {
            imageNoise1.SetActive(true);
            imageNoise2.SetActive(false);
            SoundData.noises = true;
        }
        if (SoundData.noises == false)
        {
            Debug.Log("DADDADA");
            Mixer.audioMixer.SetFloat("Nises", -80);
        }
        else
        {
            Mixer.audioMixer.SetFloat("Nises", 0);
        }
    }
}
