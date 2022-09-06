using UnityEngine;
using UnityEngine.Rendering.Universal;

public class XrayVision : MonoBehaviour
{
    private Camera mainCamera;
    public Camera overlayCamera;
    private UniversalAdditionalCameraData stackData;
    private bool isStacked;

    private void Start()
    {
        mainCamera = Camera.main;
        stackData = mainCamera.GetUniversalAdditionalCameraData();
        isStacked = false;
        CheckCameraPos();
    }


    private void Update()
    {
        CheckCameraPos();
        
        if (!Input.GetKeyDown(KeyCode.Q)) return;
        
        isStacked = !isStacked;
        StackCamera();
    }

    private void StackCamera()
    {
        if (isStacked) stackData.cameraStack.Add(overlayCamera);
        else stackData.cameraStack.Remove(overlayCamera);
    }

    void CheckCameraPos()
    {
        overlayCamera.fieldOfView = mainCamera.fieldOfView;
    }
}
