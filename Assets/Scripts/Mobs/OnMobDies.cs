using UnityEngine;
using System.Collections;

public class OnMobDies : MonoBehaviour
{
    public ParticleSystem[] alive;
    public ParticleSystem dead;

    public int worthRegular;
    public int worthSpeeder;
    public int worthTanker;

    public void OnDeath()
    {
        foreach (var v in alive)
            v.Stop();
        dead.Play();
        Destroy(this.gameObject, 1.25f);
        var team = this.GetComponent<MobEntity>().team;
        if (team == GameInfos.e_Team.TEAM1)
        {
            if (this.GetComponent<MobEntity>().id == MobEntity.e_MobId.SOLDIER)
            {
                GameObject.Find("UI Player 2").GetComponentInChildren<CurrenciesManager>().currencies[CurrenciesManager.e_Currencies.Gold].AddCurrency(worthRegular);
            }
            if (this.GetComponent<MobEntity>().id == MobEntity.e_MobId.SPEEDER)
            {
                GameObject.Find("UI Player 2").GetComponentInChildren<CurrenciesManager>().currencies[CurrenciesManager.e_Currencies.Gold].AddCurrency(worthSpeeder);
            }
            if (this.GetComponent<MobEntity>().id == MobEntity.e_MobId.TANKER)
            {
                GameObject.Find("UI Player 2").GetComponentInChildren<CurrenciesManager>().currencies[CurrenciesManager.e_Currencies.Gold].AddCurrency(worthTanker);
            }
        }
        if (team == GameInfos.e_Team.TEAM2)
        {
            if (this.GetComponent<MobEntity>().id == MobEntity.e_MobId.SOLDIER)
            {
                GameObject.Find("UI Player 1").GetComponentInChildren<CurrenciesManager>().currencies[CurrenciesManager.e_Currencies.Gold].AddCurrency(worthRegular);
            }
            if (this.GetComponent<MobEntity>().id == MobEntity.e_MobId.SPEEDER)
            {
                GameObject.Find("UI Player 1").GetComponentInChildren<CurrenciesManager>().currencies[CurrenciesManager.e_Currencies.Gold].AddCurrency(worthSpeeder);
            }
            if (this.GetComponent<MobEntity>().id == MobEntity.e_MobId.TANKER)
            {
                GameObject.Find("UI Player 1").GetComponentInChildren<CurrenciesManager>().currencies[CurrenciesManager.e_Currencies.Gold].AddCurrency(worthTanker);
            }
        }
    }
}
