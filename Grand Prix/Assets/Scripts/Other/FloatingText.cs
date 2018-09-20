using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FloatingText : MonoBehaviour {
    private Vector3 Position;
    private Vector3 ScreenPointPosition;
    private Camera CameraHold;
    private string Text; // Use this for initialization
    public Font myFont;

    void Start() {
        CameraHold = Camera.main;
        ScreenPointPosition = CameraHold.WorldToScreenPoint(Position);
    }
    void Update() {
        ScreenPointPosition.y -= .5f; //Controls popup vertical movement speed 
    }
    public static void ShowMessage(string text, Vector3 position) {
        var NewInstance = new GameObject("BonusPoint"); //Creating new gameobject named 'Damage PopUp' 
        var PopUp = NewInstance.AddComponent<FloatingText>(); //Adding this script to new gameobject created 
        PopUp.Position = position;
        PopUp.Text = text;
    }
    void OnGUI() { //Creating a rectangle, at 'xy' position, with width and heigth, with 'Text' inside: 
        GUIStyle font = new GUIStyle();
        font.normal.textColor = new Color(255, 156, 0);
        font.font = myFont;
        font.fontSize = 30;
        GUI.Label(new Rect(ScreenPointPosition.x, ScreenPointPosition.y, 150, 20), Text,font);
        Destroy(gameObject, 1); //After display, 
    }
}
