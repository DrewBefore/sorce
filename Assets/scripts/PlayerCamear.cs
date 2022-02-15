using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamear : MonoBehaviour {
    [SerializeField] private PlayerController player;
    // Start is called before the first frame update
    void Start() {
        transform.position = Vector3.up * 10;
    }

    // Update is called once per frame
    void LateUpdate() {
        transform.rotation = Quaternion.Euler (90f, 0, 0);
        transform.position = player.transform.position - (Quaternion.Euler (90f, 0, 0) * Vector3.forward * 10);
    }
}
