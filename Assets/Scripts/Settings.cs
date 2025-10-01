using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEditor;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioSettings _music;
    [SerializeField] private AudioSettings _sounds;

    void Start()
    {
        Load();
    }

    public void MusicChange()
    {
        _music.mixer.SetFloat("MusicVolume", Mathf.Log10(_music.slider.value) * 20);
    }

    public void SoundsChange()
    {
        _sounds.mixer.SetFloat("SoundsVolume", Mathf.Log10(_sounds.slider.value) * 20);
    }

    public void MusicOnOff()
    {
        if (_music.toggle.isOn)
            _music.mixer.SetFloat("MusicVolume", Mathf.Log10 (_music.slider.value) * 20);
        else
            _music.mixer.SetFloat("MusicVolume", -80);
    }

    public void SoundsOnOff()
    {
        if (_sounds.toggle.isOn)
            _sounds.mixer.SetFloat("SoundsVolume", Mathf.Log10 (_sounds.slider.value) * 20);
        else
            _sounds.mixer.SetFloat("SoundsVolume", -80);
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("MusicVolume", _music.slider.value);
        PlayerPrefs.SetFloat("SoundsVolume", _sounds.slider.value);
        PlayerPrefs.SetInt("MusicToggle", Convert.ToInt16(_music.toggle.isOn));
        PlayerPrefs.SetInt("SoundsToggle", Convert.ToInt16(_sounds.toggle.isOn));
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
            _music.slider.value = PlayerPrefs.GetFloat("MusicVolume");
        if (PlayerPrefs.HasKey("SoundsVolume"))
            _sounds.slider.value = PlayerPrefs.GetFloat("SoundsVolume");
        if (PlayerPrefs.HasKey("MusicToggle"))
            _music.toggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("MusicToggle"));
        if (PlayerPrefs.HasKey("SoundsToggle"))
            _sounds.toggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("SoundsToggle"));
    }
}
[Serializable]
public struct AudioSettings
{
    public Slider slider;
    public Toggle toggle;
    public AudioMixer mixer;
}
