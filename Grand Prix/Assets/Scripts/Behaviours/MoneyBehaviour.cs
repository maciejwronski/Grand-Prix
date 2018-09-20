using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBehaviour : MonoBehaviour {

    /*/ ------------ COIN CONFIG ----------- /*/
    public static int coinMultiplier = 200;

    private Transform playerTransform;
    private void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() {
        if (BonusIsOutOfPlayerRange() == true) {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Player") {
            FloatingText.ShowMessage("BONUS COIN", playerTransform.position + new Vector3(0,0,-1));
            Destroy(gameObject);
        }
    }
    private bool BonusIsOutOfPlayerRange() {
        if (transform.position.x - 30 > playerTransform.position.x || playerTransform.position.x > transform.position.x + 20)
            return true;
        else return false;
    }
}
