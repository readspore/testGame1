using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BtnAvailableAction
{
    ShowCamera
}

public class BtnMaster : MonoBehaviour
{
    public int clickedCounter;
    public GameObject btnSlave;
    //public GameObject btnSlave2;
    //public GameObject btnSlave3;
    Camera m_MainCamera;
    public Camera _camera;
    bool _cameraActive;
    bool playerInside = false;
    // Start is called before the first frame update
    void Start()
    {
        m_MainCamera = Camera.main;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != Constants.PlayerLayer)
            return;
        //Debug.Log("collision " + collision.gameObject.name);
        //playerInside = true;
        Radio.Radio.ToggleAvailableAction(BtnAvailableAction.ShowCamera);
        Radio.Radio.onSwipeDown += OnSwipeDown;
        Radio.Radio.onToggleBtnCameraView += ToggleBtnCameraViewHandler;
        //OnCollisionExit
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer != Constants.PlayerLayer)
            return;
        Radio.Radio.ToggleAvailableAction(BtnAvailableAction.ShowCamera);
        ShowMainCamera();
        Radio.Radio.onSwipeDown -= OnSwipeDown;
        Radio.Radio.onToggleBtnCameraView -= ToggleBtnCameraViewHandler;
    }

    void OnSwipeDown()
    {
        btnSlave.GetComponent<IBTNSlave>().ExecAction();
        PlatformHelpers.IgnorePlayerPlatform(true, "OnSwipeDown btn master", false);
    }

    void ToggleBtnCameraViewHandler()
    {

        if (m_MainCamera.enabled)
        {
            ShowBtnCamera();
        }
        else
        {
            ShowMainCamera();
        }
    }

    void ShowBtnCamera()
    {
        _camera.enabled = true;
        m_MainCamera.enabled = false;
    }

    void ShowMainCamera()
    {
        _camera.enabled = false;
        m_MainCamera.enabled = true;
    }
}
