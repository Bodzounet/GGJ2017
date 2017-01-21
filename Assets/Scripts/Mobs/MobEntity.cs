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
    private float _income;
    public float Income
    {
        get { return _income; }
    }

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

    void Ivk_SlowDebuff()
    {
        Speed = BaseSpeed;
        _isSlow = false;
    }
}
