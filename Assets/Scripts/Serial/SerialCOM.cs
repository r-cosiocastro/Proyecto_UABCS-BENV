using UnityEngine;
using System.IO.Ports;

public class SerialCOM : MonoBehaviour
{
    private static SerialCOM _instance;
    private SerialPort serialPort;

    // Start is called before the first frame update
    void Start()
    {
        serialPort = new SerialPort();

        if (!_instance)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public bool AttemptConnection(string PortName)
    {
        serialPort = new SerialPort(PortName, 115200);
        if(serialPort.IsOpen){
            try
            {
                serialPort.Close();
                Debug.Log("Disconnected");
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.ToString());
            }
        }
        try
        {
            serialPort.Open();
            serialPort.ReadTimeout = 10;
            Debug.Log("Connected");
            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.ToString());
            return false;
        }
    }

    public void Disconnect()
    {
        try
        {
            serialPort.Close();
            Debug.Log("Disconnected");
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.ToString());
        }
    }

    public string Read()
    {
        string n = serialPort.ReadLine();
        return n.Trim();
    }

    public void Write(string text)
    {
        serialPort.WriteLine(text);
    }

    public bool IsConnected()
    {
        return serialPort.IsOpen;
    }
}

