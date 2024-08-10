using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour, IDataPersistance
{
  [SerializeField] private AudioMixer myMixer;
  [SerializeField] private Slider MusicSlider;
  [SerializeField] private Slider SFXSlider;
  protected float musicVolume;
  protected float sfxVolume;

  private void Start()
  {
    if (PlayerPrefs.HasKey("MusicVolume"))
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
    musicVolume = MusicSlider.value;
    myMixer.SetFloat("Music", Mathf.Log10(musicVolume) * 20);
    PlayerPrefs.SetFloat("MusicVolume", musicVolume);
  }

  public void SetSFXVolume()
  {
    sfxVolume = SFXSlider.value;
    myMixer.SetFloat("SFX", Mathf.Log10(sfxVolume) * 20);
    PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
  }

  private void LoadVolume()
  {
    MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");

    SetMusicVolume();
    SetSFXVolume();
  }

  public void LoadData(GameData data)
  {
    this.musicVolume = data.MusicVolume;
    this.sfxVolume = data.SFXVolume;
  }

  public void SaveData(ref GameData data)
  {
    data.MusicVolume = this.musicVolume;
    data.SFXVolume = this.sfxVolume;
  }
}
