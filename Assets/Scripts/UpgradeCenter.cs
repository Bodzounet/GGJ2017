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

    public delegate void LevelUpMob(MobEntity.e_MobId mobID);
    public event LevelUpMob OnLevelUpMob;

    public delegate void LevelUpTower(TowerEntity.e_TowerId towerID);
    public event LevelUpTower OnLevelUpTower;

    public JoystickManager jm;
    public SwitchItemData towerHolder;
    public SwitchItemData mobHolder;

    public int playerID;

    void Awake()
    {
        foreach (var v in System.Enum.GetValues(typeof(MobEntity.e_MobId)) as MobEntity.e_MobId[])
        {
            if (v == MobEntity.e_MobId.None)
                continue;

            mobToLevel[v] = 1;
            if (OnLevelUpMob != null)
                OnLevelUpMob(v);
        }

        foreach (var v in System.Enum.GetValues(typeof(TowerEntity.e_TowerId)) as TowerEntity.e_TowerId[])
        {
            if (v == TowerEntity.e_TowerId.None)
                continue;

            towerToLevel[v] = 1;
           
        }

        _associatedGoldCost = GetComponent<GoldCost>();
    }

    void Start()
    {
        foreach (var obj in towerToLevel)
        {
            if (OnLevelUpTower != null)
                OnLevelUpTower(obj.Key); 
        }

        foreach (var obj in mobToLevel)
        {
            if (OnLevelUpMob != null)
                OnLevelUpMob(obj.Key);
        }
    }

    void Update()
    {
        if (jm.state[playerID].Buttons.X == XInputDotNetPure.ButtonState.Pressed)
        {
            UpgradeTowerLevel(towerHolder.GetCurrentTowerHolder());
        }
        if (jm.state[playerID].Buttons.Y == XInputDotNetPure.ButtonState.Pressed)
        {
            UpgradeMobLevel(mobHolder.GetCurrentMobHolder());
        }
    }


    public bool UpgradeMobLevel(MobEntity.e_MobId id)
    {
        if (associatedCurrenciesManager.currencies[CurrenciesManager.e_Currencies.Gold].HasEnoughCurrency(_associatedGoldCost.GetMobUpgradeCost(id)))
        {
            associatedCurrenciesManager.currencies[CurrenciesManager.e_Currencies.Gold].UseCurrency(_associatedGoldCost.GetMobUpgradeCost(id));
            mobToLevel[id]++;
            if (OnLevelUpMob != null)
                OnLevelUpMob(id);
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
            if (OnLevelUpTower != null)
                OnLevelUpTower(id);
            foreach (var v in FindObjectsOfType<TowerEntity>().Where(x => x.team == team))
            {
                v.Level = towerToLevel[id];
            }

            return true;
        }
        return false;
    }
}
