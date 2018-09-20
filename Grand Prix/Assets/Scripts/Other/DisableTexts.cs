using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableTexts : MonoBehaviour {

    [SerializeField] Text first;
    [SerializeField] Text second;

	void Start () {
        first.enabled = false;
        second.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
