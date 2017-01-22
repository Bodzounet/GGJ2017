using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class XRayTower : TowerEntity
{
    private bool _isFighting = false;
    private List<GameObject> _targets = new List<GameObject>();

    public GameObject projectile;
    public Transform projectileSpawnPos;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Mob"))
        {
            _targets.Add(col.gameObject);
            if (!_isFighting)
            {
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
        var proj = GameObject.Instantiate(projectile, projectileSpawnPos.position, Quaternion.identity) as GameObject;
        var projScript = proj.GetComponent<XRayTowerProjectile>();

        _targets = _targets.Where(x => x != null).ToList();
        if (_targets.Count == 0)
            return;

        projScript.target = _targets[0].transform;
        projScript.dmgs = Dommages;
        
    }
}
