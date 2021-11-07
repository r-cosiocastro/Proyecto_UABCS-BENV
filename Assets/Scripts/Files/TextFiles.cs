using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TextFiles : MonoBehaviour
{
    public static string ListDevices()
   {
       string text;
       try{
       string path = Application.streamingAssetsPath + "/DeviceList/Devices.txt";
       //Read the text from directly from the test.txt file
       StreamReader reader = new StreamReader(path);
       text = reader.ReadToEnd();
       //Debug.Log(text);
       reader.Close();
       }catch(Exception e){
           text = e.Message;
       }
       return text;
   }

   public static string GetCOMPort(string input)
    {
        int posFrom = input.IndexOf('(');
        if (posFrom != -1) //if found char
        {
            int posTo = input.IndexOf(')', posFrom + 1);
            if (posTo != -1) //if found char
            {
                return input.Substring(posFrom + 1, posTo - posFrom - 1);
            }
        }

        return string.Empty;
    }
}
