using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuKeyManagement : MonoBehaviour {

    [SerializeField] Button[] buttons;
    int currentOption;
	void Start () {
        currentOption = 0;
    }
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
            if (currentOption >= 4)
                return;
            currentOption++;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
            if (currentOption <= 0)
                return;
            currentOption--;
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (currentOption < 4)
                SceneManager.LoadScene(currentOption + 1);
            else
                Application.Quit();
        }
        buttons[currentOption].Select();
    }
}
