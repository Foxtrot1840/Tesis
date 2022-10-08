using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PuzzleCircle : MonoBehaviour
{
    [SerializeField] private float speed;

    public Action _onUpdate = delegate { };
    public bool _activate;
    public PuzzleManager manager;
    
    private float _rotation;
    private float t, posA, posB;
    public int snap;

    private void Start()
    {
        _activate = true;
        _rotation = Random.Range(0, 360);
        t = Random.Range(0f, 1f);
        _onUpdate += RotatePiece;
        SetPos();
    }

    private void Update()
    {
        _onUpdate();
    }

    public void RotatePiece()
    {
        t += Time.deltaTime * speed;
        _rotation = Mathf.Lerp(posA, posB,t);
        if (t >= 1)
        {
            posA += snap;
            posB += snap;
            
            t = 0;
        }
        transform.localRotation = Quaternion.Euler(0, _rotation, 0);
    }

    public void ChangeState()
    {
        _activate = !_activate;
        if (_activate) _onUpdate = RotatePiece;
        else
        {
            transform.localRotation = Quaternion.Euler(0, t < 0.5f ? posA : posB, 0);
            _onUpdate = delegate{};
            manager.Check();
        }
    }

    public void SetPos()
    {
        while (_rotation % snap != 0)
        {
            _rotation--;
        }

        posA = _rotation;
        posB = posA + snap;
    }

    public void RotateToFinal()
    {
        posA = 145;
        transform.localRotation = Quaternion.Euler(Vector3.Lerp(transform.localRotation.eulerAngles, new Vector3(0,145,0) ,speed * Time.deltaTime));
    }
}
