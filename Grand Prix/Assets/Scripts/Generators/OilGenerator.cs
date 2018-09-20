using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilGenerator : MonoBehaviour {

    [SerializeField] private GameObject oilObject;

    private Transform playerTransform;
    private float nextSpawn = 10.0f;
    private float lastSpawn = 0.0f;
    private bool stopSpawn = false;

    /*/ ------------ OIL CONFIG ----------- /*/
    private float[] OilDelayOnLevels = { 6f, 5.5f, 5f, 4.5f }; // Oil delays on all levels

    private void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        SpawnOil();
    }
    private void Update() {
        if (LevelManager.gameIsActive) {
            if (GameObject.FindGameObjectWithTag("RoadBridge") != null)
                stopSpawn = true;
            else
                stopSpawn = false;

            OilDelayOnLevels[LevelManager.currentLevel] -= Time.deltaTime;
            if (OilDelayOnLevels[LevelManager.currentLevel] <= 0.0f && !stopSpawn && playerTransform.position.x - nextSpawn > lastSpawn) {
                SpawnOil();
                OilDelayOnLevels[LevelManager.currentLevel] = 6.0f;
            }
        }
    }
    private void SpawnOil() {
        GameObject goOil;
        goOil = Instantiate(oilObject) as GameObject;
        goOil.transform.SetParent(transform);
        lastSpawn = playerTransform.position.x + nextSpawn;
        goOil.transform.position = new Vector3(lastSpawn, 0.1f, Random.Range(RoadGenerator.defaultBounds[0], RoadGenerator.defaultBounds[1]));
    }
}
