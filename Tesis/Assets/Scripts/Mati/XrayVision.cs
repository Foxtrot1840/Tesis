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
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isStacked = !isStacked;
            StackCamera();
        }
    }

    private void StackCamera()
    {
        if (isStacked) stackData.cameraStack.Add(overlayCamera);
        else stackData.cameraStack.Remove(overlayCamera);
    }
    
}
