using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class PauseScript : MonoBehaviour {

    public static bool gamePaused = false;
    [SerializeField] private GameObject pauseMenuUi;
    [SerializeField] private Button[] buttons;
    int currentOption;


    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
                if (gamePaused) {
                    Resume();
                }
                else {
                    Pause();
                }
        }
        if (gamePaused) {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
                if (currentOption >= 2)
                    return;
                currentOption++;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
                if (currentOption <= 0)
                    return;
                currentOption--;
            }
            if (Input.GetKeyDown(KeyCode.Return)) {
                if (currentOption == 0)
                    Resume();
                if (currentOption == 1)
                    LoadMenu();
                if (currentOption == 2)
                    Application.Quit();
            }
          if(buttons.Length > 0)
                buttons[currentOption].Select();
        }
	}
    private void Start() {
    }
    void Pause() {
        currentOption = 0;
        pauseMenuUi.SetActive(true);
        gamePaused = true;
        Time.timeScale = 0f;
    }
    public void Resume() {
        pauseMenuUi.SetActive(false);
        gamePaused = false;
        Time.timeScale = 1f;

    }
    public void LoadMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void QuitGame() {
        Application.Quit();
    }
}
