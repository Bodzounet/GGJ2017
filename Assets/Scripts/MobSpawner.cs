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

    public void CreateMob(MobEntity.e_MobId id, Vector3 pos, GameInfos.e_Team team)
    {
        var v = Instantiate(mobs.Single(x => x.id == id).prefab, pos, Quaternion.identity) as GameObject;
        var e = v.GetComponent<MobEntity>();
        e.team = team;
        if (team == GameInfos.e_Team.TEAM1)
        {
            cmTeam1.currencies[CurrenciesManager.e_Currencies.Gold].AddCurrency(e.Income);
            e.Level = ucTeam1.mobToLevel[id];
        }
        else
        {
            cmTeam2.currencies[CurrenciesManager.e_Currencies.Gold].AddCurrency(e.Income);
            e.Level = ucTeam2.mobToLevel[id];
        }
    }

    public Transform pos;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CreateMob(MobEntity.e_MobId.TANKER, pos.position, GameInfos.e_Team.TEAM1);
        }
    }

    [System.Serializable]
    public struct Dict_MobIdToPrefab
    {
        public MobEntity.e_MobId id;
        public GameObject prefab;
    }
}
