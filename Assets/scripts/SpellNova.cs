using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellNova : Spell {
    void Awake() {
        transform.position += transform.up * -0.5f;
        transform.position += transform.forward * -1.25f;
    }
}
