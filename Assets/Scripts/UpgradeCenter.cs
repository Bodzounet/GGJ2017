using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GoldCost))]
public class UpgradeCenter : MonoBehaviour
{
    public GameInfos.e_Team team;

    public Dictionary<MobEntity.e_MobId, int> mobToLevel = new Dictionary<MobEntity.e_MobId, int>();
    public Dictionary<TowerEntity.e_TowerId, int> towerToLevel = new Dictionary<TowerEntity.e_TowerId, int>();

    void Awake()
    {
        foreach (var v in System.Enum.GetValues(typeof(MobEntity.e_MobId)) as MobEntity.e_MobId[])
        {
            if (v == MobEntity.e_MobId.None)
                continue;

            mobToLevel[v] = 1;
        }

        foreach (var v in System.Enum.GetValues(typeof(TowerEntity.e_TowerId)) as TowerEntity.e_TowerId[])
        {
            if (v == TowerEntity.e_TowerId.None)
                continue;

            towerToLevel[v] = 1;
        }
    }

    public void UpgradeMobLevel(MobEntity.e_MobId id)
    {

    }

    public void UpgradeTowerLevel(TowerEntity.e_TowerId id)
    {

    }
}
