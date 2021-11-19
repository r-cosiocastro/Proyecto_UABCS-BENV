using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera mainVCamera, deviceCamera;
    // Start is called before the first frame update
    public void ChangeToDeviceCamera(){
        deviceCamera.Priority = mainVCamera.Priority + 1;
        mainVCamera.Priority--;
    }

    public void ChangeToMainCamera(){
        mainVCamera.Priority = deviceCamera.Priority + 1;
        deviceCamera.Priority--;
    }
}
