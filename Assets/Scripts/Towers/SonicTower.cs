using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SonicTower : TowerEntity
{
    public ParticleSystem[] pss;

    List<MobEntity> _targetsInRange = new List<MobEntity>();

    private bool _isAttacking;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Mob"))
        {
            _targetsInRange.Add(col.gameObject.GetComponent<MobEntity>());
            if (!_isAttacking)
            {
                InvokeRepeating("Ivk_Attack", 0, 1 / AttackSpeed);
                _isAttacking = true;
                foreach (var v in pss)
                    v.Play();
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
                CancelInvoke("Ivk_Attack");
                _isAttacking = false;
                foreach (var v in pss)
                    v.Stop();
            }
        }
    }

    void Update()
    {
        _targetsInRange = _targetsInRange.Where(x => x != null).ToList();
        if (_targetsInRange.Count == 0)
        {
            CancelInvoke("Ivk_Attack");
            _isAttacking = false;
            foreach (var v in pss)
                v.Stop();
        }
    }

    protected override void Ivk_Attack()
    {
        foreach (var v in _targetsInRange)
        {
            v.Life -= Dommages;
        }
    }
}
