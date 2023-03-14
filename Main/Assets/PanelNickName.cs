using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PanelNickName : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button btn_Connect;
    [SerializeField] private TMP_Text textConnect;
    [SerializeField] private TMP_InputField ipName;
    private void Start()
    {
        btn_Connect.onClick.AddListener(Connect_Click);
    }
    private void Connect_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        if(ipName.text.Length>1)
        {
            PhotonNetwork.NickName = ipName.text;
            textConnect.text = "Đang tải...";
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            PopupManager.Instance.ShowNotification(PopupManager.Instance.canvas.gameObject, "Hãy nhập biệt danh!", 1.8f, null);
        }
    }
    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("ArenaZone");
    }
}
