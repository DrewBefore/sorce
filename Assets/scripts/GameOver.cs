using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameOver : MonoBehaviour {
    // Start is called before the first frame update
    
    public void toMainMenu() {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Main Menu");
    }
}
