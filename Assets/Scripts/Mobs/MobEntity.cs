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
    }

    [SerializeField]
    private float _speed;
    public float Speed
    {
        get { return _speed; }
    }

    [SerializeField]
    private float _income;
    public float Income
    {
        get { return _income; }
    }

    public e_MobId id;
    public GameInfos.e_Team team;
}
