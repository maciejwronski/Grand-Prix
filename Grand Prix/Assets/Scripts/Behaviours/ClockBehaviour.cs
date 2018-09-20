using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockBehaviour : MonoBehaviour {

    /*/ ------------ CLOCK CONFIG ----------- /*/
    private float revertTime = 3.0f;

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
            Destroy(gameObject);
            FloatingText.ShowMessage("-" + revertTime + "seconds", playerTransform.position + new Vector3(-2, 0, -1));
            TimeCounter.seconds -= revertTime;
        }
    }
    private bool BonusIsOutOfPlayerRange() {
        if (transform.position.x - 30 > playerTransform.position.x || playerTransform.position.x > transform.position.x + 20)
            return true;
        else return false;
    }
}
