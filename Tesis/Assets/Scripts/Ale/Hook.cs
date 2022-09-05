using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] private GameObject _hook;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _hand;
    private LineRenderer _line;

    private void Start()
    {
        _hook.SetActive(false);
        _line = _hook.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (Physics.Raycast(_hand.position, _camera.transform.forward, out RaycastHit hit))
            {
                _hook.SetActive(true);
                _hook.transform.position = hit.point;
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            _hook.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        _line.SetPosition(0, _hand.position);
        _line.SetPosition(1, _hook.transform.position);
    }
}
