using UnityEngine;
using UnityEngine.Audio;

public class SoundStart : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup Mixer;
    private void Start()
    {
        Debug.Log(SoundData.sound);
        Debug.Log(SoundData.noises);
        if (SoundData.sound == false)
        {
            Mixer.audioMixer.SetFloat("Music", -80);
        }
        else
        {
            Mixer.audioMixer.SetFloat("Music", 0);
        }

        if (SoundData.noises == false)
        {
            Mixer.audioMixer.SetFloat("Nises", -80);
        }
        else
        {
            Mixer.audioMixer.SetFloat("Nises", 0);
        }
    }
}
