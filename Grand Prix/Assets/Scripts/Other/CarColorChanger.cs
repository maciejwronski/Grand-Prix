using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarColorChanger : MonoBehaviour {

    [SerializeField] private Texture[] NormalTextures;
    [SerializeField] private Texture[] DownTextures;
    [SerializeField] private Texture[] UpTextures;
    [SerializeField] private Texture[] SpecialCarDown;
    [SerializeField] private Texture[] SpecialCarUp;
    [SerializeField] private Texture[] SpecialCarNormal;

    private Renderer rend;
    EnemyCarBehaviour enemyCarBehaviour;
    int currentTexture;
    void Start() {
        rend = GetComponent<Renderer>();
        rend.material.mainTexture = RandomTexture();
        enemyCarBehaviour = GetComponent<EnemyCarBehaviour>();
    }
    private Texture RandomTexture() {
        currentTexture = Random.Range(0, NormalTextures.Length - 1);
        return NormalTextures[currentTexture];
    }


    public void changeColor() {
        rend = GetComponent<Renderer>();
        RandomTexture();
        if (enemyCarBehaviour.GetDirectionOfCar() == 2) {
            rend.material.mainTexture = DownTextures[currentTexture];
        }
        else if (enemyCarBehaviour.GetDirectionOfCar() == 1) {
            rend.material.mainTexture = UpTextures[currentTexture];
        }
        else
            rend.material.mainTexture = NormalTextures[currentTexture];
    }


    public void Update() {
        if (name == "OilCar") {
            if (enemyCarBehaviour.GetDirectionOfCar() == 2) {
                rend.material.mainTexture = SpecialCarDown[0];
            }
            else if (enemyCarBehaviour.GetDirectionOfCar() == 1) {
                rend.material.mainTexture = SpecialCarUp[0];
            }
            else rend.material.mainTexture = SpecialCarNormal[0];
            return;
        }
        if (name == "CashCar") {
            if (enemyCarBehaviour.GetDirectionOfCar() == 2) {
                rend.material.mainTexture = SpecialCarDown[1];
            }
            else if (enemyCarBehaviour.GetDirectionOfCar() == 1) {
                rend.material.mainTexture = SpecialCarUp[1];
            }
            else rend.material.mainTexture = SpecialCarNormal[1];
            return;
        }

        if (enemyCarBehaviour.GetDirectionOfCar() == 2) {
            rend.material.mainTexture = DownTextures[currentTexture];
        }
        else if (enemyCarBehaviour.GetDirectionOfCar() == 1) {
            rend.material.mainTexture = UpTextures[currentTexture];
        }
        else
            rend.material.mainTexture = NormalTextures[currentTexture];
    }
}
