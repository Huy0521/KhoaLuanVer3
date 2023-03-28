using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject level;
    [SerializeField] private TMP_InputField ipRoomName;
    [SerializeField] private TMP_InputField ipRoomJoin;
    [SerializeField] private Button btnClose;
    [SerializeField] private GameObject panelExit;

    void Start()
    {
        PopupManager.Instance.canvas = gameObject;
        PhotonNetwork.JoinLobby();
        btnClose.onClick.AddListener(Close_Click);
    }
    public void CreateRoom()
    {
        if(ipRoomName.text.Length>=1)
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 2;
            PhotonNetwork.CreateRoom(ipRoomName.text, options,TypedLobby.Default);
        }
        
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(ipRoomJoin.text);
    }
    public override void OnJoinedRoom()
    {
        /*room.SetActive(true);
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        Debug.Log("Create: " + PhotonNetwork.CurrentRoom.Name);*/
        StarGame_Click();
        PopupManager.Instance.currentDashboard.UpdatePlayerInfor();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PopupManager.Instance.currentDashboard.UpdatePlayerInfor();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PopupManager.Instance.currentDashboard.UpdatePlayerInfor();
    }
    public void LeaveRoom_Click()
    {
        PhotonNetwork.LeaveRoom();
    }
   public void StarGame_Click()
    {
        PopupManager.Instance.currentDashboard = Instantiate(PopupManager.Instance.userPlay, PopupManager.Instance.canvas.transform);
        PopupManager.Instance.currentMap = Instantiate(level);
    }
    private void Close_Click()
    {
        panelExit.SetActive(true);
     
    }    
}
