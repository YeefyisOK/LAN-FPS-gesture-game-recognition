using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {
    public int speed = 1;//子弹速率
    GameObject robot;
    public GameObject robotfire;
    private Vector3 velocity;//子弹速度
    GameObject particlefire;
    Health targetHealth;
    void Start()
    {
        robot = GameObject.Find("robot");
        if(this.tag=="Enemy"){
            velocity = speed * (robot.GetComponent<EnemyAni>().target+new Vector3(0,1,0) - this.transform.position);//子弹方向
        }
        else
        {
            velocity = speed * this.transform.forward;
        }
        this.GetComponent<Rigidbody>().velocity = velocity;
        Destroy(gameObject, 5f);//1秒后摧毁游戏对象s       
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player")
        {
            Debug.Log("实例化火");
            particlefire = Instantiate(robotfire, this.transform.position, Quaternion.identity);
            Destroy(particlefire, 0.7f);
            if (isServer)
            {
                targetHealth = other.collider.GetComponent("Health") as Health;
                if (targetHealth)
                {
                    //调用该脚本的OnDamage方法对该物体进行伤害
                    targetHealth.OnDamage();
                }
                Destroy(this.gameObject);

            }
        }
        else
        {
            if (isServer)
            {

            }
            //Destroy(this.gameObject);

        }
        
    }

	// Update is called once per frame
	void Update () {
	}
}
