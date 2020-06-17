using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Playerstep : NetworkBehaviour {
    public AudioClip stepman2;
    public AudioClip stepkuai5;
    public GameObject headlook;
    public GameObject camera;
    public bool mouserotate = false;
    private float lastTime;
    GameObject canvas;
    public GameObject rendercamera;
    CharacterController controller;
    float walkspeed = 3;
    float runspeed = 6;
   // public Transform playerTraform;
    //脊椎的tranform
   // Transform headlookTransform;
    bool isdun = false;
    //脊椎的旋转
    Vector3 headlookRot;
    AudioSource maudio;
    Animator ani;
    int i;
    void Awake()
    {
        controller = this.GetComponent<CharacterController>();
        maudio = this.GetComponent<AudioSource>();
        ani = this.GetComponent<Animator>();
    }
	void Start () {
        GameObject.Find("IpAddr").GetComponent<Text>().text = Network.player.ipAddress;
        headlookRot.x = headlook.transform.localRotation.x;
        //headlookRot.y = 7.131001f;
        if (!isLocalPlayer)
        {
            camera.GetComponent<Camera>().enabled = false;
            camera.GetComponent<AudioListener>().enabled = false;
            rendercamera.SetActive(false);
        }
        //Cursor.lockState = CursorLockMode.None;
	}
    /*[Command]//应该有函数能直接调用我没找到
    public void CmdOnPlayerConnected()
    {
        
        canvas = GameObject.Find("canvas");
        if (canvas != null)
        {
            //当有个客户端连接
            canvas.GetComponent<TimeUI>().startflag = true;

        }
    }*/
    [Command]
    void CmdAni(string name,bool flag,string name1,bool flag1,float speed)
    {

        ani.SetBool(name, flag);
        ani.SetBool(name1, flag1);
        ani.speed = speed;
    }
    [Command]
    void CmdSimpleMove(Vector3 vec,float speed,float axis)
    {
        controller.SimpleMove(vec * speed * axis);
    }
    void Move()
    {        
        //transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal")) * 3);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 down = transform.TransformDirection(-Vector3.up);

        CmdSimpleMove(down, 10, 1);//往下，模拟重力
        Vector3 up = transform.TransformDirection(Vector3.up);


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)||connect._instance.left != 0)
        {//加入手势位置变化//加入手势位置变化//加入手势位置变化//加入手势位置变化
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //ani.SetBool("walk", true);
                //ani.SetBool("idle", false);
                //ani.speed = 2;
                CmdAni("walk", true, "idle", false, 2);
                maudio.pitch = 1.2f;
                maudio.clip = stepkuai5;
                if(!maudio.isPlaying)
                    maudio.Play();
                CmdSimpleMove(right , runspeed , Input.GetAxis("Horizontal") );
            }
            else
            {
                if (connect._instance.left!=0)
                {//加入手势位置变化
                    CmdAni("walk", true, "idle", false, 1);
                    maudio.pitch = 1;
                    maudio.clip = stepman2;
                    if (!maudio.isPlaying)
                        maudio.Play();
                    CmdSimpleMove(right, walkspeed, connect._instance.left);//加入手势位置变化
                }
                else
                    {
                    CmdAni("walk", true, "idle", false, 1);
                    maudio.pitch = 1;
                    maudio.clip = stepman2;
                    if (!maudio.isPlaying)
                        maudio.Play();
                    CmdSimpleMove(right ,walkspeed , Input.GetAxis("Horizontal"));

                }
            }
        }
        else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) || connect._instance.up != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift)){
                //ani.SetBool("walk", true);
                //ani.SetBool("idle", false);
                //ani.speed = 2;
                CmdAni("walk", true, "idle", false, 2);
                maudio.pitch = 1.2f;
                maudio.clip = stepkuai5;
                if (!maudio.isPlaying)
                    maudio.Play();
                CmdSimpleMove(forward , runspeed , Input.GetAxis("Vertical"));
            }
            else
            {
                if (connect._instance.up!=0)
                {//加入手势位置变化
                    CmdAni("walk", true, "idle", false, 1);
                    maudio.pitch = 1;
                    maudio.clip = stepman2;
                    if (!maudio.isPlaying)
                        maudio.Play();
                    CmdSimpleMove(forward, walkspeed, connect._instance.up);//加入手势位置变化
                }
                else
                {
                    CmdAni("walk", true, "idle", false, 1);
                    maudio.pitch = 1;
                    maudio.clip = stepman2;
                    if (!maudio.isPlaying)
                        maudio.Play();
                    CmdSimpleMove(forward, walkspeed ,Input.GetAxis("Vertical"));

                }
            }
        }        
        else if (connect._instance.up == 0 && connect._instance.left == 0)
        {//超过很长时间不动了
            lastTime = Time.time;
            if (Time.time > lastTime + 15f)
            {
                lastTime = Time.time;
                if (maudio.isPlaying)
                    maudio.Stop();
                CmdAni("walk", false, "idle", true, 1);
            }
        }
        else
        {
            if (maudio.isPlaying)
                maudio.Stop();
            //ani.SetBool("walk", false);
            //ani.SetBool("idle", true);
            //ani.speed = 1;
            CmdAni("walk", false, "idle", true, 1);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (isdun == false && controller.isGrounded)
            {
                CmdSimpleMove(up , walkspeed ,Input.GetAxis("Vertical"));
            }
        }
    }
    [Command]//客户端调用，服务器执行,服务端除了要知道客户端人物转向，还要知道骨骼的拉伸
    void CmdLook(Vector3 headlookRot,float v,float h)
    {
        headlookRot.z += v;
        headlookRot.z = Mathf.Clamp(headlookRot.z, -30, 30);//上下
        //Debug.Log("headlookRot.z" + headlookRot.z);
        headlookRot.x += h;
        // headlookRot.x = Mathf.Clamp(headlookRot.x, -60, 60);//左右
        headlook.transform.localEulerAngles = new Vector3(0, 7.131001f, headlookRot.z);

        Vector3 playerRot = new Vector3(0, headlookRot.x, 0);

        this.transform.eulerAngles = playerRot;//服务端执行了根的旋转
       // Rpclook(v, h);//服务器执行完让客户端自己调用，如果直接在客户端调用，由于加了组件服务器限制客户端人物运动
    }
	// Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }//是当前玩家才能控制
        if (isClient && !isServer)
        {
            canvas = GameObject.Find("Canvas");
            canvas.GetComponent<TimeUI>().startflag = true;
        }
        if (Input.GetMouseButtonDown(1))//按下右键鼠标显示和消失
        {
            i++;
            if (i % 2 == 0)
            {
                Cursor.visible = true;
                Cursor.lockState=CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if (Cursor.visible == false)
        {   
            Move();
            float h=0;
            float v=0;
            if (mouserotate == true)
            {
                 h = Input.GetAxis("Mouse X");
                 v = Input.GetAxis("Mouse Y");
            }
            else
            {
                h = connect._instance.changex0;
                v = connect._instance.changey0;

            }

           // h = connect._instance.left;
           // v = connect._instance.up;
            //客户端自己调用自己的骨骼，改变视角朝向
            headlookRot.z += v;
            headlookRot.z = Mathf.Clamp(headlookRot.z, -30,30);//上下
           
            headlookRot.x += h;
           // headlookRot.x = Mathf.Clamp(headlookRot.x, -60, 60);//左右
            headlook.transform.localEulerAngles = new Vector3(0, 7.131001f, headlookRot.z);
            if (isServer)
            {
                Rpcserverbine(v, h);
            }
            //Debug.Log("headlookRot.x" + headlookRot.x);
            //不需要 旋转
            CmdLook(headlookRot,v,h);//让服务器知道客户端的旋转，服务器只同步根Transform信息
        }
        
	}
    [ClientRpc]
    void Rpcserverbine(float v,float h)
    {
        headlookRot.z += v;
        headlookRot.z = Mathf.Clamp(headlookRot.z, -30, 30);//上下

        headlookRot.x += h;
        // headlookRot.x = Mathf.Clamp(headlookRot.x, -60, 60);//左右
        headlook.transform.localEulerAngles = new Vector3(0, 7.131001f, headlookRot.z);

    }

}
