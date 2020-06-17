using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class PlayerAI : MonoBehaviour {
    public GameObject player;
    public GameObject bomb;
    NavMeshAgent nav;
	// Use this for initialization
	void Start () {
        nav = player.GetComponent<NavMeshAgent>();
        player.GetComponent<NavMeshAgent>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnClick()
    {
        player.GetComponent<NavMeshAgent>().enabled = true;
        nav.SetDestination(new Vector3(bomb.transform.position.x + 1.6f, bomb.transform.position.y, bomb.transform.position.z));

    }
}
