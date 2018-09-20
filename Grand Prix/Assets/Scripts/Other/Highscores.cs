using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Highscores : MonoBehaviour {

    /*/ Sets limit of the highscore /*/
    private int highscoresLimit = 10;
    [SerializeField] private Text textBox;
    void Start() {
        ReadScores();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(0);
        }
    }
    public void ReadScores() {
        if (textBox.text == null)
            return;

        for(int i=0; i<highscoresLimit; i++) {
            if(PlayerPrefs.GetInt(i + "HScore") != 0)
            textBox.text += (i+1) + "." +  PlayerPrefs.GetString(i + "HScoreName") + "  -   " +PlayerPrefs.GetInt(i + "HScore") + "\n";
        }
    }
    /*/ Adds new highscore to table /*/
    public void AddScore(string nick, int score) {
        int newScore;
        string newName;
        int oldScore;
        string oldName;
        newScore = score;
        newName = nick;
        for (int i = 0; i < highscoresLimit; i++) {
            if (PlayerPrefs.HasKey(i + "HScore")) {
                if (PlayerPrefs.GetInt(i + "HScore") < newScore) {
                    oldScore = PlayerPrefs.GetInt(i + "HScore");
                    oldName = PlayerPrefs.GetString(i + "HScoreName");
                    PlayerPrefs.SetInt(i + "HScore", newScore);
                    PlayerPrefs.SetString(i + "HScoreName", newName);
                    newScore = oldScore;
                    newName = oldName;
                }
            }
            else {
                PlayerPrefs.SetInt(i + "HScore", newScore);
                PlayerPrefs.SetString(i + "HScoreName", newName);
                newScore = 0;
                newName = "";
            }
        }
    }
}
