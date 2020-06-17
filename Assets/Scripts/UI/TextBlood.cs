using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class TextBlood : NetworkBehaviour {
    public Text bloodText;
    public Slider sd;
    public float blood;
	// Use this for initialization
	void Start () {
        sd.value = 100;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            //OnValueChanged(this.GetComponent<Health>().blood);
        }
	}
    public void OnValueChanged(float blood1)
    {
        bloodText.text=blood1.ToString()+"%";
        sd.value = blood1;
    }
}
