using UnityEngine;
using System;
using TMPro;

public class GunsCanvasManager : MonoBehaviour
{
    public static GunsCanvasManager instance;
    
    [SerializeField] private TextMeshProUGUI bullets;
    private int totalBullets;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        totalBullets = 30;
        bullets.text = totalBullets.ToString();
    }

    public void Shot()
    {
        if(totalBullets == 0) return;
        totalBullets--;
        bullets.text = totalBullets.ToString();
    }
}
