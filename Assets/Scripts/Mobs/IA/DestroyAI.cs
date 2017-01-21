using UnityEngine;
using System.Collections;

public class DestroyAI : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Mob"))
        {
            col.GetComponent<MobEntity>().Life = 0;
        }
    }
}
