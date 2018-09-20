using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAnim : MonoBehaviour {


    RectTransform myRect;
    [SerializeField] RawImage[] cars;
    private float time = 1f;

    private bool menuAnim;
    void Start () {
        switch (PlayerPrefs.GetInt("MenuAnimation")) {
            case 0: menuAnim = false; break;
            case 1: menuAnim = true; break;
        }
        myRect = gameObject.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (menuAnim == true) {
            foreach (RawImage car in cars) {
                Vector3 newVec = new Vector3(car.rectTransform.position.x + time, car.rectTransform.position.y, car.rectTransform.position.z);
                car.rectTransform.position = newVec;
                if (car.rectTransform.position.x >= myRect.rect.width * myRect.localScale.x + car.rectTransform.rect.width*myRect.localScale.x) {
                    newVec = new Vector3(-car.rectTransform.rect.width * myRect.localScale.x, car.rectTransform.position.y, car.rectTransform.position.z);
                    car.rectTransform.position = newVec;
                }
            }
        }
    }
}
