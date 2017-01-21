using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class XRayTower : TowerEntity
{
    private bool _isFighting = false;
    private List<GameObject> _targets = new List<GameObject>();

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Mob"))
        {
            _targets.Add(col.gameObject);
            if (!_isFighting)
            {
                Debug.Log(1 / AttackSpeed);
                InvokeRepeating("Ivk_Attack", 0, 1 / AttackSpeed);
                _isFighting = true;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Mob"))
        {
            _targets.Remove(col.gameObject);
            if (_targets.Count == 0)
            {
                CancelInvoke("Ivk_Attack");
                _isFighting = false;
            }
        }
    }

    protected override void Ivk_Attack()
    {
      //  Debug.Log("j'attaque");
    }
}
