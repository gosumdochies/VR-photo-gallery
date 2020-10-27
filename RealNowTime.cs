using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealNowTime : MonoBehaviour
{
    DateTime dt;
    private Text Nowtime;

    void Awake()
    {
        Nowtime = GetComponent<Text>();
    }

    void Update()
    {
        dt = DateTime.Now;
        string hour = dt.Hour.ToString().PadRight(1, '0');
        string minute = dt.Minute.ToString().PadRight(1, '0');
        string second = dt.Second.ToString().PadRight(1, '0');
        string dayday = dt.Date.ToString().PadLeft(2, '0');
        
        Nowtime.text = hour + ":" + minute + ":" + second + "\r\n" + dayday;
    }
}
