using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour {
    [SerializeField] float speed;
    private float resetSpeed;
    private float dashSpeed = 1000;

    private Vector2 i_movement;
    private Rigidbody rb;
    private PhotonView view;
    private Animator anim;
    private GameObject skin;
    private int health = 100;
    [SerializeField] Canvas playerUi;
    [SerializeField] TMP_Text nameDisplay;
    private bool isAlive = true;

    [SerializeField] Spell attack1;
    [SerializeField] Spell attack2;
    [SerializeField] Spell attack3;
    [SerializeField] Spell attack4;


    void Start() {
        resetSpeed = this.speed;
        rb = GetComponent<Rigidbody>();
        view = GetComponent<PhotonView>();
        skin = GameObject.FindGameObjectWithTag("Skin");
        anim = skin.GetComponent<Animator>();
        if (view.IsMine) {
            playerUi.gameObject.SetActive(true);
            nameDisplay.text = PhotonNetwork.NickName;
        } else {
            if (view.Owner != null) {
            nameDisplay.text = view.Owner.NickName;
            } else {
                nameDisplay.text = "db4";
            }
        }
    }
    // Update is called once per frame
    void Update() {
        if (view.IsMine && isAlive) {
            Vector3 movement = new Vector3(i_movement.x, 0, i_movement.y).normalized;
            rb.AddForce(movement * speed * Time.deltaTime);
            transform.forward += movement;
            if (i_movement == Vector2.zero) {
                anim.SetBool("IsRunning", false);
            } else {
                anim.SetBool("IsRunning", true);
            }
        }
    }

    IEnumerator Dash() {
        speed = dashSpeed;
        yield return new WaitForSeconds(1);
        speed = resetSpeed;
    }
    
    private void OnTriggerEnter(Collider other) {
        // if (view.IsMine) {
        //     if (other.tag == "Enemy") {
        //         updateHealth(-10);
        //     }
        // }
    }

    public void updateHealth(int amount) {
        // if (view.IsMine) {
        //     view.RPC("updateHealthRPC", RpcTarget.All, amount);
        // }
        health += amount;
        Debug.Log(health);
        playerUi.GetComponentInChildren<Text>().text = health.ToString();
        if (health <= 0) {
            isAlive = false;
            anim.SetBool("Death", true);
            playerUi.transform.Find("GameOver").gameObject.SetActive(true);
        } else {
            anim.SetTrigger("Hit");
        }
    }

    [PunRPC]
    public void updateHealthRPC(int amount) {
        health += amount;
    }

    private void Fire(Spell attack) {
        if (view.IsMine) {
            Vector3 spawn = transform.position + transform.forward * 1.25f + transform.up * .5f;
            Debug.Log(transform.rotation);
            var newProjectile = PhotonNetwork.Instantiate("Spells/" + attack.name, spawn, transform.rotation, 0).GetComponent<Spell>();
        }
    }

     private void OnMove(InputValue value) {
        i_movement = value.Get<Vector2>();
    }
    
    private void OnLTrigger() {
        // anim.ResetTrigger("Walking");
        // this.aim = !this.aim;
    }

    private void OnSouth() {
        // StartCoroutine(stopMovement(.5f, "Attacking", attack1));
        if (view.IsMine) {
            anim.Play("Attack2");
            Fire(attack1);
            // StartCoroutine(stopMovement(.5f, "LevitateStart", scourge));
        }
        // if (i_movement != Vector2.zero) {
        //     StartCoroutine(Dash());
        // }
        Debug.Log("South");
    }

    private void OnEast() {
        if (view.IsMine) {
            anim.Play("Attack2");
            Fire(attack3);
            // StartCoroutine(stopMovement(.5f, "LevitateStart", scourge));
        }
    }
    private void OnWest() {
        if (view.IsMine) {
            anim.Play("Attack2");
            Fire(attack2);
            // StartCoroutine(stopMovement(.5f, "LevitateStart", scourge));
        }
        // StartCoroutine(stopMovement(.5f, "Attacking", attack2));
    }
    private void OnNorth() {
        if (view.IsMine) {
            anim.Play("LevitateStart");
            Fire(attack4);
            // StartCoroutine(stopMovement(.5f, "LevitateStart", scourge));
        }
        // StartCoroutine(stopMovement(.5f, "Attacking", attack3));
    }

    [PunRPC]
    void playAnimation(string name) {
        anim.Play(name);
    }
}
