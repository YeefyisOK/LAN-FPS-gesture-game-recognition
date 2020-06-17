using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManagment : MonoBehaviour
{
    int[] anothergun;
    bool[] gunperf;
    public GameObject gun0;
    public GameObject gun1;
    public GameObject gun2;
    public GameObject gun3;
    public GameObject gun4;
    bool flag;
    // Use this for initialization
    void Start()
    {
        gunperf=Guan.gunper;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            if (Guan.gunper[i] == true)
            {
                switch (i)
                {
                    case 0:
                        gun0.SetActive(true);
                        flag = true;
                        break;
                    case 1:
                        gun1.SetActive(true);
                        flag = true;
                        break;
                    case 2:
                        gun2.SetActive(true);
                        flag = true;
                        break;
                    case 3:
                        gun3.SetActive(true);
                        flag = true;
                        break;
                    case 4:
                        gun4.SetActive(true);
                        flag = true;
                        break;
                    default:
                        Debug.Log("error");
                        break;
                }
                if (flag == true)
                {
                    break;//显示一把枪就好了
                }
            }
        }
    }

}
