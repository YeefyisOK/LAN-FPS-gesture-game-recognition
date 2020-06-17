using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class Health : NetworkBehaviour {
    //同步变量还可以指定函数，使用hook;  
    //当服务器改变了blood的值，客户端会调用ChangHP这个函数  
    //这个值是以服务器为准。就算客户端改变了，服务器改变之后，客户端还是显示服务器的数据  
    //[SyncVar(hook = "ChangHP")]
    [SyncVar]
    public float blood;
    GameObject Canvas;
    float weiliPlayer=18;
    float weiliRobot=10;
    GameObject gamelose;
    GameObject canvas;
    Transform startpos;
    Animator ani;
    public void ChangHP(float newHP)
    {
        blood = newHP;
    }
	// Use this for initialization
	void Start () {

        ani = GetComponent<Animator>();
       // GameObject root = GameObject.Find("Canvas");
        canvas = GameObject.Find("Canvas");
        if (transform.tag == "Player")
        {
            blood = 100;
        }
        if (transform.tag == "Enemy")
        {
            blood = 50;
        }
        weiliPlayer=10;
        weiliRobot = 18;
        startpos = this.transform;
	}

    [Command]
    void CmdDieAni()
    {
        ani.SetBool("die", true);
    }
    [Command]
    void CmdDestory()
    {
          Destroy(this.gameObject, 5.0f);

    }
	// Update is called once per frame
	void Update () {
        if (isLocalPlayer)
        {
            canvas.GetComponent<myBloodUI>().OnValueChanged(this.blood);          
            if (this.blood <= 0)
            {
                connect._instance.StopFengzhuang();
                Canvas=GameObject.Find("Canvas");
                gamelose=Canvas.transform.Find("Gameover").gameObject;
                gamelose.SetActive(true);
                CmdDestory();
                Destroy(this.gameObject, 5.0f);
                //Time.timeScale = 0;
                this.blood = 0;
                CmdDieAni();
            }
        }
	}
    [Command]
    void CmdBlood(){
        this.blood -= Random.Range(weiliRobot / 2, weiliRobot);   
    }
    [ClientRpc]
    void RpcBlood()
    {
        this.blood -= Random.Range(weiliRobot / 2, weiliRobot);   

    }
    public void OnDamage()
    {
        //受到枪击
        if (transform.tag == "Player")
        {
            if (isServer)
            {
                this.blood -= Random.Range(weiliRobot / 2, weiliRobot); 

            }
        }
        if (transform.tag == "Enemy")
        {
            if (isServer)
            {
                this.blood -= Random.Range(weiliPlayer / 2, weiliPlayer); 
            }

            if (this.blood <= 0)
            {
                this.GetComponent<EnemyAni>().isDie();
                this.blood = 0;
            }
        }
    }
}
