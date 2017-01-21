using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(UpgradeCenter))]
public class GoldCost : MonoBehaviour
{
    public GameInfos.e_Team team;

    [Range(0, 100)]
    public int percentageAdditiveCostPerLevel = 5;

    public Dict_MobToGold[] mobToGoldWorth;
    public Dict_TowerToGold[] towerToGoldWorth;

    private UpgradeCenter _associatedUpgradeCenter;

    void Start()
    {
        _associatedUpgradeCenter = GetComponent<UpgradeCenter>();
    }

    public int GetMobCost(MobEntity.e_MobId id)
    {
        return Mathf.CeilToInt(((1 + percentageAdditiveCostPerLevel * (_associatedUpgradeCenter.mobToLevel[id] - 1) / 100.0f)) * mobToGoldWorth.Single(x => x.id == id).worth);
    }

    public int GetTowerCost(TowerEntity.e_TowerId id)
    {
        return Mathf.CeilToInt(((1 + percentageAdditiveCostPerLevel * (_associatedUpgradeCenter.towerToLevel[id] - 1) / 100.0f)) * towerToGoldWorth.Single(x => x.id == id).worth);
    }

    public struct Dict_MobToGold
    {
        public MobEntity.e_MobId id;
        public int worth;
    }

    public struct Dict_TowerToGold
    {
        public TowerEntity.e_TowerId id;
        public int worth;
    }
}
