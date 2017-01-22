using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RegularAI : MonoBehaviour
{
    public GameObject goal;

    private NavMeshAgent _agent;
    private MobEntity _soldier;
    private float _previousRemaningDistance = -1;

    private List<GameObject> _triggeredTowerList;
    private bool _unlockPassage = false;

    void Start()
    {
        _agent = this.GetComponent<NavMeshAgent>();
        _soldier = this.GetComponent<MobEntity>();
        _agent.speed = _soldier.Speed;
        _triggeredTowerList = new List<GameObject>();
        goal = GameObject.Find("Destination" + _soldier.team.ToString());
    }

    void Update()
    {
        _agent.ResetPath();
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
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
