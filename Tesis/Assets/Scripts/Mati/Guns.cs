using System;
using UnityEngine;

public class Guns : MonoBehaviour
{
    private float counter;
    [SerializeField] private Transform spawn;
    [SerializeField] private Camera mainCamera;
    
    float range = 100;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Mouse0)) return;
        GunsCanvasManager.instance.Shot();
        Debug.Log("Shot!");
        Aiming();
    }

    void Aiming()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(mainCamera.transform.localPosition, mainCamera.transform.forward, Color.cyan);
    }
}
