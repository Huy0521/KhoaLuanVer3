using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
public class PlayerInfor : MonoBehaviour
{
    [SerializeField] private TMP_Text txtPlayerName;
    void Start()
    {
        
    }
    public void SetPlayerInfor(Player _player)
    {
        txtPlayerName.text = _player.NickName;
    }
}
