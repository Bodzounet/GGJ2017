using UnityEngine;
using System.Collections;

public class DestroyAI : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        Destroy(col.gameObject);
    }
}
