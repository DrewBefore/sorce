using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCleave : Spell {
    [SerializeField] private ParticleSystem wallParticle;

    void Awake() {
        transform.position += transform.up * -0.5f;
    }
}
