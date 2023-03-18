using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class ArenaMapController : MonoBehaviour
{
    public GameObject playerPrefab;
    public int playerNumberInRoom = 0;
    [SerializeField] private List<GameObject> listSpawposition;
    void Start()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            if (p != PhotonNetwork.LocalPlayer)
            {
                playerNumberInRoom++;
            }
        }
        gameObject.transform.position = new Vector3(-15, -0.32f, 0);
        LeanTween.moveLocalX(gameObject, -4f, 0.65f).setEaseOutQuad();
        GameObject pl = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        pl.transform.SetParent(PopupManager.Instance.currentMap.transform);
        pl.transform.localPosition = listSpawposition[playerNumberInRoom].transform.localPosition;
        if (pl.GetComponent<PhotonView>().IsMine)
        {
            PopupManager.Instance.playerControllerInArena = pl.GetComponent<PlayerControllerInArena>();
        }
    }

}
