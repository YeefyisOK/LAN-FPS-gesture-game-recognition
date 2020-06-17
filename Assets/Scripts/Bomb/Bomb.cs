using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bomb : MonoBehaviour {
    public GameObject zhunxing;
    public bool isChaiDan;
    public GameObject pointa;
    public GameObject pointb;
    public GameObject gamewin;
    bool startflag;
    float tempTime=0;
    public Slider sd;
    public GameObject canvas;
	// Use this for initialization
	void Start () {
        isChaiDan = false;
        if(Random.Range(0,10)<5){
            this.transform.position = pointa.transform.position;
        }
        else
        {
            this.transform.position = pointb.transform.position;
        }
	}	
	// Update is called once per frame
	void Update () {
        sd.value=tempTime;
	}
    void OnTriggerStay(Collider other)
    {
        GameObject []go= GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(go.Length);
        startflag=canvas.GetComponent<TimeUI>().startflag;
        if (go.Length >1|| startflag == false)
        {
            return;
        }
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<BulletShoot>().canfire=false;
            zhunxing.SetActive(false);
            sd.gameObject.SetActive(true);
            isChaiDan=true;            
        }
        if (Input.GetMouseButton(0))
        {
            tempTime = Mathf.Lerp(tempTime, 1, Time.deltaTime);
        }
        else
        {
            tempTime = Mathf.Lerp(tempTime, 0, Time.deltaTime);
        }
        if (tempTime > 0.99)
        {
            tempTime = 1;
            gamewin.SetActive(true);
            Time.timeScale = 0;
        }
    }
    
}
