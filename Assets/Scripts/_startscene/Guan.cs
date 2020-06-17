using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Guan : MonoBehaviour {
    public GameObject weaponpanel;
    public Text text;
    public GameObject buttonzhuangBei1;
    public GameObject buttonzhuangBei2;
    public GameObject buttonzhuangBei3;
    public GameObject buttonzhuangBei4;
    public GameObject buttonzhuangBei5;
    public static bool[] gunper = new bool[5];//枪的预制体
    
    public Sprite yizhuangbei;
    public Sprite zhuangbei;

    public int []a = new int[5];
	// Use this for initialization   
    int k = 0;
	void Start () {
        text.text = "";
        for (int i = 0; i < 5; i++)
        {
            gunper[i] = false; 
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnClickHelp()
    {
        k++;
        if(k%2 != 0){

            text.text="";
        }
        else
        {
            text.text = "WASD控制人物行走,shift加速，鼠标控制视野，右键切换鼠标显示和隐藏,你只能携带一把枪，你的任务是在规定时间内消灭所有敌人并拆弹";
        }

    }
    public void OnClickGuan()
    {
        weaponpanel.SetActive(false);
    }
    public void OnClickWeapon()
    {
        weaponpanel.SetActive(true);
    }

    public void OnClickStart()
    {        
        for (int i = 0; i < 5; i++)
        {
            if (a[i]%2 != 0)
            {
                switch (i)
                {
                    case 0:
                        gunper[0] = true;
                        break;
                    case 1:
                        gunper[1] = true;
                        break;
                    case 2:
                        gunper[2] = true;
                        break;
                    case 3:
                        gunper[3] = true;
                        break;
                    case 4:
                        gunper[4] = true;
                        break;
                    default:
                        Debug.Log("error");
                        break;
                }
            }
        }
        SceneManager.LoadScene("GameScene");
    }   
    public void OnClickZhuangbei0()//手枪
    {
        a[0]++;
        if (a[0] % 2 == 0)
        {
            buttonzhuangBei1.GetComponent<Image>().sprite = zhuangbei;
        }
        else
        {
            buttonzhuangBei1.GetComponent<Image>().sprite = yizhuangbei;
        }
    }
    public void OnClickZhuangbei1()
    {
        a[1]++;
        if (a[1] % 2 == 0)
        {
            buttonzhuangBei2.GetComponent<Image>().sprite = zhuangbei;
        }
        else
        {
            buttonzhuangBei2.GetComponent<Image>().sprite = yizhuangbei;
        }
    }
    public void OnClickZhuangbei2()//冲锋枪
    {
        a[2]++;
        if (a[2] % 2 == 0)
        {
            buttonzhuangBei3.GetComponent<Image>().sprite = zhuangbei;
        }
        else
        {
            buttonzhuangBei3.GetComponent<Image>().sprite = yizhuangbei;
        }
    }
    public void OnClickZhuangbei3()//狙击枪
    {
        a[3]++;
        if (a[3] % 2 == 0)
        {
            buttonzhuangBei4.GetComponent<Image>().sprite = zhuangbei;
        }
        else
        {
            buttonzhuangBei4.GetComponent<Image>().sprite = yizhuangbei;
        }
    }
    public void OnClickZhuangbei4()
    {
        a[4]++;
        if (a[4] % 2 == 0)
        {
            buttonzhuangBei5.GetComponent<Image>().sprite = zhuangbei;
        }
        else
        {
            buttonzhuangBei5.GetComponent<Image>().sprite = yizhuangbei;
        }
    }
}
