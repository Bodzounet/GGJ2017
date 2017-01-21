using UnityEngine;
using System.Collections;

public abstract class TowerEntity : MonoBehaviour
{
    public enum e_TowerId
    {
        XRAY,
        SONIC,
        SHOCK,

        None
    }

    public GameInfos.e_Team team = GameInfos.e_Team.TEAM1;

    [SerializeField]
    private float _baseDommages;
    public float BaseDommages
    {
        get { return _baseDommages; }
    }

    private float _dommages;
    public float Dommages
    {
        get { return _dommages; }
        set
        {
            _dommages = value;
        }
    }

    [SerializeField]
    private float _baseAttackSpeed;
    public float BaseAttackSpeed
    {
        get { return _baseAttackSpeed; }
    }

    private float _attackSpeed;
    public float AttackSpeed
    {
        get { return _attackSpeed; }
        set
        {
            _attackSpeed = value;
        }
    }

    [SerializeField]
    private int _baseGoldCost;
    public int BaseGoldCost
    {
        get { return _baseGoldCost; }
    }

    private int _goldCost;
    public int GoldCost
    {
        get { return _goldCost; }
        set
        {
            _goldCost = value;
        }
    }

    [Range(0, 100)]
    [SerializeField]
    protected int _bonusPercentagePerNextLevel;

    private int _level = 0;
    public virtual int Level
    {
        get { return _level; }
        protected set
        {
            _level = value;
            AttackSpeed = BaseAttackSpeed * (1 + _bonusPercentagePerNextLevel * (_level - 1) / 100.0f); 
            Dommages = BaseDommages * (1 + _bonusPercentagePerNextLevel * (_level - 1) / 100.0f);
            GoldCost = Mathf.FloorToInt(BaseGoldCost * (1 + _bonusPercentagePerNextLevel * (_level - 1) / 100.0f));
        }
    }

    public void UpdateLevel()
    {
        Level++;
    }

    protected virtual void Start()
    {
        Level = 1;
    }

    protected abstract void Ivk_Attack();
}
