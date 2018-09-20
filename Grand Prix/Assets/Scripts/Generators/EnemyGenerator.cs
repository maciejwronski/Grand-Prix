using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {

    public static int enemyCounter = 0;

    private Transform playerTransform;
    private Transform enemyTransform;
    [SerializeField] private GameObject car;

    /*/ ------------ ENEMY CONFIG ----------- /*/
    private float[] zAxisOfEnemies = { -3, -1, 1, 3 };
    private float spawnInFrontOfPlayer = 20.0f;
    private int[] chanceToSpawnEnemy = { 50, 40, 30, 20 };
    private int[] maxEnemiesAtSameTime = { 10, 12, 14, 16 };


    private void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

    }
    private void Update() {
        if (LevelManager.gameIsActive) {
            if (Random.Range(1, chanceToSpawnEnemy[LevelManager.currentLevel]) == 1) {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy() {
        if (enemyCounter >= maxEnemiesAtSameTime[LevelManager.currentLevel])
            return;
        GameObject goEnemy;
        Vector3 enemyPosition = new Vector3(playerTransform.position.x + spawnInFrontOfPlayer, 0.55f, zAxisOfEnemies[Random.Range(0, zAxisOfEnemies.Length)]);
        Vector3 Half = new Vector3(Random.Range(5, 15), 0, Random.Range(0.7f, 2f));
        var overlap = Physics.OverlapBox(enemyPosition, Half);
        if (overlap.Length != 0)
            return;
        goEnemy = Instantiate(car) as GameObject;
        goEnemy.transform.SetParent(transform);
        goEnemy.transform.position = enemyPosition;
        enemyCounter++;
    }
}
