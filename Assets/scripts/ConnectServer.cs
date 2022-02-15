using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectServer : MonoBehaviourPunCallbacks {
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject multiplayerRoom;

    private void Start() {
        if (!PhotonNetwork.IsConnected) {
            PhotonNetwork.ConnectUsingSettings();
        } else {
            loadingScreen.SetActive(false);
            multiplayerRoom.SetActive(true);
        }
    }
    public override void OnConnectedToMaster() {
        base.OnConnectedToMaster();
        loadingScreen.SetActive(false);
        multiplayerRoom.SetActive(true);
    }
}
