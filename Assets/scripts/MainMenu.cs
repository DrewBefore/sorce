using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MainMenu : MonoBehaviourPunCallbacks {
    public InputField createInput;
    public InputField joinInput;
    public InputField nameInput;

    void Start() {
        PhotonNetwork.OfflineMode = true;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    public void changeName() {
        PhotonNetwork.NickName = nameInput.text;
    }

    public void CreateRoom() {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(createInput.text, roomOptions);
    }

    public void JoinRoom() {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom() {
        PhotonNetwork.LoadLevel("Game");
    }
}
