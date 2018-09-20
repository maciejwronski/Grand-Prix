using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FinishButtonScript : MonoBehaviour {

    [SerializeField] InputField input;
    [SerializeField] GameObject gameobj;
    Highscores highscores;
	void Start () {
    }
    public void SendHighscoreAndReturnToMainMenu() {
        if (input.text == null ||  input.text.Length < 3 || input.text.Length > 15) {
            input.text = "Length of nick 3-15 letters";
            return;
        }
            GameObject go;
            go = Instantiate(gameobj) as GameObject;
            go.transform.SetParent(transform);
            highscores = go.GetComponent<Highscores>();
            highscores.AddScore(input.text, PlayerBehaviour.totalScore);
            SceneManager.LoadScene(0);
            Destroy(go);
        Destroy(highscores);
    }    
}

