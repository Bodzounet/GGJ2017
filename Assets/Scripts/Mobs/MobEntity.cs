using UnityEngine;
using System.Collections;

public class MobEntity : MonoBehaviour {

    // Use this for initialization
    public enum e_MobId
    {
        SPEEDER,
        SOLDIER,
        TANKER,
        FLYER,

        None
    }

    [SerializeField]
    private float _baseLife;
    public float BaseLife
    {
        get { return _baseLife; }
    }

    private float _life;
    public float Life
    {
        get { return _life; }
        set
        {
            _life = value;
            if (_life == 0)
            {
                this.GetComponent<OnMobDies>().OnDeath();
                Destroy(GetComponent<RegularAI>());
                Destroy(GetComponent<NavMeshAgent>());
            }
        }
    }

    [SerializeField]
    private float _baseSpeed;
    public float BaseSpeed
    {
        get { return _baseSpeed; }
    }

    private float _speed;
    public float Speed
    {
        get { return _speed; }
        set
        {
            _speed = value;
            GetComponent<NavMeshAgent>().speed = _speed;
        }
    }

    [SerializeField]
    private int _income;
    public int Income
    {
        get { return _income; }
    }

    [Range(0, 100)]
    [SerializeField]
    protected int _bonusPercentagePerNextLevel;

    public e_MobId id;
    public GameInfos.e_Team team;

    private bool _isSlow = false;
    public void SetSlowDebuff(float time, float percentage)
    {
        if (!_isSlow)
        {
            _isSlow = true;
            Speed = Speed * percentage;
            Invoke("Ivk_SlowDebuff", time);
        }
        else
        {
            CancelInvoke("Ivk_SlowDebuff");
            Invoke("Ivk_SlowDebuff", time);
        }
    }

    private int _level = 0;
    public int Level
    {
        get { return _level; }
        set
        {
            _level = value;

            Life = BaseLife * (1 + _bonusPercentagePerNextLevel * (_level - 1) / 100.0f);
            Speed = BaseSpeed * (1 + _bonusPercentagePerNextLevel * (_level - 1) / 100.0f);
        }
    }

    void Start()
    {
        if (Level == 0)
            Level = 1;
    }

    void Ivk_SlowDebuff()
    {
        Speed = BaseSpeed;
        _isSlow = false;
    }
}
