using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour {

    public static float[] defaultBounds = { -3.3f, 3.3f };  // Range of default map
    public static float[] bridgeBounds = { -1.0f, 1.0f }; // Range of bridge

    public GameObject[] roadsList;
    private Transform playerTransform;

    // Tile Generator // 
    private float tileLength = 10.0f;
    private float nextSpawn = -10.0f;
    private float startWithAmountOfNormalRoads = 5;
    private float safeZone = 25.0f;
    private float lastSpawnedIndex = 0;

    // Bridge //
    private bool bridgeShouldBeGenerated = false;
    private int generateBridgeEvery = 25;
    private int tileCounter = 0;
    private int bridgeCounter = 0;
    private int lastTile;

    List<GameObject> activeRoads;

	private void Start () {
        activeRoads = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        lastTile = LastTileIndex();
        for(int i=0; i<startWithAmountOfNormalRoads; i++)
          SpawnRoads(LevelManager.currentLevel*6);
	}
	
	private void Update () {
		if(playerTransform.position.x - safeZone > (nextSpawn - startWithAmountOfNormalRoads * tileLength)) {
            SpawnRoads(generateRandomIndexOfRoad());
            DeleteRoads();
        }
	}
    private void SpawnRoads(int defaultIndex = 0) {
        GameObject goRoad;
        goRoad = Instantiate(roadsList[defaultIndex]) as GameObject;
        goRoad.transform.SetParent(transform);
        goRoad.transform.position = Vector3.right * nextSpawn;
        nextSpawn += tileLength;
        activeRoads.Add(goRoad);

        tileCounter++;
        if(tileCounter >= generateBridgeEvery) {
            bridgeShouldBeGenerated = true;
            tileCounter = 0;
        }
    }

    private void DeleteRoads() {
        Destroy(activeRoads[0]);
        activeRoads.Remove(activeRoads[0]);
    }
    private int generateRandomIndexOfRoad() {
        if (LevelManager.currentLevel < LevelManager.finishMeters.Length) {
            if (bridgeShouldBeGenerated == true && bridgeCounter != lastTile - 1) {
                bridgeShouldBeGenerated = false;
                bridgeCounter++;
                return LevelManager.currentLevel * 6 + (roadsList.Length / 4) - 2;
            }
            else if (bridgeShouldBeGenerated == true && bridgeCounter == lastTile - 1) {
                bridgeShouldBeGenerated = false;
                bridgeCounter = 0;
                return LevelManager.currentLevel * 6 + (roadsList.Length / 4) - 1;
            }
            int random = Random.Range(LevelManager.currentLevel * 6, LevelManager.currentLevel * 6 + (roadsList.Length / 4) - 2);
            while (random == lastSpawnedIndex) {
                random = Random.Range(LevelManager.currentLevel * 6, LevelManager.currentLevel * 6 + (roadsList.Length / 4) - 2);
            }
            lastSpawnedIndex = random;
            return random;
        }
        else {
            return Random.Range(0, roadsList.Length - 1);
        }
    }

    private int LastTileIndex() {
        return Mathf.FloorToInt(LevelManager.finishMeters[LevelManager.currentLevel] / (generateBridgeEvery*tileLength));
    }
}
