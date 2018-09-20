using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBehaviour : MonoBehaviour {

    public static float startCounter = 6.0f;
    [SerializeField] private Texture[] typeOfLights;
    [SerializeField] private Text startText;
    [SerializeField] private Text timerText;
    [SerializeField] private GameObject[] lights;

    GameObject[] go = new GameObject[3];

    private float changePeriod = 0.2f;
    private Transform playerTransform;

    private Renderer[] rend = new Renderer[3];


	private void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < 3; i++) {
            rend[i] = lights[i].GetComponent<Renderer>();
            rend[i].sharedMaterial.mainTexture = typeOfLights[0];
            go[i] = Instantiate(lights[i]);
        }
    }
    
    /*/ Increments Timers /*/
    private void Update () {
        if (TimeCounter.startTimer <= GameBehaviour.startCounter + 1) {
            UpdateStartTimer();
            TimeCounter.startTimer += Time.deltaTime;
            return;
        }
        if (LevelManager.gameIsActive) {
            TimeCounter.AddTime(Time.deltaTime);
            UpdateTimer();
        }
    }

    /*/ Shows the HUD before game starts /*/
    private void UpdateStartTimer() {
        if (TimeCounter.startTimer < (startCounter * 1 / 3)) {
            startText.text = "READY!";
            rend[0].sharedMaterial.mainTexture = typeOfLights[1];
        }

        else if ((TimeCounter.startTimer >= (startCounter * 1 / 3)) && TimeCounter.startTimer < (startCounter * 2 / 3)) {
            startText.text = "STEADY!";
            rend[1].sharedMaterial.mainTexture = typeOfLights[2];
        }

        else if ((TimeCounter.startTimer >= startCounter * 2 / 3) && (TimeCounter.startTimer < startCounter)) {
            startText.text = "GOOOOOO!";
            for (int i = 0; i < 2; i++)
                rend[i].sharedMaterial.mainTexture = typeOfLights[0];
            rend[2].sharedMaterial.mainTexture = typeOfLights[3];
        }
        else {
            startText.text = "";
            for (int i = 0; i < 3; i++)
                Destroy(go[i]);
        }


        if (TimeCounter.startTimer >= changePeriod) {
            startText.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
            changePeriod *= 2;
        }
  }
    /*/ Shows the HUD After game start /*/
    private void UpdateTimer() {
        timerText.text = "Level: " + (LevelManager.currentLevel+1)  + "      Score: " +  (PlayerBehaviour.totalScore + playerTransform.position.x).ToString("0") + "     Coins Collected: " + PlayerBehaviour.coinsCollected + "     Time: " + TimeCounter.GetTime();
    }
}
