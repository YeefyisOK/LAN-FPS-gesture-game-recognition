using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunZhunxing : MonoBehaviour {

	// Use this for initialization
    public Texture texture;
    // Use this for initialization  
    void Start()
    {

    }

    // Update is called once per frame  
    void Update()
    {

    }
    void OnGUI()
    {
        Rect rect = new Rect(Screen.width/2 - (texture.width >> 1),
            Screen.height/2- (texture.height >> 1),
            texture.width, texture.height);
        GUI.DrawTexture(rect, texture);
    }  
}
