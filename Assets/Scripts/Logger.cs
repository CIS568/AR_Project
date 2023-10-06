using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public class Logger : MonoBehaviour
    {
        uint qsize = 15;  // number of messages to keep
        Queue myLogQueue = new();

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Application.logMessageReceived += HandleLog;
        }
        private void OnDestroy()
        {
            Application.logMessageReceived -= HandleLog;
        }
        void HandleLog(string logString, string stackTrace, LogType type)
        {
            myLogQueue.Enqueue("[" + type + "] : " + logString);
            if (type == LogType.Exception)
                myLogQueue.Enqueue(stackTrace);
            while (myLogQueue.Count > qsize)
                myLogQueue.Dequeue();
        }

        void OnGUI()
        {
            GUIStyle style = new()
            {
                fontSize = 15,
                normal =
                {
                    textColor = Color.green
                }
            };
            GUILayout.BeginArea(new Rect(Screen.width - 400, 0, 400, Screen.height));
            GUILayout.Label("\n" + string.Join("\n", myLogQueue.ToArray()), style);
            GUILayout.EndArea();
        }
    }
}
