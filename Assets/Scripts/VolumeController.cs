using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{
  [SerializeField] private AudioMixer myMixer;
  [SerializeField] private Slider MusicSlider;
  [SerializeField] private Slider SFXSlider;

  private void Start()
  {
    if(PlayerPrefs.HasKey("musicVolume"))
    {
      LoadVolume();
    }
    else
    {
      SetMusicVolume();
      SetSFXVolume();
    }
    
  }

  public void SetMusicVolume()
  {
    float volume = MusicSlider.value;
    myMixer.SetFloat("music",Mathf.Log10(volume)*20);
    PlayerPrefs.SetFloat("musicVolume", volume);
  }

    public void SetSFXVolume()
  {
    float volume = SFXSlider.value;
    myMixer.SetFloat("SFX",Mathf.Log10(volume)*20);
    PlayerPrefs.SetFloat("SFXVolume", volume);
  }

  private void LoadVolume()
  {
    MusicSlider.value = PlayerPrefs.GetFloat("musicVolume");
    SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");

    SetMusicVolume();
    SetSFXVolume();
  }
}
