using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spell : MonoBehaviour {
    [SerializeField] GameObject explosion;
    [SerializeField] float push;
    [SerializeField] float dmg;
    [SerializeField] float speed;
    [SerializeField] float explosionTime = 5f;
    private PhotonView view;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(Explode());
        view = GetComponent<PhotonView>();
        if (view.IsMine) {
            if (this.speed != 0) {
                this.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward.normalized) * speed;
            }
        }
    }

    // Update is called once per frame
    void Update() {
       
    }

    private void OnTriggerEnter(Collider other) {
        PhotonNetwork.Destroy(this.gameObject);
        if (other.CompareTag("Player")) {
            other.GetComponentInParent<PlayerController>().updateHealth(-10);
        }
        // view.RPC("SpawnParticle", RpcTarget.All);
    }

    [PunRPC]
    void SpawnParticle() {
        Instantiate(explosion.gameObject, transform.position, Quaternion.identity);
    }

    IEnumerator Explode() {
        yield return new WaitForSeconds(explosionTime);
        PhotonNetwork.Instantiate(explosion.name, transform.position, Quaternion.identity);
        PhotonNetwork.Destroy(gameObject);
    }
}
