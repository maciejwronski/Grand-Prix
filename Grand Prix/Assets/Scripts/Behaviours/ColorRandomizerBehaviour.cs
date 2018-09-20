using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRandomizerBehaviour : MonoBehaviour {
    /*/ ------------ COLOR RANDOMIZER CONFIG ----------- /*/
    private float RandomizerTime = 10.0f;

    private Transform playerTransform;
    private PlayerBehaviour player;
    private BonusTextScript bonusTextScript;

    private void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        bonusTextScript = GameObject.FindGameObjectWithTag("GameCanvas").GetComponent<BonusTextScript>();
    }

    void Update() {
        if (BonusIsOutOfPlayerRange() == true) {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Player") {
            player.StartColour((RandomizerTime));
            bonusTextScript.AddBonusText("Color Randomizer", RandomizerTime);
            Destroy(gameObject);
        }
    }
    private bool BonusIsOutOfPlayerRange() {
        if (transform.position.x - 30 > playerTransform.position.x || playerTransform.position.x > transform.position.x + 20)
            return true;
        else return false;
    }
}
