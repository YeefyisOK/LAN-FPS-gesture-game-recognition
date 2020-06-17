using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BulletUI : MonoBehaviour {
    public Text text;
    int bulletnum;
    GameObject gun;
	// Use this for initialization
	void Start () {
        bulletnum=10;
        text.text = bulletnum.ToString() + "/10";
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    public void UpdateBulletNumUI(int bulletnum)
    {
        
        text.text = bulletnum.ToString() + "/10";
    }
}
