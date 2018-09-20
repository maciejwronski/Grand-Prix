using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    PlayerBehaviour playerBehaviour;
    private void Start() {
        playerBehaviour = GetComponent<PlayerBehaviour>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Money") {
            playerBehaviour.AddCoin();
        }
    }
}
