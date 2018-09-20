using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyCarBehaviour : MonoBehaviour {

    private float randomSpeed;
    private float[] minEnemySpeedOnLevels = { 1f, 1f, 1f, 1f };
    private float[] maxEnemySpeedOnLevels = { 20f, 22f, 24f, 26f };
    private int DirectionOfCar;
    private int roadChangeChance = 1000;
    private Transform playerTransform;
    private Rigidbody rb;
    private EnemyCollision enemyCollision;
    

    OilGenerator og;
    private void Start() {
        randomSpeed = ReturnRandomCarSpeedBetween(minEnemySpeedOnLevels[LevelManager.currentLevel], maxEnemySpeedOnLevels[LevelManager.currentLevel]);
        rb = GetComponent<Rigidbody>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyCollision = GetComponent<EnemyCollision>();
    }

    private void Update() {
        if (EnemyIsOutOfPlayerRange() == true) {
            Destroy(gameObject);
            EnemyGenerator.enemyCounter--;
        }
    }
    private void FixedUpdate() {
        UpdateSpeed();
        ChangeRoadLane();
    }
    private float ReturnRandomCarSpeedBetween(float a, float b) {
        return Random.Range(a, b);
    }
    private void UpdateSpeed() {
        rb.velocity = randomSpeed * Vector3.right;
    }
    private void ChangeRoadLane() {
        if (Random.Range(1, roadChangeChance) == 1) {
            if (rb.position.z <= 3 && rb.position.z > 1) {
                Vector3 pos = rb.transform.position;
                StartCoroutine(MoveObject(transform, pos, pos + new Vector3(4, 0, -2), 0.5f));
                DirectionOfCar = 2;
            }
            else if (rb.position.z >= -3 && rb.position.z < -1) {
                Vector3 pos = rb.transform.position;
                StartCoroutine(MoveObject(transform, pos, pos + new Vector3(4, 0, 2), 0.5f));
                DirectionOfCar = 1;
            }
            else if (rb.position.z <= 1 && rb.position.z >= -1) {
                Vector3 pos = rb.transform.position;
                if (Random.Range(0f, 1f) <= 0.5f) {
                    StartCoroutine(MoveObject(transform, pos, pos + new Vector3(4, 0, -2), 0.5f));
                    DirectionOfCar = 2;
                }
                else {
                    StartCoroutine(MoveObject(transform, pos, pos + new Vector3(4, 0, 2), 0.5f));
                    DirectionOfCar = 1;
                }
            }
        }
    }
    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time) {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f && !enemyCollision.collidingWithCar()) {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
        DirectionOfCar = 0;
    }
    private bool EnemyIsOutOfPlayerRange() {
        if (transform.position.x - 30 > playerTransform.position.x || playerTransform.position.x > transform.position.x + 20)
            return true;
        else return false;
    }
    public int GetDirectionOfCar() {
        return DirectionOfCar;
    }
    public void setDirectionOfCar(int value) {
        DirectionOfCar = value;
    }
}