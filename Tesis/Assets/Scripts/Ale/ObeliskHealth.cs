using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeliskHealth : MonoBehaviour
{
    [SerializeField] private float cooldown = 5;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != GameManager.instance.player) return;
        StartCoroutine(GiveLife());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != GameManager.instance.player) return;
        StopAllCoroutines();
    }

    IEnumerator GiveLife()
    {
        while (true)
        {
            yield return  new WaitForSeconds(cooldown);
            GameManager.instance.player.GetComponent<Controller>().GetLife(1);
        }
    }
}
