using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMaterialSwapper : MonoBehaviour
{

    public SkinnedMeshRenderer renderer;
    public Material[] spiderMaterials;
    public Material[] detectionMaterials;
    private bool rendereractive;

    void Start()
    {
        renderer = GetComponent<SkinnedMeshRenderer>();
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
