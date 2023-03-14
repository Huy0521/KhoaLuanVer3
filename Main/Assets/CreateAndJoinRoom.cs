using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField ipRoomName;
    [SerializeField] private GameObject room;
    [SerializeField] private TMP_Text roomName;
    void Start()
    {
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
        PhotonNetwork.JoinRoom("a");
    }
    public override void OnJoinedRoom()
    {
        room.SetActive(true);
        roomName.text = PhotonNetwork.CurrentRoom.Name;
    }
}
