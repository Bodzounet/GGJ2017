using UnityEngine;
using System.Collections;

public class DestroyAI : MonoBehaviour
{

    public CurrenciesManager cm;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Mob"))
        {
            col.GetComponent<MobEntity>().Life = 0;
            cm.currencies[CurrenciesManager.e_Currencies.Life].UseCurrency(1);
        }
    }
}
