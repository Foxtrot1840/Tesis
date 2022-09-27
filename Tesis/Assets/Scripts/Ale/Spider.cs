using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Spider : Entity
{
    private NavMeshAgent _navMesh;
    private Transform _player;
    private Renderer _renderer; 
    
    public float _range;
        
    [SerializeField] private float _rangeAttack;
    [SerializeField] private int _maxHealth;
    [SerializeField] private float cooldown;
    [SerializeField] private int damage;

    private bool _isAttack;
    
    private void Start()
    {
        currentHealth = _maxHealth;
        _player = GameManager.instance._player.transform;
        _navMesh = GetComponent<NavMeshAgent>();
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position,_player.position) < _rangeAttack)
        {
            _navMesh.destination = transform.position;
            if (!_isAttack)
            {
                _isAttack = true;
                StartCoroutine(Attack());
            }

            if (!Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _rangeAttack))
            {
                Debug.Log("Rote");
                    Quaternion rotation = Quaternion.LookRotation(_player.position - transform.position);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 5 * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            }
        }
        else if (Vector3.Distance(transform.position,_player.position) < _range)
        {
            _navMesh.destination = _player.position;
        }
    }
    
    IEnumerator Attack()
    {
        _renderer.material.color = Color.red;
        yield return new WaitForSeconds(.2f);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _rangeAttack))
        {
            IDamagable dmg = hit.collider.GetComponent<IDamagable>();
            if(dmg != null) dmg.GetDamage(damage);
        }
        _renderer.material.color = Color.white;
        yield return new WaitForSeconds(cooldown);
        _isAttack = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _range);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _rangeAttack);
    }

    public override void GetDamage(int damage)
    {
        SoundManager.instance.Play(SoundID.Spider);
        base.GetDamage(damage);
    }
}
