using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class myBloodUI : MonoBehaviour {

    public Text mybloodText;
    public Slider mysd;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
    public void OnValueChanged(float blood1)
    {
        mybloodText.text = blood1.ToString() + "%";
        mysd.value = blood1;
    }
}
