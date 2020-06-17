using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Threading;

public class Move : MonoBehaviour
{

    public enum ColorType
    {
        skin, black, gray, red, orange, yellow, green, cyan, blue, purple, white
    }

    public ColorType colorType;
    public GameObject obj;
    public float speed;

    [DllImport("TestDLL")]
    private static extern void ActionRun(int type);
    [DllImport("TestDLL")]
    private static extern void ActionEnd();
    [DllImport("TestDLL")]
    private static extern int GetState();

    //object lockd = new object();
    private Thread thread;
    private int state;

    void StartThread()
    {
        thread.Start();
        print("thread start run\n");
    }
    // Use this for initialization
    void Start()
    {
        thread = new Thread(new ThreadStart(Run));
        thread.IsBackground = true;
        state = GetState();
    }

    // Update is called once per frame
    void Update()
    {
        print("thread current state : " + thread.ThreadState.ToString() + "\n");
        //print(colorType.ToString() + " number = " + (int)colorType);
    }

    private void FixedUpdate()
    {
        state = GetState();
        if (state == 1)
        {
            obj.transform.position += Vector3.Lerp(obj.transform.position, obj.transform.right * speed, 1f);
            print("向右移动\n");
        }
        else if (state == 2)
        {
            obj.transform.position -= Vector3.Lerp(obj.transform.position, obj.transform.right * speed, 1f);
            print("向左移动\n");
        }
    }

    private void OnGUI()
    {
        GUI.TextArea(new Rect(Screen.width / 2, (Screen.height / 2) - 100, 200, 100), "position state : " + state);
        if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 200, 100), "Run"))
            StartThread();
        if (GUI.Button(new Rect(Screen.width / 2, (Screen.height / 2) + 100, 200, 100), "Abort"))
        {
            ActionEnd();
            thread.Abort();
        }
    }

    void Run()
    {
        ActionRun((int)colorType);
        print("thread run out\n");
        return;
    }
}