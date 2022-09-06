using UnityEngine;

public class MaterialSwapper : MonoBehaviour
{
    public Material[] materials;
    public Renderer renderer;
    private bool rendererActive;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        rendererActive = true;
        if (renderer.material == null || rendererActive)
            renderer.material = materials[0];
    }

    // Update is called once per frame
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Q)) return;

        rendererActive = !rendererActive;
        renderer.material = rendererActive ? materials[0] : materials[1];
    }

}
