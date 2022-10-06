using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMaterialSwapper : MonoBehaviour
{

    public MeshRenderer renderer;
    public Material[] spiderMaterials;
    public Material[] detectionMaterials;
    private bool rendereractive;

    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        rendereractive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Q)) return;

        rendereractive = !rendereractive;

        renderer.materials = rendereractive ? spiderMaterials : detectionMaterials;
    }
}
