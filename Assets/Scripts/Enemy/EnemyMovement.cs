using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    
    Vector3 targ;
    private Vector3 dir;
    private Vector3 movedir;
	// Use this for initialization
	void Start () {
        dir = -transform.right;        
	}
	
	// Update is called once per frame
	void Update () {
        //设置插值朝向目标点
        if (this.GetComponent<EnemyAni>().target != null)
        {
            targ = this.GetComponent<EnemyAni>().target;
            movedir = targ - transform.position;
            movedir.y = 0;
            dir = Vector3.Slerp(dir, movedir, Time.deltaTime);
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir);  
        }    

	}
}
