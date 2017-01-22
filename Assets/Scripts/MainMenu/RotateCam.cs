using UnityEngine;
using System.Collections;

public class RotateCam : MonoBehaviour
{
    Quaternion _target;

    void Start()
    {
        _target = transform.rotation;
    }

    public void RotateRight()
    {
        transform.rotation = _target;
        _target = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 90, transform.rotation.eulerAngles.z);

        StopAllCoroutines();
        StartCoroutine("Co_Rotate");
    }

    public void RotateLeft()
    {
        transform.rotation = _target;
        _target = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 90, transform.rotation.eulerAngles.z);

        StopAllCoroutines();
        StartCoroutine("Co_Rotate");
    }

    private IEnumerator Co_Rotate()
    {
        while (transform.rotation != _target)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _target, 0.1f);
            yield return new WaitForEndOfFrame();
        }
    }
}
