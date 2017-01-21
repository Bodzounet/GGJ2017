using UnityEngine;
using System.Collections;
using System.Linq;

public class MobSpawner : MonoBehaviour
{
    public Dict_MobIdToPrefab[] mobs;

    public void CreateMob(MobEntity.e_MobId id, Vector3 pos, GameInfos.e_Team team)
    {
        var v = Instantiate(mobs.Single(x => x.id == id).prefab, pos, Quaternion.identity) as GameObject;
        v.GetComponent<MobEntity>().team = team;
    }

    [System.Serializable]
    public struct Dict_MobIdToPrefab
    {
        public MobEntity.e_MobId id;
        public GameObject prefab;
    }
}
