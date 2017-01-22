using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

    public UpgradeCenter uc;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            uc.UpgradeMobLevel(MobEntity.e_MobId.TANKER);
            uc.UpgradeTowerLevel(TowerEntity.e_TowerId.XRAY);
        }
	}
}
