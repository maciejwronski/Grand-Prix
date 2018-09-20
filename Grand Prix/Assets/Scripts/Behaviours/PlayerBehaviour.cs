using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    public static int totalScore = 0;
    public static int coinsCollected = 0;
    private bool isRotating; // Is player rotating?
    private bool isColouring; // Is player getting coloured?
    private bool HasFlower = false;
    private bool canShoot = false;
    private bool hasNitro = false;
    private bool playerCanMoveWhileOnRotation = false; // Can player move, when he will be rotated
    private bool playerImmune = false;
    private Rigidbody rb;
    private GameObject playerObject;
    [SerializeField] private GameObject shootObject;

    void Start() {
        rb = GetComponent<Rigidbody>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (HasFlower == false || !canShoot)
                return;
            StartCoroutine(Shoot());
        }
    }
    /*/ Shoot /*/
    private IEnumerator Shoot() {
        GameObject go;
        go = Instantiate(shootObject) as GameObject;

        Vector3 pos = new Vector3(transform.position.x + 1.5f, 0.55f, transform.position.z);
        go.transform.position = pos;
        canShoot = false;
        yield return new WaitForSeconds(ShootBehaviour.timeBetweenShoots);
        canShoot = true;
    }
    /*/ Rotate function /*/
    private IEnumerator Rotate(Vector3 angles, float duration) {
        isRotating = true;
        for (int i = 0; i < 2; i++) {
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.Euler(angles) * startRotation;
            for (float t = 0; t < duration; t += Time.deltaTime) {
                transform.rotation = Quaternion.Lerp(startRotation, endRotation, t / duration);
                yield return null;

                transform.rotation = endRotation;
            }
        }

        isRotating = false;
    }

    /*/ Set player coloured function /*/
    private IEnumerator SetColourful(float duration) {
        isColouring = true;
        PlayerTextureManager colorChanger = GetComponent<PlayerTextureManager>();
        for (int t = 0; t < duration; t++) {
            colorChanger.changeColor();
            yield return new WaitForSeconds(1f);
        }
        isColouring = false;
    }

    /*/ Disables flower bonus /*/
    private void DisableFlower() {
        canShoot = false;
        HasFlower = false;
    }
    /*/ Sets player immune /*/
    private void DisableImmune() {
        playerObject.layer = 0;
        playerImmune = false;
    }
    /*/ Disables player's nitro /*/
    private void DisableNitro() {
        hasNitro = false;
    }

    /*/ Adds points to player score /*/
    public void AddToPlayerScore(float add) {
        totalScore += Mathf.FloorToInt(add);
    }
    /*/ Subtracts from player score /*/
    public void SubtractPlayerScore(float subb) {
        totalScore -= Mathf.FloorToInt(subb);
    }
    public void AddCoin(int defaultIndex = 1) {
        coinsCollected += defaultIndex;
    }
    /*/ Sets player immune /*/
    public void SetImmune(float time) {
        playerObject.layer = 8;
        playerImmune = true;
        Invoke("DisableImmune", time);
    }
    /*/ Player gets nitro /*/
    public void NitroBonus(float time) {
        hasNitro = true;
        Invoke("DisableNitro", time);
    }
    /*/ Reduces player speed by multiplier of reducer /*/
    public void ReduceSpeed(float reducer) {
        rb.velocity = rb.velocity * reducer;
    }
    /*/ If player is not getting coloured already, activate bonus /*/
    public void StartColour(float Time) {
        if (!isColouring) {
            StartCoroutine(SetColourful(Time));
        }
    }
    /*/ Grants player flower bonus/*/
    public void FlowerBonus(float Time) {
        HasFlower = true;
        canShoot = true;
        Invoke("DisableFlower", Time);
    }
    public bool IsRotating() {
        return isRotating;
    }
    public bool CanMoveOnRotation() {
        return playerCanMoveWhileOnRotation;
    }
    public bool HasNitro() {
        return hasNitro;
    }
    /*/ If player is not getting rotated already, activate bonus /*/
    public void StartRotation(float Degrees, float Time) {
        if (!isRotating)
            StartCoroutine(Rotate(Vector3.up * Degrees, Time));
    }
    public bool IsImmune() {
        return playerImmune;
    }
    /*/ Returns player score /*/
    public int getPlayerScore() {
        return totalScore;
    }

}

