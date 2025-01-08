using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private List<AudioSource> audioSources; // Lista de AudioSource-uri
    [SerializeField] private Slider volumeSlider; // Slider-ul de volum
    private const string VolumePrefKey = "VolumeSetting";

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, 1.0f);

        if (PlayerPrefs.HasKey(VolumePrefKey))
        {
            Debug.Log("Has Volume");
        }
        else
        {
            Debug.Log("Does not have volume");
        }

        // Initializează slider-ul la volumul mediu al tuturor AudioSource-urilor
        if (audioSources != null && audioSources.Count > 0 && volumeSlider != null)
        {
            foreach (AudioSource source in audioSources)
            {
                source.volume = savedVolume;
            }
            volumeSlider.value = savedVolume;
        }

        // Adaugă un listener pentru slider
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat(VolumePrefKey, volume);
        PlayerPrefs.Save();

        // Actualizează volumul tuturor AudioSource-urilor din listă
        if (audioSources != null && audioSources.Count > 0)
        {
            foreach (AudioSource source in audioSources)
            {
                if (source != null)
                {
                    source.volume = volume;
                }
            }
        }
    }
}
