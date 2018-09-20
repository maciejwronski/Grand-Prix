using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour {

    private AudioSource audioSource;
    private Rigidbody rb;
    [SerializeField] private int[] gearRatio;

    void Start () {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        setMusic();
    }
	
	// Update is called once per frame
	void Update () {
        if (PauseScript.gamePaused)
            setMusic();
        EngineSound();
    }
    /*/ Simulates engine sound /*/
    private void EngineSound() {
        int i;
        for (i = 0; i < gearRatio.Length - 1; i++) {
            if (gearRatio[i] >= rb.velocity.x) {
                break;
            }
        }
        float minValue = 0.0f;
        float maxValue = 0.0f;
        if (i == 0) {
            minValue = 0.0f;
            maxValue = gearRatio[i];
        }
        else {
            minValue = gearRatio[i - 1];
            maxValue = gearRatio[i];
        }
        float enginePitch = ((rb.velocity.x - minValue) / (maxValue - minValue)) + 1;
        audioSource.pitch = enginePitch;
    }
    /*/ Sets music on start or in menu /*/
    private void setMusic() {
        audioSource.volume = PlayerPrefs.GetFloat("CarAudio");
        switch (PlayerPrefs.GetInt("MusicEnabled")) {
            case 1: audioSource.enabled = true; break;
            case 0: audioSource.enabled = false; break;
        }
    }
}
