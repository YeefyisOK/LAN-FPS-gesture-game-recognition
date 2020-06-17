using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeUI : MonoBehaviour {
    public GameObject gameover;
    public Sprite []d=new Sprite[10];
    public Sprite maohaoSprite;
    public GameObject fen;
    public GameObject shimiao;
    public GameObject gemiao;
    public GameObject maohao;
    int i = 180;
    int j = 0;
    public bool startflag = false;
	// Use this for initialization
	void Start () {
        maohao.GetComponent<Image>().sprite = maohaoSprite;
	}	
	// Update is called once per frame
	void FixedUpdate () {
        if (startflag == false)
        {
            return;
        }
        
        j++;
        if (j == 50)
        {
            i-- ;
            j = 0;
        }
        gemiao.GetComponent<Image>().sprite = d[i % 10];
        shimiao.GetComponent<Image>().sprite = d[i % 60 / 10];
        fen.GetComponent<Image>().sprite = d[i / 60];
        if (i == 0)
        {
            gameover.SetActive(true);
        }
	}
    
}
