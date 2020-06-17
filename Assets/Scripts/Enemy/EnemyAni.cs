using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;
public class EnemyAni : NetworkBehaviour
{
    public Vector3 target;
    private Animator ani;
    private Vector3 startPosition;
    public bool isignore = false;
    public bool die;
    GameObject player;
    float playerdis;
    // Use this for initialization
    void Start()
    {
        ani = GetComponent<Animator>();
        startPosition = this.transform.position;
    }
    [Command]
    void CmdRun()//跑的动画有问题，
    {
        this.transform.Translate(Vector3.forward * 0.015f);
    }
    void Update()
    {
        if (player == null)
        {
            return;
        }
        float distance = (transform.position - startPosition).sqrMagnitude;
        playerdis = (transform.position - player.transform.position).sqrMagnitude;
        //如果距离出生点太远
        if (distance > 40)
        {
            //设置为走回起始点
            isignore = true;
            target = startPosition; 
            Cmdgoback();
            //ani.SetBool("shoot2walk ", true);
        }
        else if (distance < 8)
        {
            Cmdgetspawnidle();
            //ani.SetBool("walk2idle 0", true);
            isignore = false;
        }
        AnimatorStateInfo stateInfo = ani.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("run"))
        {
            Vector3 forward = this.transform.TransformDirection(Vector3.forward);
            CmdRun();
        }
    }
    [Command]
    void Cmdgoback()
    {

        ani.SetBool("shoot2walk ", true);
    }
    [Command]
    void Cmdgetspawnidle()
    {
        ani.SetBool("shoot2walk ", false);
        ani.SetBool("walk2idle 0", true);

    }
    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.tag == "Player" && isignore == false)
        {
            player = other.gameObject;
            Cmdstayani(other.transform.position );
        }
    }
    [Command]
    void Cmdstayani(Vector3 other)
    {
        ani.SetBool("walk2idle 0", false);
        target = other;
        Debug.Log("开始设置动画");

        ani.SetBool("Idle2shoot 0", true);
        ani.SetBool("walk2shoot 0", true);
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CmdExit();
        }
        player = null;
    }
    [Command]
    void CmdExit()
    {
        target = startPosition;

        ani.SetBool("Idle2shoot 0", false);
        ani.SetBool("walk2shoot 0", false);

        ani.SetBool("shoot2walk 0", true);
    }
    public void isDie()
    {
        die = true;
        ani.SetBool("eve2die 0", true);
        Destroy(gameObject, 5);
    }
    [Command]
    public void CmdisDie()
    {
        die = true;
        ani.SetBool("eve2die 0", true);
        Destroy(gameObject, 5);
    }
}
