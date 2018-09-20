using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class OptionsBehaviour : MonoBehaviour {
    [SerializeField] Toggle audioToogle;
    [SerializeField] Toggle animToogle;
    [SerializeField] Slider audioSlider;
    [SerializeField] Slider carSlider;
    Resolution[] resolutions;
    AudioSource musicSource;
    public Dropdown ResolutionD;

    void Start() {
        musicSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        carSlider.value = PlayerPrefs.GetFloat("CarAudio");
        audioSlider.value = PlayerPrefs.GetFloat("BackgroundAudio");
        switch (PlayerPrefs.GetInt("MusicEnabled")) {
            case 1: audioToogle.isOn = true; musicSource.enabled = true; break;
            case 0: audioToogle.isOn = false; musicSource.enabled = false; break;
        }
        switch (PlayerPrefs.GetInt("MenuAnimation")) {
            case 1: animToogle.isOn = true; break;
            case 0: animToogle.isOn = false; break;
        }
        if (PlayerPrefs.GetInt("ResolutionsWidth") != 0 && PlayerPrefs.GetInt("ResolutionsHeight") != 0) {
            Screen.SetResolution(PlayerPrefs.GetInt("ResolutionsWidth"), PlayerPrefs.GetInt("ResolutionsHeight"), true);
            ResolutionD.value = PlayerPrefs.GetInt("ResolutionsValue");
        }
            resolutions = Screen.resolutions;
        for (int i = 9; i < resolutions.Length; i++) {
            ResolutionD.options.Add(new Dropdown.OptionData(ResToString(resolutions[i])));
            //ResolutionD.value = i;
            ResolutionD.onValueChanged.AddListener(delegate { Screen.SetResolution(resolutions[ResolutionD.value].width, resolutions[ResolutionD.value].height, true);
            });

        }
        ResolutionD.onValueChanged.AddListener(delegate {
            DropdownValueChanged(ResolutionD);
        });
        audioSlider.onValueChanged.AddListener(delegate { AudioValueCheck(); });
        carSlider.onValueChanged.AddListener(delegate { CarValueCheck(); });
        audioToogle.onValueChanged.AddListener(delegate { SoundTooglerCheck(); });
        animToogle.onValueChanged.AddListener(delegate { AnimTooglerCheck(); });
    }
    string ResToString(Resolution res) {
            return res.width + " x " + res.height;
        }
    void DropdownValueChanged(Dropdown change) {
        PlayerPrefs.SetInt("ResolutionsWidth", resolutions[change.value].width);
        PlayerPrefs.SetInt("ResolutionsHeight", resolutions[change.value].height);
        PlayerPrefs.SetInt("ResolutionsValue", change.value);
        Screen.SetResolution(resolutions[change.value].width, resolutions[change.value].height, true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void DisableMusic() {
        PlayerPrefs.SetInt("MusicEnabled", 0);
        audioSlider.enabled = false;
        musicSource.enabled = false;
    }
    void EnableMusic() {
        PlayerPrefs.SetInt("MusicEnabled", 1);
        musicSource.enabled = true;
        audioSlider.enabled = true;
        musicSource.volume = audioSlider.value;
    }
    void DisableAnim() {
        PlayerPrefs.SetInt("MenuAnimation", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void EnableAnim() {
        PlayerPrefs.SetInt("MenuAnimation", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void AudioValueCheck() {
        musicSource.volume = audioSlider.value;
        PlayerPrefs.SetFloat("BackgroundAudio", audioSlider.value);
    }
    void CarValueCheck() {
        PlayerPrefs.SetFloat("CarAudio", carSlider.value);
    }
    void SoundTooglerCheck() {
        if (audioToogle.isOn == false)
            DisableMusic();
        else 
            EnableMusic();
    }
    void AnimTooglerCheck() {
        if (animToogle.isOn == false)
            DisableAnim();
        else
            EnableAnim();
    }
}
