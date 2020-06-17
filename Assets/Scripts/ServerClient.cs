using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ServerClient : NetworkManager {
    GameObject canvas;
    int i = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void OnServerConnect(NetworkConnection conn)
    {
        i++;
        Debug.Log(i+"qianmians"+conn);
        if (i == 2)
        {
            canvas = GameObject.Find("Canvas");
            if (canvas != null)
            {
                canvas.GetComponent<TimeUI>().startflag = true;
            }

        }
    }
}
