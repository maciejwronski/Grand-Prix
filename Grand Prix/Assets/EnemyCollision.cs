using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour {

    private bool collisionWithCar;
    private bool enemyExplosionOnCollisionWithBridge = true;

    private int DirectionOfCar;

    private PlayerBehaviour playerBehaviour;
    private EnemyCarBehaviour enemyCarBehaviour;
    private Transform playerTransform;

    [SerializeField] private GameObject explosion;

    private void Start() {
        playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        enemyCarBehaviour = GetComponent<EnemyCarBehaviour>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void OnCollisionEnter(Collision collision) {
        if (GameObject.FindGameObjectWithTag("RoadBridge") != null && collision.collider.tag == "BridgeBound" && enemyExplosionOnCollisionWithBridge == true) {
            CreateExplosion();
            EnemyGenerator.enemyCounter--;
            Destroy(gameObject);
        }
        if (collision.collider.tag == "Player" && playerBehaviour.IsImmune() == true) {
            CreateExplosion();
            EnemyGenerator.enemyCounter--;
            FloatingText.ShowMessage("+100 points", playerTransform.position + new Vector3(0, 0, -1));
            playerBehaviour.AddToPlayerScore(100);
            Debug.Log("aaa");
            Destroy(gameObject);
        }
        if (collision.collider.tag == ("Player") || collision.collider.tag == ("Enemy")) {
            collisionWithCar = true;
        }
    }
    private void OnCollisionExit(Collision collision) {
        if (collision.collider.tag == ("Player") || collision.collider.tag == ("Enemy"))
            collisionWithCar = false;
    }
    private void OnCollisionStay(Collision collision) {
        if (collision.collider.tag == ("Player") || collision.collider.tag == "Enemy") {
            enemyCarBehaviour.setDirectionOfCar(0);
        }
    }
    private void CreateExplosion() {
        Vector3 pos = transform.position;
        GameObject go;
        go = Instantiate(explosion);
        go.transform.position = pos;
    }
    public bool collidingWithCar() {
        return collisionWithCar;
    }
}
