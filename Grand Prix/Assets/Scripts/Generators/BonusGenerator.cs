using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusGenerator : MonoBehaviour {

    [SerializeField] private GameObject[] bonusObjects;
    private Transform playerTransform;
    private float nextSpawn = 10.0f;
    private float lastSpawn = 0.0f;
    private bool stopSpawn = false;
    private int chanceToSpawnBonus = 200;
    private void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update() {
        if (LevelManager.gameIsActive) {
            if (GameObject.FindGameObjectWithTag("RoadBridge") != null)
                stopSpawn = true;
            else
                stopSpawn = false;

           if (Random.Range(1, chanceToSpawnBonus) == 1 && !stopSpawn && playerTransform.position.x - nextSpawn > lastSpawn) {
                SpawnBonus();
            }
        }
    }

    /*/ Spawn bonus as a random bonus object /*/
    private void SpawnBonus() {
        GameObject goBonus;
        int randomBonusIndex = returnRandomIndexOfBonus();
        goBonus = Instantiate(bonusObjects[randomBonusIndex]) as GameObject;
        goBonus.transform.SetParent(transform);
        lastSpawn = playerTransform.position.x + nextSpawn;
        goBonus.transform.position = new Vector3(lastSpawn, 0.55f, Random.Range(RoadGenerator.defaultBounds[0], RoadGenerator.defaultBounds[1]));
    }
    private int returnRandomIndexOfBonus() {
        return Random.Range(0, bonusObjects.Length);
    }
}
