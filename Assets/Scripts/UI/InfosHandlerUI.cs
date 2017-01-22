using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfosHandlerUI : MonoBehaviour
{

    // Use this for initialization

    public UpgradeCenter upteam;
    public GoldCost gcteam;

    public GameObject prefabSonic;
    public GameObject prefabRayX;
    public GameObject prefabShockwave;

    public Text[] infosSonic;
    public Text[] infosRayX;
    public Text[] infosShockwave;

    public GameObject prefabRegular;
    public GameObject prefabSpeeder;
    public GameObject prefabTanker;

    public Text[] infosRegular;
    public Text[] infosSpeeder;
    public Text[] infosTanker;

    void Start()
    {
        upteam.OnLevelUpMob += UpdateMobInfosTeam;
        upteam.OnLevelUpTower += UpdateTowerInfosTeam;

        UpdateMobInfosTeam(MobEntity.e_MobId.SOLDIER);
        UpdateMobInfosTeam(MobEntity.e_MobId.SPEEDER);
        UpdateMobInfosTeam(MobEntity.e_MobId.TANKER);

        UpdateTowerInfosTeam(TowerEntity.e_TowerId.SHOCK);
        UpdateTowerInfosTeam(TowerEntity.e_TowerId.SONIC);
        UpdateTowerInfosTeam(TowerEntity.e_TowerId.XRAY);
    }

    // Mathf.FloorToInt().ToString();
    void UpdateMobInfosTeam(MobEntity.e_MobId mobID)
    {
        int level = upteam.mobToLevel[mobID];
        if (mobID == MobEntity.e_MobId.SOLDIER)
        {
            MobEntity tmp = prefabRegular.GetComponentInChildren<MobEntity>();
            infosRegular[0].text = Mathf.FloorToInt(tmp.BaseLife * (1 + tmp._bonusPercentagePerNextLevel * (level - 1) / 100.0f)).ToString();
            infosRegular[1].text = gcteam.GetMobCost(MobEntity.e_MobId.SOLDIER).ToString();
            infosRegular[2].text = Mathf.FloorToInt(tmp.BaseSpeed * (1 + tmp._bonusPercentagePerNextLevel * (level - 1) / 100.0f)).ToString();
            infosRegular[3].text = tmp.Income.ToString();
            infosRegular[4].text = "Level " + level;
            infosRegular[5].text = gcteam.GetMobUpgradeCost(MobEntity.e_MobId.SOLDIER).ToString();
        }
        if (mobID == MobEntity.e_MobId.TANKER)
        {
            MobEntity tmp = prefabTanker.GetComponentInChildren<MobEntity>();
            infosTanker[0].text = Mathf.FloorToInt(tmp.BaseLife * (1 + tmp._bonusPercentagePerNextLevel * (level - 1) / 100.0f)).ToString();
            infosTanker[1].text = gcteam.GetMobCost(MobEntity.e_MobId.TANKER).ToString();
            infosTanker[2].text = Mathf.FloorToInt(tmp.BaseSpeed * (1 + tmp._bonusPercentagePerNextLevel * (level - 1) / 100.0f)).ToString();
            infosTanker[3].text = tmp.Income.ToString();
            infosTanker[4].text = "Level " + level;
            infosTanker[5].text = gcteam.GetMobUpgradeCost(MobEntity.e_MobId.TANKER).ToString();
        }
        if (mobID == MobEntity.e_MobId.SPEEDER)
        {
            MobEntity tmp = prefabSpeeder.GetComponentInChildren<MobEntity>();
            infosSpeeder[0].text = Mathf.FloorToInt(tmp.BaseLife * (1 + tmp._bonusPercentagePerNextLevel * (level - 1) / 100.0f)).ToString();
            infosSpeeder[1].text = gcteam.GetMobCost(MobEntity.e_MobId.SPEEDER).ToString();
            infosSpeeder[2].text = Mathf.FloorToInt(tmp.BaseSpeed * (1 + tmp._bonusPercentagePerNextLevel * (level - 1) / 100.0f)).ToString();
            infosSpeeder[3].text = tmp.Income.ToString();
            infosSpeeder[4].text = "Level " + level;
            infosSpeeder[5].text = gcteam.GetMobUpgradeCost(MobEntity.e_MobId.SPEEDER).ToString();
        }
    }
    void UpdateTowerInfosTeam(TowerEntity.e_TowerId towerID)
    {
        int level = upteam.towerToLevel[towerID];
        if (towerID == TowerEntity.e_TowerId.SONIC)
        {
            TowerEntity tmp = prefabSonic.GetComponentInChildren<TowerEntity>();
            infosSonic[0].text = Mathf.FloorToInt(tmp.BaseDommages * (1 + tmp._bonusPercentagePerNextLevel * (level - 1) / 100.0f)).ToString();
            infosSonic[1].text = gcteam.GetTowerCost(TowerEntity.e_TowerId.SONIC).ToString();
            infosSonic[2].text = Mathf.FloorToInt(tmp.BaseAttackSpeed * (1 + tmp._bonusPercentagePerNextLevel * (level - 1) / 100.0f)).ToString();
            infosSonic[3].text = "Level " + level;
            infosSonic[4].text = gcteam.GetTowerUpgradeCost(TowerEntity.e_TowerId.SONIC).ToString();
        }
        if (towerID == TowerEntity.e_TowerId.XRAY)
        {
            TowerEntity tmp = prefabRayX.GetComponentInChildren<TowerEntity>();
            infosRayX[0].text = Mathf.FloorToInt(tmp.BaseDommages * (1 + tmp._bonusPercentagePerNextLevel * (level - 1) / 100.0f)).ToString();
            infosRayX[1].text = gcteam.GetTowerCost(TowerEntity.e_TowerId.XRAY).ToString();
            infosRayX[2].text = Mathf.FloorToInt(tmp.BaseAttackSpeed * (1 + tmp._bonusPercentagePerNextLevel * (level - 1) / 100.0f)).ToString();
            infosRayX[3].text = "Level " + level;
            infosRayX[4].text = gcteam.GetTowerUpgradeCost(TowerEntity.e_TowerId.XRAY).ToString();
        }
        if (towerID == TowerEntity.e_TowerId.SHOCK)
        {
            TowerEntity tmp = prefabShockwave.GetComponentInChildren<TowerEntity>();
            infosShockwave[0].text = Mathf.FloorToInt(tmp.BaseDommages * (1 + tmp._bonusPercentagePerNextLevel * (level - 1) / 100.0f)).ToString();
            infosShockwave[1].text = gcteam.GetTowerCost(TowerEntity.e_TowerId.SHOCK).ToString();
            infosShockwave[2].text = Mathf.FloorToInt(tmp.BaseAttackSpeed * (1 + tmp._bonusPercentagePerNextLevel * (level - 1) / 100.0f)).ToString();
            infosShockwave[3].text = "Level " + level;
            infosShockwave[4].text = gcteam.GetTowerUpgradeCost(TowerEntity.e_TowerId.SHOCK).ToString();
        }
    }
}
