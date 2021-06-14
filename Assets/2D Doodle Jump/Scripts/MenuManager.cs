using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MenuManager : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] AudioMixerGroup musicMixer;
    AudioSource audioSoure;

    private void Awake()
    {
        LoadPlayerPrefs();
    }

    private void Start()
    {
        audioSoure = GetComponent<AudioSource>();
        audioSoure.playOnAwake = false;
        audioSoure.loop = true;
        audioSoure.outputAudioMixerGroup = musicMixer;
        PlayMusic();
    }

    public void PlayMusic()
    {
        audioSoure.clip = backgroundMusic;
        audioSoure.Play();
    }

    public void SetMusicVolume(float _value)
    {
        mixer.SetFloat("MusicVol", _value);
    }

    public void SetSFXVolume(float _value)
    {
        mixer.SetFloat("SFXVol", _value);
    }

    public void LoadPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("MusicVol"))
        {
            float musicVol = PlayerPrefs.GetFloat("MusicVol");
            musicSlider.value = musicVol;
            mixer.SetFloat("MusicVol", musicVol);
        }

        if (PlayerPrefs.HasKey("SFXVol"))
        {
            float SFXVol = PlayerPrefs.GetFloat("SFXVol");
            sfxSlider.value = SFXVol;
            mixer.SetFloat("SFXVol", SFXVol);
        }
    }

    public void SaveSFXVol()
    {
        float SFXVol;
        if (mixer.GetFloat("SFXVol", out SFXVol))
        {
            PlayerPrefs.SetFloat("SFXVol", SFXVol);
        }

        PlayerPrefs.Save();
    }

    public void SaveMusicVol()
    {
        float musicVol;
        if (mixer.GetFloat("MusicVol", out musicVol))
        {
            PlayerPrefs.SetFloat("MusicVol", musicVol);
        }

        PlayerPrefs.Save();
    }

    public void StartGame()
    {
        // Load the game scene
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        // Load the Main Menu scene
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        // Quit game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
