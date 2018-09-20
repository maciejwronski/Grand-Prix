using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuBehaviour : MonoBehaviour {

    private void Start() {
        LevelManager.currentLevel = 0;
        PlayerBehaviour.totalScore = 0;
        PlayerBehaviour.coinsCollected = 0;
        PauseScript.gamePaused = false;
        TimeCounter.TimeReset();
    }
    public void StartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ShowHighscores() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    public void ShowInstructions() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }
    public void ShowOptions() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }
    public void ExitGame() {
        Application.Quit();
    }
    public void ReturnToMainMenu() {
             SceneManager.LoadScene(0);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0)) {
                Application.Quit();
                return;
            }
            SceneManager.LoadScene(0);
            return;
        }
    }
}
