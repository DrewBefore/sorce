using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Enemy : MonoBehaviour {
    // Start is called before the first frame update
    PlayerController[] players;
    PlayerController nearestPlayer;
    [SerializeField] float speed;
    private float distanceOne;
    private float distanceTwo;
    [SerializeField] GameObject hitFX;
    private PhotonView view;


    void Start() {
        view = GetComponent<PhotonView>();
        players = FindObjectsOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update() {
        distanceOne = Vector3.Distance(transform.position, players[0].transform.position);
        nearestPlayer = players[0];
        if (players.Length > 1) {
            distanceTwo = Vector3.Distance(transform.position, players[1].transform.position);
            if (distanceOne < distanceTwo) {
                nearestPlayer = players[0];
            } else {
                nearestPlayer = players[1];
            }
        }

        if (nearestPlayer != null) {
            transform.position = Vector3.MoveTowards(transform.position, nearestPlayer.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other) {
        other.GetComponentInParent<PlayerController>().updateHealth(-10);
        if (PhotonNetwork.IsMasterClient) {
            if (other.gameObject.tag == "Player") {
                view.RPC("SpawnParticle", RpcTarget.All);
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }

    [PunRPC]
    void SpawnParticle() {
        Instantiate(hitFX, transform.position, Quaternion.identity);
    }
}
