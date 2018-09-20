using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    private Transform playerTransform;
    private Vector3 cameraOffset;
    private void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        setNewOffset();
    }
    /*/ Resets OFFSET of the camera /*/
    private void setNewOffset() {
        cameraOffset.x = 0.0f;
        cameraOffset.y = 10.0f;
        cameraOffset.z = 0.0f;
    }

    private void Update () {
        UpdateCamera();
	}
    /*/ Updates the camera /*/
    private void UpdateCamera() {
        cameraOffset = playerTransform.position;
        cameraOffset.y = 8.5f;
        cameraOffset.z = 0.0f;
        transform.position = cameraOffset;
    }
}
