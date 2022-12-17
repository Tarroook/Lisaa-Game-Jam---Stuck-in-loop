using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : Modifier
{
    public ParticleSystem holyParticle;

    private void Start()
    {
        Transform part = Instantiate(holyParticle, transform).transform;
        part.localPosition = Vector3.zero;
    }
}
