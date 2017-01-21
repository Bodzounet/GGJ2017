using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NormalAI : MonoBehaviour
{

    // Use this for initialization

    public GameObject goal;

    private NavMeshAgent _agent;
    private SoldatEntity soldier;
    private float _previousRemaningDistance = -1;

    private List<GameObject> _triggeredTowerList;
    private bool _unlockPassage = false;

    void Start()
    {
        _agent = this.GetComponent<NavMeshAgent>();
        soldier = this.GetComponent<SoldatEntity>();
        _agent.speed = soldier.Speed;
        _triggeredTowerList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(goal.transform.position);
        if (_agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            _unlockPassage = true;
        }  
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tower")
        {
            if (_unlockPassage == true)
            {
                Debug.Log(other.gameObject);
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
