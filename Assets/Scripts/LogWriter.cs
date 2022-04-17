/* AR-Based Surgery on the Human Cadaver
   Copyright (C) 2022 Mark Cesov and Behrus Puladi

   This program is free software; you can redistribute it and/or
   modify it under the terms of the GNU General Public License
   as published by the Free Software Foundation; either version 2
   of the License, or (at your option) any later version.
   
   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.
   
   You should have received a copy of the GNU General Public License
   along with this program; if not, write to the Free Software
   Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogWriter : MonoBehaviour
{
    string myLog;
    string myVersion;
    public Text text;
    public Text version;

    Queue myLogQueue = new Queue();

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;

    }

    private void Start()
    {
        //        myVersion = "";
        //#if UNITY_EDITOR
        //        myVersion += "EDITOR";
        //#endif
        //#if UNITY_WSA
        //        myVersion += ", WSA_1";
        //#endif
        //#if UNITY_WSA_10_0
        //        myVersion += ", WSA_2";
        //#endif
        //        version.text = myVersion;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        myLog = logString;
        string newString = "\n " + System.DateTime.Now + " [" + type + "] : " + myLog;
        myLogQueue.Enqueue(newString);
        if (myLogQueue.Count > 15)
            myLogQueue.Dequeue();
        if (type == LogType.Exception)
        {
            newString = "\n" + stackTrace;
            myLogQueue.Enqueue(newString);
        }
        myLog = string.Empty;
        foreach (string mylog in myLogQueue)
        {
            myLog += mylog;
        }

        text.text = myLog;
    }
}
