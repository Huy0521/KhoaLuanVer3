using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject level;
    [SerializeField] private TMP_InputField ipRoomName;
    [SerializeField] private TMP_InputField ipRoomJoin;
    [SerializeField] private GameObject room;
    [SerializeField] private TMP_Text roomName;
    [SerializeField] private Button btn_LeaveRoom;

    void Start()
    {
        PopupManager.Instance.canvas = gameObject;
        PhotonNetwork.JoinLobby();
    }
    public void CreateRoom()
    {
        if(ipRoomName.text.Length>=1)
        {
            PhotonNetwork.CreateRoom(ipRoomName.text);
        }
        
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(ipRoomJoin.text  );
    }
    public override void OnJoinedRoom()
    {
        room.SetActive(true);
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        Debug.Log("Create: " + PhotonNetwork.CurrentRoom.Name);
    }
    public void LeaveRoom_Click()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        room.SetActive(false);
    }
   public void StarGame_Click()
    {
        PopupManager.Instance.currentDashboard = Instantiate(PopupManager.Instance.userPlay, PopupManager.Instance.canvas.transform);
        PopupManager.Instance.currentMap = Instantiate(level);
    }
}
