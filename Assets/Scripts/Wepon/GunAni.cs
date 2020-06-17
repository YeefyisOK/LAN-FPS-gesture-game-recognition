using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAni : MonoBehaviour {
    Animation mAni;
    public float fireAnimationSpeed = 1;//开枪速度
    public float takeInOutSpeed = 1;//枪的装入、掏出速度
    public float reloadMiddleRepeat = 3;//装载弹药中间动画重复次数
    public bool isSniperRifle = false;//是否为狙击步枪标志位
    public AudioClip reload;
    public AudioClip weaponchange;

    AudioSource maudio;
    bool flag;
	// Use this for initialization
	void Awake () {
        mAni = GetComponent<Animation>();
        mAni.Play("Idle");

        maudio = GetComponent<AudioSource>();
        if (this.gameObject.name == "Hands+Blaser R93 LRS2")
        {

            isSniperRifle = true;
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void Fire()
    {
        mAni.Rewind("Fire");//倒回到动画开始时
        mAni["Fire"].speed = fireAnimationSpeed;//控制动画播放速度
        mAni.Play("Fire");//播放射击动画
    }
    public void Reloading(float reloadTime)
    {
        maudio.clip = reload;
        if (!maudio.isPlaying)
        {
            maudio.Play();
        }
        if (!isSniperRifle)
        {//当不是狙击步枪时
            mAni.Stop("Reload");//停止重新装载（弹药）动画
            mAni["Reload"].speed = mAni["Reload"].clip.length / reloadTime;//控制动画播放速度
            mAni.Rewind("Reload");//控制动画播放速度
            mAni.Play("Reload");//播放重新装载（弹药）动画
        }
        else
        {
            AnimationState newReload1 = mAni.CrossFadeQueued("Reload_1_3");//将装弹的第一段动画放入淡入淡出队列
            newReload1.speed = mAni["Reload_1_3"].clip.length / reloadTime;//控制动画播放速度
            for (int i = 0; i < reloadMiddleRepeat; i++)
            {
                AnimationState newReload2 = mAni.CrossFadeQueued("Reload_2_3");//将装弹的第二段动画放入淡入淡出队列
                newReload2.speed = mAni["Reload_2_3"].clip.length / reloadTime;//控制动画播放速度
            }
            AnimationState newReload3 = mAni.CrossFadeQueued("Reload_3_3");//将装弹的第三段动画放入淡入淡出队列
            newReload3.speed = mAni["Reload_3_3"].clip.length / reloadTime;//控制动画播放速度
        }
    }
    public void TakeIn()
    {
        mAni.Rewind("TakeIn");//倒回到动画开始时
        mAni["TakeIn"].speed = takeInOutSpeed;//控制动画播放速度
        mAni["TakeIn"].time = 0;//从动画开始的地方开始播放
        mAni.Play("TakeIn");//播放抢装入动画
    }

    public void TakeOut()
    {
        maudio.clip = weaponchange;
        if (!maudio.isPlaying)
        {
            maudio.Play();
        }
        mAni.Rewind("TakeOut");//倒回到动画开始时
        mAni["TakeOut"].speed = takeInOutSpeed;//控制动画播放速度
        mAni["TakeOut"].time = 0;//从动画开始的地方开始播放
        mAni.Play("TakeOut");//播放掏枪动画
    }
}
