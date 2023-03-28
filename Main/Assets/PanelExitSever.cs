using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
public class PanelExitSever : MonoBehaviour
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
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("SampleScene");
    }
    private void No_Click()
    {
        gameObject.SetActive(false);
    }

}
