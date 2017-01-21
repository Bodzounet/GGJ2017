using UnityEngine;
using System.Collections;

public class NormalAI : MonoBehaviour {

    // Use this for initialization

    public NavMeshAgent agent;
    public GameObject goal;

    private SoldatEntity soldier;
    private float _previousRemaningDistance = -1;

	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
        agent.SetDestination(goal.transform.position);
        soldier = this.GetComponent<SoldatEntity>();
        agent.speed = soldier.Speed;
    }

    // Update is called once per frame
    void Update () {
        if (_previousRemaningDistance == agent.remainingDistance)
            Debug.Log("JE SUIS BLOQUÉ !!");
        _previousRemaningDistance = agent.remainingDistance;
    }
}
