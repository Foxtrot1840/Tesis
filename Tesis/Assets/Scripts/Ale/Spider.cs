using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.UI;

public class Spider : Entity
{
    private NavMeshAgent _navMesh;
    private Transform _player;
    private Animator _anim;
    public Material[] dissolveMaterials;
    
    public float _range;
        
    [SerializeField] private float _rangeAttack;
    [SerializeField] private int _maxHealth;
    [SerializeField] private float cooldown;
    [SerializeField] private int damage;

    private bool _isAttack;
    private float _lerpDissolve = 0.3f;
    
    private void Start()
    {
        currentHealth = _maxHealth;
        _player = GameManager.instance.player.transform;
        _navMesh = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position,_player.position) < _rangeAttack)
        {
            if (!Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _rangeAttack))
            {
                Quaternion rotation = Quaternion.LookRotation(_player.position - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 5 * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            }
            else
            {
                _navMesh.destination = transform.position;
                if (!_isAttack)
                {
                    _isAttack = true;
                    StartCoroutine(CorrutineAttack());
                }
            }
        }
        else if (Vector3.Distance(transform.position,_player.position) < _range)
        {
            _navMesh.destination = _player.position;
        }
    }
    
    IEnumerator CorrutineAttack()
    {
        _anim.SetTrigger("Attack");
        yield return new WaitForSeconds(cooldown);
        _isAttack = false;
    }

    public void Attack()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _rangeAttack))
        {
            IDamagable dmg = hit.collider.GetComponent<IDamagable>();
            if(dmg != null) dmg.GetDamage(damage);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _range);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _rangeAttack);
    }

    public override void GetDamage(int damage, Vector3 point, Vector3 normal)
    {
        SoundManager.instance.Play(SoundID.Spider);
        base.GetDamage(damage, point, normal);
    }

    public override void Die()
    {
        GetComponentInChildren<Renderer>().materials = dissolveMaterials;
        Material[] materials = GetComponentInChildren<Renderer>().materials;
        _navMesh.enabled = false;
        _anim.enabled = false;
        StartCoroutine(DissolveSpider(materials));
    }

    IEnumerator DissolveSpider(Material[] materials)
    {
        while (_lerpDissolve > -1)
        {
            foreach (var mat in materials)
            {
                mat.SetFloat("_alphaDissolve",_lerpDissolve);
            }
            _lerpDissolve -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
    }
}
