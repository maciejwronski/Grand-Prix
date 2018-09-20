using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilBehaviour : MonoBehaviour {

    /*/ ------------ OIL CONFIG ----------- /*/
    private float[] speedReducerOnOil = { 0.8f, 0.6f, 0.4f, 0.2f }; // Speed reducer for all of the levels

    // CONSIDER YOUR VALUE AND DIVIDE IT BY 2, like if you want rotation by 360 degree which takes 1.0s, place 180.0f and 0.5f and so on.
    private float rotation = 180.0f; // Rotate player on oil by 360 degrees
    private float timeRotation = 0.5f;

    private PlayerBehaviour player;
    private Transform playerTransform;


    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update() {
        if (BonusIsOutOfPlayerRange() == true) {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider collider) {
        if(collider.tag == "Player") { 
            Destroy(gameObject);
            player.StartRotation(rotation, timeRotation);
            player.ReduceSpeed(speedReducerOnOil[LevelManager.currentLevel]);
        }
    }
    private bool BonusIsOutOfPlayerRange() {
        if (transform.position.x - 30 > playerTransform.position.x || playerTransform.position.x > transform.position.x + 20)
            return true;
        else return false;
    }
}
