using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public static int currentLevel = 0;
    public static float[] finishMeters = { 1000f, 2000f, 3000f, 4000f }; // Finishmeters for all of the levels
    public static bool gameIsActive = true;

    public float restartDelay = 3.0f; // Time, which player has to wait until next level
    private float[] penaltyMultiplierForTime = { 4f, 3f, 2f, 1.5f }; // Penalty on all levels for time

    private Transform playerTransform;
    private PlayerBehaviour playerBehaviour;

    [SerializeField] private GameObject levelComplete;
    [SerializeField] private GameObject gameComplete;
    [SerializeField] private Text playerScore;
    [SerializeField] private Text finishText;

    private void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        TimeCounter.TimeReset();
        gameIsActive = true;
    }

    /*/ Sets game to inactive, while level completed, sets new player score and shows game result /*/
    private void Update () {
        if (gameIsActive) {
            if (playerTransform.position.x > finishMeters[currentLevel]) {
                CompleteLevel();
            }
        }
	}
    /*/ Sets game to inactive, while level completed, sets new player score and shows game result /*/
    private void CompleteLevel() {
        gameIsActive = false;
        playerBehaviour.AddToPlayerScore(finishMeters[currentLevel]);
        playerBehaviour.SubtractPlayerScore((TimeCounter.TotalTime() * penaltyMultiplierForTime[currentLevel]));
        currentLevel++;
        if (currentLevel < finishMeters.Length) {
            levelComplete.SetActive(true);
            if (PlayerBehaviour.coinsCollected == 0)
                playerScore.text = "Current Score: " + PlayerBehaviour.totalScore.ToString();
            else {
                int bonus = MoneyBehaviour.coinMultiplier * PlayerBehaviour.coinsCollected;
                playerScore.text = "Current Score: " + PlayerBehaviour.totalScore.ToString() + "\n      Bonus for coins: " + bonus + "   Total: " + (PlayerBehaviour.totalScore + bonus).ToString();
                playerBehaviour.AddToPlayerScore(bonus);
            }
            Invoke("Restart", 5.0f);
        }
        else {
            gameComplete.SetActive(true);
            int bonus = MoneyBehaviour.coinMultiplier * PlayerBehaviour.coinsCollected;
            finishText.text = "Final Score: " + PlayerBehaviour.totalScore.ToString() + "\n      Bonus for coins: " + bonus + "   Total: " + (PlayerBehaviour.totalScore + bonus).ToString();
            playerBehaviour.AddToPlayerScore(bonus);
        }
    }
    /*/ Restarts map /*/
    void Restart() {
        if (currentLevel < finishMeters.Length) {
            PlayerBehaviour.coinsCollected = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            gameComplete.SetActive(false);
        }
    }
}
