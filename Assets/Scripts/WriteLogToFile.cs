using System;
using System.IO;
using UnityEngine;

namespace Diwide.Arkanoid
{
    public class WriteLogToFile : MonoBehaviour
    {
        private string _filepath = "";

        private void Awake()
        {
            _filepath = Application.dataPath + "/LogFile.log";
        }

        private void OnEnable()
        {
            Application.logMessageReceived += Log;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= Log;
        }

        public void Log(string logString, string backtrace, LogType logType)
        {
            TextWriter tw = new StreamWriter(_filepath, true);
            tw.WriteLine("[" + System.DateTime.Now + "] " + logString);
            tw.Close();
        }
    }
}