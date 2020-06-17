using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
public class BulletShoot : NetworkBehaviour {
    //public GameObject bulletPerfab;
    public GameObject huoguangperfab;
    public GameObject gun;
    public int bulletnum=10;
    public bool impactHoles = true;//是否产生弹孔标志位
    public GameObject bullethole;//弹孔效果对象
    public GameObject robotfire;
    public Camera camera;
    public GameObject qiangkouempty;
    public GameObject bulletperfab;
    public bool canfire = true;
    //伤害范围
    private int damageAmount = 20;
    private Vector3 velocity;//子弹速度
    private bool hasHit = false;//是否已经产生碰撞标志位
    GameObject particlefire;
    private float lastFireTime;
    GameObject bullet;
    int i;
    public Vector3 targetPoint;
    bool isChaiDan0=false;
	// Use this for initialization
	void Start () {

	}
	[Command]
    void Cmdparticlefire(Vector3 pos)
    {
        particlefire = Instantiate(robotfire, pos, Quaternion.identity);
        Destroy(particlefire, 0.7f);
    }
    [Command]
    void CmdKaihuo(Vector3 pos,Vector3 targe)
    {
        GameObject huog = Instantiate(huoguangperfab, pos, Quaternion.identity);
        NetworkServer.Spawn(huog);
        //Destroy(huog, 0.1f);
        GameObject bullet = Instantiate(bulletperfab, pos, gun.transform.rotation);
        bullet.transform.LookAt(targe);
        NetworkServer.Spawn(bullet);
        Debug.Log("服务器实例化子弹kaihuo");
    }
    //[ClientRpc]
    /*void RpcKaihuo(Vector3 pos)
    {
        GameObject huog = Instantiate(huoguangperfab, pos, Quaternion.identity);
        //huog.transform.localEulerAngles = new Vector3(0, 90, 0);
        Destroy(huog, 0.1f);
        GameObject bullet = Instantiate(bulletperfab, qiangkouempty.transform.position, gun.transform.rotation);
        Debug.Log("服务器实例化子弹kaihuo");
        bullet.transform.LookAt(targetPoint);
    }*/
   // [ClientRpc]
  //  void Rpcbullet(Vector3 targetPoint)
 //   {
  //      GameObject bullet = Instantiate(bulletperfab, qiangkouempty.transform.position, gun.transform.rotation);
 //       Debug.Log("服务器实例化子弹");
 //       bullet.transform.LookAt(targetPoint);
  //  }
	// Update is called once per frame
	void Update () {
        if (isLocalPlayer)
        {
            if ((Input.GetMouseButtonDown(0) || connect._instance.canfire==true && Cursor.visible == false && canfire))//鼠标不可见时点下开火
            {//加入手势位置变化//加入手势位置变化//加入手势位置变化//加入手势位置变化
                if (Time.time > lastFireTime + 0.5f)
                {
                    lastFireTime = Time.time;          
                    bulletnum--;
                    if (bulletnum == 0)
                    {
                        bulletnum = 10;
                    }
                    Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {  //GameObject hole = (GameObject)Instantiate(bullethole, hit.point, Quaternion.identity);
                        targetPoint = hit.point;
                        //获取碰撞物体的，即认为的Health脚本
                        //Health targetHealth = hit.collider.GetComponent("Health") as Health;
                        if (hit.transform.tag == "Enemy" || hit.transform.tag == "Player")
                        {
                            //particlefire = Instantiate(robotfire, hit.point, Quaternion.identity);
                            //Destroy(particlefire, 0.7f);
                            //Cmdparticlefire(hit.point);
                            //particlefire = Instantiate(robotfire, hit.point, Quaternion.identity);
                            //Destroy(particlefire, 0.7f);
                            impactHoles = false;
                        }
                        else
                        {
                            impactHoles = true;
                        }
                        //对物体表面子弹射击点
                        if (impactHoles)
                        {
                            //Cmdparticlefire(hit.point);
                            //弹孔没有同步
                            GameObject hole = (GameObject)Instantiate(bullethole, hit.point, Quaternion.identity);//在碰撞点上实例化一个弹孔
                            hole.transform.LookAt(hit.point + hit.normal);
                            hole.transform.Translate(Vector3.forward * 0.01f);

                            hole.transform.parent = hit.transform;
                        }
                    }
                    else
                    {
                        targetPoint = camera.transform.forward * 1000;

                    }
                  //  Rpcbullet(targetPoint);
                 //   GameObject bullet = Instantiate(bulletperfab, qiangkouempty.transform.position, gun.transform.rotation);
                  //  bullet.transform.LookAt(targetPoint);
                    //客户端自己执行
                    /*
                    GameObject huog = Instantiate(huoguangperfab, qiangkouempty.transform.position, Quaternion.identity);
                    huog.transform.localEulerAngles = new Vector3(0, 90, 0);
                    Destroy(huog, 0.1f);
                    //告诉服务端执行
                    if (isServer)
                    {//Host只进行一次判断
                        RpcKaihuo(qiangkouempty.transform.position);
                    }
                    else
                    {   GameObject bullet = Instantiate(bulletperfab, qiangkouempty.transform.position, gun.transform.rotation);
                        bullet.transform.LookAt(targetPoint);
                        CmdKaihuo(qiangkouempty.transform.position);
                    }*/
                    CmdKaihuo(qiangkouempty.transform.position,targetPoint);
                }
            
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (bulletnum != 10)
                {
                    bulletnum = 10;
                }
            }

        }
	}
}
