using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(GoldCost))]
public class UpgradeCenter : MonoBehaviour
{
    private GoldCost _associatedGoldCost;

    public CurrenciesManager associatedCurrenciesManager;

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

        _associatedGoldCost = GetComponent<GoldCost>();
    }

    public bool UpgradeMobLevel(MobEntity.e_MobId id)
    {
        if (associatedCurrenciesManager.currencies[CurrenciesManager.e_Currencies.Gold].HasEnoughCurrency(_associatedGoldCost.GetMobUpgradeCost(id)))
        {
            associatedCurrenciesManager.currencies[CurrenciesManager.e_Currencies.Gold].UseCurrency(_associatedGoldCost.GetMobUpgradeCost(id));
            mobToLevel[id]++;
            return true;
        }
        return false;
    }

    public bool UpgradeTowerLevel(TowerEntity.e_TowerId id)
    {
        if (associatedCurrenciesManager.currencies[CurrenciesManager.e_Currencies.Gold].HasEnoughCurrency(_associatedGoldCost.GetTowerUpgradeCost(id)))
        {
            associatedCurrenciesManager.currencies[CurrenciesManager.e_Currencies.Gold].UseCurrency(_associatedGoldCost.GetTowerUpgradeCost(id));
            towerToLevel[id]++;

            foreach (var v in FindObjectsOfType<TowerEntity>().Where(x => x.team == team))
            {
                Debug.Log("plop");
                v.Level = towerToLevel[id];
            }

            return true;
        }
        return false;
    }
}
