using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;
using System.Management;
using System.Linq;
using TMPro;
using System.Diagnostics;
using System.Text.RegularExpressions;

public class Testing : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI debugLabel;
    // Start is called before the first frame update
    void Start()
    {
        /*
        Process process = new Process();
        process.StartInfo.FileName = Application.streamingAssetsPath+"/DeviceList/ConsoleApp1.exe";
        process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
        process.Start();

        
        process.WaitForExit();
*/
        char[] delims = new[] { '\r', '\n' };
        string[] ports = TextFiles.ListDevices().Split(delims);

        debugLabel.text = TextFiles.ListDevices();
        Regex r = new Regex(@"\bCOM\d+\b");
  
            foreach (string value in ports)  
            {  
                if(r.IsMatch(value))
                UnityEngine.Debug.Log(TextFiles.GetCOMPort(value));
            }

        //UnityEngine.Debug.Log(GetStringBetweenCharacters(TextFiles.ListDevices(), '(',')'));
    }
}
