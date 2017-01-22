using UnityEngine;
using System.Collections;

public class XRayTowerProjectile : MonoBehaviour
{
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public float dmgs;
	
	void Update ()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        transform.LookAt(target.transform);
        transform.position = Vector3.MoveTowards(transform.position, target.position, 0.5f);
	}

    void OnTriggerEnter(Collider col)
    {
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        if (col.gameObject == target.gameObject)
        {
            Destroy(this.gameObject);
            col.GetComponent<MobEntity>().Life -= dmgs;
        }
    }
}
