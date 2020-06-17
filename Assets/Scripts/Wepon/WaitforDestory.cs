using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitforDestory : MonoBehaviour {
    public GameObject bulletHoleHeat;
    public float lifeTime = 0.3f;//定义对象生存时间
    public bool isFade = true ;//是否逐渐消失，然后销毁
     float duration = 0.1f;//对颜色进行线性插值时的时间间隔
    void Awake()
    {
        if (!isFade)
        {
            Destroy(gameObject, lifeTime);//lifeTime秒后摧毁对象
        }
        else
        {
            StartCoroutine(FadeAndDestroy());//启动协程
        }
    }

    IEnumerator FadeAndDestroy()
    {
        while (true)
        {
            yield return new WaitForSeconds(lifeTime);//等待lifeTime秒

            Renderer render = bulletHoleHeat.GetComponent<MeshRenderer>();
            Color c   =  render.material.GetColor("_TintColor");//得到材质主颜色
            //Debug.Log(c.a);
            c.a = Mathf.Lerp(c.a, 0, duration);//对颜色的alpha值进行插值
            bulletHoleHeat.GetComponent<MeshRenderer>().material.SetColor("_TintColor",c);//改变材质颜色（alpha值不断减小）
            if (c.a < 0.2f)
            {//当alpha值小于0.2时，
                Destroy(gameObject);//销毁对象
            }
        }
    }
}
