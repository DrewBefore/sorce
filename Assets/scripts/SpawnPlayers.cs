using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour {
    [SerializeField] GameObject player;
    [SerializeField] float minX, minZ, maxX, maxZ;
    [SerializeField] bool multiplayer;

    private void Start() {
        multiplayer = PhotonNetwork.IsConnectedAndReady;
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
        if (multiplayer) {
            PhotonNetwork.Instantiate(player.name, randomPosition, Quaternion.identity);
        } else {
            Instantiate(player, randomPosition, Quaternion.identity);
        }
    }
}
