using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
public class PanelExitRoom : MonoBehaviour
{
    [SerializeField] private Button btnNo;
    [SerializeField] private Button btnYes;
    private void Start()
    {
        btnNo.onClick.AddListener(No_Click);
        btnYes.onClick.AddListener(Yes_Click);
    }
    private void Yes_Click()
    {
        Destroy(PopupManager.Instance.currentDashboard.gameObject);
        Destroy(PopupManager.Instance.currentMap);
        PhotonNetwork.LeaveRoom();
        GameController.Instance.ResetGameController();
    }
    private void No_Click()
    {
        gameObject.SetActive(false);
    }
}
