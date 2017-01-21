using UnityEngine;
using System.Collections;

public class SoldatEntity : MonoBehaviour {

    // Use this for initialization
    public enum e_SoldatId
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
    private float _worthGold;
    public float WorthGold
    {
        get { return _worthGold; }
    }

    [SerializeField]
    private float _income;
    public float Income
    {
        get { return _income; }
    }

    public e_SoldatId id;
    public GameInfos.e_Team team;

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
