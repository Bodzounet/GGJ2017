using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ShockWaveTower : TowerEntity
{
    List<MobEntity> _targetsInRange = new List<MobEntity>();
    Animator _anim;
    public ParticleSystem ps;
    bool _isAttacking = false;

    public float slowDebuffTime = 2;
    public float slowDebuffPercentage = 0.33f;

    void Awake()
    {
        _anim = transform.root.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Mob"))
        {
            _targetsInRange.Add(col.gameObject.GetComponent<MobEntity>());
            if (!_isAttacking)
            {
                InvokeRepeating("Ivk_Attack", 0, 1 / AttackSpeed);
                _isAttacking = true;
                _anim.SetBool("IsAttacking", true);
                ps.Play();
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Mob"))
        {
            _targetsInRange.Remove(col.gameObject.GetComponent<MobEntity>());
            if (_targetsInRange.Count == 0)
            {
                _anim.SetBool("IsAttacking", false);
                ps.Stop();
                CancelInvoke("Ivk_Attack");
                _isAttacking = false;
            }
        }
    }

    protected override void Ivk_Attack()
    {
        foreach (var v in _targetsInRange)
        {
            v.Life -= Dommages;
            v.SetSlowDebuff(slowDebuffTime, slowDebuffPercentage);
        }
    }
}
