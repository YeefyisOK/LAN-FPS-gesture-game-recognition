using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkandFire : MonoBehaviour {
    public float x;
    public float y;
    //子弹的prefab对象
    public GameObject bulletPrefab;
    public GameObject gunparticalpos;
    private float lastFireTime;
    //开枪的标志位
    bool firing = false;    

    Animator animator;
	// Use this for initialization
	void Start () {
        //脚本启用重新记录时间
        lastFireTime = Time.time;
        animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        
        if (stateInfo.IsName("shoot"))
        {
            firing = true;
            //sgun.transform.LookAt(player.transform.position);
            //Debug.Log("LOOKAT");
        }
        else
        {
            firing = false;
        }
        if (firing)
        {
            if (Time.time > lastFireTime + 1.5f)
            {
                //记录上一次的发射时间
                lastFireTime = Time.time;
                //实例化子弹
                Instantiate(bulletPrefab, gunparticalpos.transform.position, this.transform.rotation);
                //Debug.Log("SHIlihua");
            }
        }
	}
}
