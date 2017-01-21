using UnityEngine;
using System.Collections;
using System;

public class ShockWaveTower : TowerEntity
{
    int targetInRange = 0;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Mob"))
        {
            InvokeRepeating("Ivk_Attack", 0, 1 / AttackSpeed);
            targetInRange++;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Mob"))
        {
            if (--targetInRange == 0)
            {
                CancelInvoke("Ivk_Attack");
            }
        }
    }

    protected override void Ivk_Attack()
    {
        Debug.Log("j'attaque !!!");
    }
}
