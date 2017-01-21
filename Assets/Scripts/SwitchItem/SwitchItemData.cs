using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// To make it work, 
///  - use the designed prefab for any children.
///  - each child must have the same size
///  - let the same space between each child
///  - order the child from the one which is the most on the left to the one which is the most on the right
/// </summary>
/// 
public class SwitchItemData : MonoBehaviour
{
    private List<RectTransform> _holders = new List<RectTransform>();
    private List<Vector3> _pos = new List<Vector3>();
    public delegate void SwitchItem(TowerEntity.e_TowerId towerId, MobEntity.e_MobId mobID);
    public event SwitchItem OnSwitchItem;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _holders.Add(transform.GetChild(i) as RectTransform);
            _pos.Add(_holders[i].transform.localPosition);
        }
        if (OnSwitchItem != null)
            OnSwitchItem(_holders[1].GetComponent<InfoTowerUI>().towerId, _holders[1].GetComponent<InfosMobUI>().mobId);
    }

    public void SwitchPositionFromLeftToRight()
    {
        for (int i = 0; i < _holders.Count - 1; i++)
        {
            _holders[i].SendMessage("MoveTo", _pos[i + 1]);
        }
        _holders.Last().SendMessage("GoTo", _pos[0]);

        List<RectTransform> reordered = new List<RectTransform>();
        reordered.Add(_holders[_holders.Count - 1]);
        reordered.AddRange(_holders.Take(_holders.Count - 1));

        _holders = reordered;

        if (OnSwitchItem != null)
            OnSwitchItem(_holders[1].GetComponent<InfoTowerUI>().towerId, _holders[1].GetComponent<InfosMobUI>().mobId);
    }

    public void SwitchPositionFromRightToLeft()
    {
        for (int i = _holders.Count - 1; i > 0; i--)
        {
            _holders[i].SendMessage("MoveTo", _pos[i - 1]);
        }
        _holders.First().SendMessage("GoTo", _pos[_pos.Count - 1]);

        List<RectTransform> reordered = new List<RectTransform>();
        reordered.AddRange(_holders.Skip(1).Take(_holders.Count - 1));
        reordered.Add(_holders[0]);

        _holders = reordered;
        if (OnSwitchItem != null)
            OnSwitchItem(_holders[1].GetComponent<InfoTowerUI>().towerId, _holders[1].GetComponent<InfosMobUI>().mobId);
    }
}
