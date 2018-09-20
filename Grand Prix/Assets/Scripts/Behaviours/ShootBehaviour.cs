using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehaviour : MonoBehaviour {

    public static float timeBetweenShoots = 0.5f; /*/ Time between each shot /*/

    private float timeOfExistence = 2.0f; /*/ how long shot exists /*/
    private float timeOfCreate = 0.0f;
    private float speed = 1.5f; 

    [SerializeField] private GameObject explosion;
    private Transform playerTransform;
    private Rigidbody playerRB;
    private PlayerBehaviour playerBehaviour;
    private void Start() {
        timeOfCreate = Time.deltaTime;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
    }

    void Update() {
        timeOfCreate += Time.deltaTime;
        if (timeOfCreate >= timeOfExistence || ShootOutOfRange()) {
            Destroy(gameObject);
        }
    }

    /*/ Shot Movement/*/
    private void FixedUpdate() {
        if(Time.deltaTime * playerRB.velocity.x * speed < 0.5f)
             transform.Translate(Vector3.right*0.5f);
        else
            transform.Translate(Vector3.right * Time.deltaTime * playerRB.velocity.x * speed);
    }

    /*/ If enemy get's hit => kaboom /*/
    private void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Enemy" && (collider.name != "CashCar" && collider.name != "ColourCar")) {
            GameObject go;
            go = Instantiate(explosion) as GameObject;
            go.transform.position = collider.transform.position;
                FloatingText.ShowMessage("+100 points", playerTransform.position + new Vector3(0, 0, -1));
                playerBehaviour.AddToPlayerScore(100);
            
            Destroy(collider.gameObject);
            Destroy(gameObject);
            EnemyGenerator.enemyCounter--;
        }
        if(collider.name == "CashCar") {
            GameObject go;
            go = Instantiate(explosion) as GameObject;
            go.transform.position = collider.transform.position;
            FloatingText.ShowMessage("+ " +SpecialCarScript.cashForSpecialCar+  " Points", playerTransform.position + new Vector3(0, 0, -1));
            PlayerBehaviour.totalScore += SpecialCarScript.cashForSpecialCar;
            Destroy(collider.gameObject);
            Destroy(gameObject);
            EnemyGenerator.enemyCounter--;
        }
        if (collider.name == "ColourCar") {
            GameObject go;
            go = Instantiate(explosion) as GameObject;
            go.transform.position = collider.transform.position;
            int bonus = Random.Range(1, SpecialCarScript.cashForSpecialCar + 1);
            FloatingText.ShowMessage("+ " + bonus + " Points", playerTransform.position + new Vector3(0, 0, -1));
            PlayerBehaviour.totalScore += bonus;
            Destroy(collider.gameObject);
            Destroy(gameObject);
            EnemyGenerator.enemyCounter--;
        }
    }
    /*/ If shot out of range, destroy it /*/
    private bool ShootOutOfRange() {
        if (transform.position.x - 30 > playerTransform.position.x || playerTransform.position.x > transform.position.x + 20)
            return true;
        else return false;
    }
}
