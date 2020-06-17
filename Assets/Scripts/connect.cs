using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Threading;
public class connect : MonoBehaviour {
    public static connect _instance;
    [DllImport("PLAYCV")]
    private static extern void ActionRun();
    [DllImport("PLAYCV")]
    private static extern int GetDetecnum();
    [DllImport("PLAYCV")]
    private static extern int GetLeft();
    [DllImport("PLAYCV")]
    private static extern int GetUp();
    [DllImport("PLAYCV")]
    private static extern int Stop();

    [DllImport("PLAYCV")]
    private static extern int Getxroa();

    [DllImport("PLAYCV")]
    private static extern int Getyroa();
    [DllImport("PLAYCV")]   
    private static extern bool IsGun();
//    [DllImport("PLAYCV")]
//    private static extern double y;
    public bool canfire=false;
    public int lastx0;
    public int lasty0;
    public int newx0;
    public int newy0;
    public int changex0;
    public int changey0;
    public Thread thread;
    public int detenum;
    public int left = -1;
    public int up = -1;
    void Awake()
    {
        _instance = this;
    }
	// Use this for initialization
    void StartThread()
    {
        thread.Start();
        print("thread start run\n");
    }
    // Use this for initialization
    void Start()
    {
        thread = new Thread(new ThreadStart(Run));
        thread.IsBackground = true;
        thread.Start();
        detenum = GetDetecnum();
        left = GetLeft();
        up = GetUp();
        lastx0 = Getxroa();
        lasty0 = Getyroa();
    }
	// Update is called once per frame
    void Update()
    {
        
        print("当前线程状态 : " + thread.ThreadState.ToString() + "\n");
        //print(colorType.ToString() + " number = " + (int)colorType);
    }
    private void FixedUpdate()
    {
        detenum = GetDetecnum();
        left = GetLeft();
        up = GetUp();
        Debug.Log("凸包数为" + detenum);
        Debug.Log("Left是" + left);
        Debug.Log("Up是" + up);
        canfire=IsGun();
        lastx0 = newx0;
        newx0 = Getxroa();
        lasty0 = newy0;
        newy0 = Getyroa();
        changex0 = newx0 - lastx0;
        changey0 = newy0 - lasty0;

    }
    void Run()
    {
        ActionRun();
        print("thread run out\n");
        return;
    }
    void OnGUI()
    {
        //开始按钮  
        if (GUI.Button(new Rect(0, 10, 100, 30), "退出摄像头"))
        {
            Stop();
        }

    }
    public void StopFengzhuang()
    {
        Stop();
    }
}
