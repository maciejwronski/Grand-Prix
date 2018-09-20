using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTextureManager : MonoBehaviour {

    [SerializeField] private Texture[] NormalTextures;
    [SerializeField] private Texture[] DownTextures;
    [SerializeField] private Texture[] UpTextures;
    private Renderer rend;
    int currentTexture;

    void Start () {
        rend = GetComponent<Renderer>();
        rend.material.mainTexture = RandomTexture();
	}
    void Update() {
        if (!PauseScript.gamePaused) {
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                rend.material.mainTexture = DownTextures[currentTexture];

            else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                rend.material.mainTexture = UpTextures[currentTexture];

            else
                rend.material.mainTexture = NormalTextures[currentTexture];

        }
    }

    private Texture RandomTexture() {
        currentTexture = Random.Range(0, NormalTextures.Length - 1);
        return NormalTextures[currentTexture];
    }
    public void changeColor() {
        rend = GetComponent<Renderer>();
        RandomTexture();
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            rend.material.mainTexture = DownTextures[currentTexture];
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            rend.material.mainTexture = UpTextures[currentTexture];
        }
        else
            rend.material.mainTexture = NormalTextures[currentTexture];
    }
}
