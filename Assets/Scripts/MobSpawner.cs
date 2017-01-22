using UnityEngine;
using System.Collections;
using System.Linq;

public class MobSpawner : MonoBehaviour
{
    public Dict_MobIdToPrefab[] mobs;

    public UpgradeCenter ucTeam1;
    public UpgradeCenter ucTeam2;

    public CurrenciesManager cmTeam1;
    public CurrenciesManager cmTeam2;

    public GoldCost gcTeam1;
    public GoldCost gcTeam2;

    public void CreateMob(MobEntity.e_MobId id, Vector3 pos, GameInfos.e_Team team)
    {
         if (team == GameInfos.e_Team.TEAM1)
        {
            Debug.Log("team1");
            if (cmTeam1.currencies[CurrenciesManager.e_Currencies.Gold].HasEnoughCurrency(gcTeam1.GetMobCost(id)))
            {
                cmTeam1.currencies[CurrenciesManager.e_Currencies.Gold].UseCurrency((gcTeam1.GetMobCost(id)));
                var v = Instantiate(mobs.Single(x => x.id == id).prefab, pos, Quaternion.identity) as GameObject;
                var e = v.GetComponent<MobEntity>();
                e.team = team;
                e.Level = ucTeam1.mobToLevel[id];
                cmTeam1.currencies[CurrenciesManager.e_Currencies.Income].AddCurrency(e.Income);
            }
        }
        else if (team == GameInfos.e_Team.TEAM2)
        {
            Debug.Log("team2");
            if (cmTeam2.currencies[CurrenciesManager.e_Currencies.Gold].HasEnoughCurrency(gcTeam2.GetMobCost(id)))
            {
                cmTeam2.currencies[CurrenciesManager.e_Currencies.Gold].UseCurrency((gcTeam2.GetMobCost(id)));
                var v = Instantiate(mobs.Single(x => x.id == id).prefab, pos, Quaternion.identity) as GameObject;
                var e = v.GetComponent<MobEntity>();
                e.team = team;
                e.Level = ucTeam2.mobToLevel[id];
                cmTeam2.currencies[CurrenciesManager.e_Currencies.Income].AddCurrency(e.Income);
            }
        }
    }

    [System.Serializable]
    public struct Dict_MobIdToPrefab
    {
        public MobEntity.e_MobId id;
        public GameObject prefab;
    }
}
