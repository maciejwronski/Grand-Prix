using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBehaviour : MonoBehaviour {

    /*/ ------------ FLOWER CONFIG ----------- /*/
    private float flowerTime = 10.0f;

    private PlayerBehaviour player;
    private Transform playerTransform;
    private BonusTextScript bonusTextScript;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        bonusTextScript = GameObject.FindGameObjectWithTag("GameCanvas").GetComponent<BonusTextScript>();
    }
    void Update() {
        if (BonusIsOutOfPlayerRange() == true) {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Player") {
            player.FlowerBonus(flowerTime);
            bonusTextScript.AddBonusText("Flower", flowerTime);
            Destroy(gameObject);
        }
    }
    private bool BonusIsOutOfPlayerRange() {
        if (transform.position.x - 30 > playerTransform.position.x || playerTransform.position.x > transform.position.x + 20)
            return true;
        else return false;
    }
}
