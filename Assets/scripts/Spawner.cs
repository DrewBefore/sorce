using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour {
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject enemy;
    [SerializeField] float startTimeBtwSpawns;
    private float timeBtwSpawns;

    // Start is called before the first frame update
    void Start() {
        timeBtwSpawns = startTimeBtwSpawns;
    }

    // Update is called once per frame
    void Update() {
        if (PhotonNetwork.IsMasterClient == true) {
            if (timeBtwSpawns <= 0) {
                Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position;
                PhotonNetwork.Instantiate(enemy.name, spawnPosition, Quaternion.identity);
                timeBtwSpawns = startTimeBtwSpawns;
            } else {
                timeBtwSpawns -= Time.deltaTime;
            }
        }
    }
}
