using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCarScript : MonoBehaviour {

    public static int cashForSpecialCar = 1000;
    private bool OilCar = false;

    // (1/Amount of specialCars) * (1/CarChance) = Chance to spawn this car
    private int oilCarChance = 3;
    private int chanceToGenerateOil = 1000;

    private bool cashCar = false;
    private int cashCarChance = 3;

    private bool colourCar = false;
    private int colourCarChance = 3; 

    [SerializeField] GameObject oilgo;
    [SerializeField] Texture oilCarTexture;
    [SerializeField] Texture cashCarTexture;
    [SerializeField] 
    private Renderer myRend;
    void Start() {
        switch (Random.Range(1, 4)) { // 1/9
        case 1:  OilCar = checkForOilCar();
        if (OilCar) {
            name = "OilCar";
            myRend = GetComponent<Renderer>();
            myRend.material.mainTexture = oilCarTexture;
            return;
        } break;
            case 2: cashCar = checkForCashCar(); // 1/9
        if (cashCar) {
            name = "CashCar";
            myRend = GetComponent<Renderer>();
            myRend.material.mainTexture = cashCarTexture;
            return;
        }break;
        case 3: colourCar = checkForColourCar(); // 1/9
        if (colourCar) {
            name = "ColourCar";
            StartCoroutine(SetColourful());
            return;
        } break;
    }
}
	void Update () {
        if (OilCar) {
            if(Random.Range(1,chanceToGenerateOil) == 1) {
                GameObject go;
                go = Instantiate(oilgo) as GameObject;
                go.transform.position = transform.position;
            }
        }
	}
    private bool checkForOilCar() {
        if (Random.Range(1, oilCarChance) == 1)
            return true;
        else return false;
    }
    private bool checkForColourCar() {
        if (Random.Range(1, colourCarChance) == 1)
            return true;
        else return false;
    }
    private bool checkForCashCar() {
        if (Random.Range(1, cashCarChance) == 1)
            return true;
        else return false;
    }
    private IEnumerator SetColourful() {
        CarColorChanger colorChanger = GetComponent<CarColorChanger>();
        while (true) { 
            colorChanger.changeColor();
            yield return new WaitForSeconds(1f);
        }
    }

    public bool isOilCar() {
        return OilCar;
    }
    public bool isCashCar() {
        return cashCar;
    }
}
