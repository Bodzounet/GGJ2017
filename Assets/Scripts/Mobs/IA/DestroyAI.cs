using UnityEngine;
using System.Collections;

public class DestroyAI : MonoBehaviour
{

    public CurrenciesManager cm;
    public int playerID;

    bool gameEnded = false;
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Mob"))
        {
            col.GetComponent<MobEntity>().Life = 0;
            cm.currencies[CurrenciesManager.e_Currencies.Life].UseCurrency(1);
            if (!gameEnded)
            {
                if (cm.currencies[CurrenciesManager.e_Currencies.Life].HasEnoughCurrency(1))
                {
                    cm.jm.LaunchVib(playerID, 0.2f);
                }
                else
                {
                    cm.jm.LaunchVib(playerID, 1f);
                    gameEnded = true;
                }
            }
        }
    }
}
