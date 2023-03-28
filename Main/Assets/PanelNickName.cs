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
    [SerializeField] private Button btn_Close;
    [SerializeField] private TMP_Text textConnect;
    [SerializeField] private TMP_InputField ipName;
    private void Start()
    {
        btn_Connect.onClick.AddListener(Connect_Click);
        btn_Close.onClick.AddListener(Close_Click);
    }
    private void Connect_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        if(ipName.text.Length>=3)
        {
            PhotonNetwork.NickName = ipName.text;
            textConnect.text = "Đang tải...";
            PopupManager.Instance.isArena = true;
            PhotonNetwork.ConnectUsingSettings();
            PopupManager.Instance.loaibai = Loaibai.tuantu;
            
        }
        else
        {
            PopupManager.Instance.ShowNotification(PopupManager.Instance.canvas.gameObject, "Biệt danh phải có ít nhất 3 ký tự!", 1.8f, null);
        }
    }
    private void Close_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        gameObject.SetActive(false);
    }
    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("ArenaZone");
    }
}
