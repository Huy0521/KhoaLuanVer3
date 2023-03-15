using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawPlayer : MonoBehaviour
{
    public GameObject playerPrefab;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        GameObject pl =  PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        if(pl.GetComponent<PhotonView>().IsMine)
        {
            PopupManager.Instance.playerControllerInArena = pl.GetComponent<PlayerControllerInArena>();
        }
        
    }

}
