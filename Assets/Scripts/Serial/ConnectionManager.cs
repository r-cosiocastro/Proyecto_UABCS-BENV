using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionManager : MonoBehaviour
{
    [SerializeField] SerialCOM serialPort;
    [SerializeField] TextMeshProUGUI connectionLabel;
    [SerializeField] TextMeshProUGUI buttonLabel;
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] Image connectedImage;
    [SerializeField] Sprite connectedSprite;
    [SerializeField] Sprite disconnectedSprite;
    [SerializeField] UISoundManager _UISoundManager;

    public void Connect()
    {
        if (!serialPort.IsConnected())
        {
            if (serialPort.AttemptConnection(TextFiles.GetCOMPort(dropdown.options[dropdown.value].text)))
            {
                connectionLabel.text = "Conectado";
                connectionLabel.color = Color.green;
                buttonLabel.text = "Desconectar";
                connectedImage.sprite = connectedSprite;
                dropdown.enabled = false;
                _UISoundManager.PlayConnectedSound();
            }
            else
            {
                connectionLabel.text = "Error";
                connectionLabel.color = Color.red;
                connectedImage.sprite = disconnectedSprite;
                dropdown.enabled = true;
                _UISoundManager.PlayDisconnectedSound();
            }
        }
        else
        {
            serialPort.Disconnect();
            connectionLabel.text = "Desconectado";
            connectionLabel.color = Color.gray;
            buttonLabel.text = "Conectar";
            connectedImage.sprite = disconnectedSprite;
            dropdown.enabled = true;
            _UISoundManager.PlayDisconnectedSound();
        }
    }
}
