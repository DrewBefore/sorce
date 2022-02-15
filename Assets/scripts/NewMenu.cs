using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

using Photon.Pun;

public class NewMenu : MonoBehaviourPunCallbacks {
    public Button lobbyButton;
    public Button matchmakingButton;
    public Button localButton;
    public GameObject connectServer;

    private VisualElement mainMenu;
    private VisualElement loadingScreen;
    private VisualElement lobbyScreen;

    // Start is called before the first frame update
    void Start() {
        var root = GetComponent<UIDocument>().rootVisualElement;
        mainMenu = root.Q<VisualElement>("MainMenu");
        loadingScreen = root.Q<VisualElement>("LoadingScreen");
        lobbyScreen = root.Q<VisualElement>("LobbyScreen");

        lobbyButton = root.Q<Button>("lobbyButton");
        lobbyButton.Focus();

        localButton = root.Q<Button>("localButton");

        matchmakingButton = root.Q<Button>("matchmakingButton");
        lobbyButton.clicked += lobbyPressed;
        matchmakingButton.clicked += matchmakingPressed;
        localButton.clicked += localPressed;
    }

    void lobbyPressed() {
        mainMenu.style.display = DisplayStyle.None;
        loadingScreen.style.display = DisplayStyle.Flex;
        if (!PhotonNetwork.IsConnected) {
            PhotonNetwork.ConnectUsingSettings();
        } else {
            loadingScreen.style.display = DisplayStyle.None;
            lobbyScreen.style.display = DisplayStyle.Flex;
        }
    }

    void matchmakingPressed() {

    }

    void localPressed() {
        SceneManager.LoadScene("Game");
    }

    public override void OnConnectedToMaster() {
        base.OnConnectedToMaster();
        loadingScreen.style.display = DisplayStyle.None;
        lobbyScreen.style.display = DisplayStyle.Flex;
    }
}
