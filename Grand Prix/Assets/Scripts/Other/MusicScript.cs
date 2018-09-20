using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour {

    private AudioSource audioSource;
    private void Start() {
        audioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("BackgroundAudio");
        switch (PlayerPrefs.GetInt("MusicEnabled")) {
            case 1: audioSource.enabled = true; break;
            case 0: audioSource.enabled = false; break;
        }
    }
    private void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
}
