using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ArenaMapController : MonoBehaviour
{
    public GameObject playerPrefab;
    [SerializeField] private List<GameObject> listSpawposition;
    void Start()
    {
        gameObject.transform.position = new Vector3(-15, -0.32f, 0);
        LeanTween.moveLocalX(gameObject, -4f, 0.65f).setEaseOutQuad();
        GameObject pl = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        pl.transform.SetParent(gameObject.transform);
        pl.transform.localPosition = listSpawposition[Random.Range(0, 3)].transform.localPosition;
        if (pl.GetComponent<PhotonView>().IsMine)
        {
            PopupManager.Instance.playerControllerInArena = pl.GetComponent<PlayerControllerInArena>();
        }
    }

}
