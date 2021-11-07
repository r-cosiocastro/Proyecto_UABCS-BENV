using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO.Ports;
using System;
using TMPro;
using System.Text.RegularExpressions;

public class DropdownSerialPort : MonoBehaviour
{
    TMP_Dropdown m_Dropdown;
    [SerializeField] Sprite itemImage;
    void Start()
    {
        ListCOMPorts();
    }

    public void ListCOMPorts()
    {
        //Fetch the Dropdown GameObject the script is attached to
        m_Dropdown = GetComponent<TMP_Dropdown>();
        //Clear the old options of the Dropdown menu
        m_Dropdown.ClearOptions();

        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        /*
        foreach (string port in SerialPort.GetPortNames())
                {
                    //SerialEvent serialEvent = new SerialEvent();
                    //serialEvent.AddListener(ChangeCOMPort);
                    //m_Dropdown.AddOptions(new TMP_Dropdown.OptionData(port, USBIcon));

                    options.Add(new TMP_Dropdown.OptionData(port));
                }
                */
            char[] delims = new[] { '\r', '\n' };
        string[] ports = TextFiles.ListDevices().Split(delims);

        Regex r = new Regex(@"\bCOM\d+\b");
  
            foreach (string value in ports)  
            {  
                if(r.IsMatch(value))
                options.Add(new TMP_Dropdown.OptionData(value, itemImage));
                //UnityEngine.Debug.Log(TextFiles.GetCOMPort(value));
            }

            

                m_Dropdown.AddOptions(options);
        }


}
