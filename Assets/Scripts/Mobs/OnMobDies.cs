using UnityEngine;
using System.Collections;

public class OnMobDies : MonoBehaviour
{
    public ParticleSystem[] alive;
    public ParticleSystem dead;

    public void OnDeath()
    {
        foreach (var v in alive)
            v.Stop();
        dead.Play();
        Destroy(this.gameObject, 1.25f);
    }
}
