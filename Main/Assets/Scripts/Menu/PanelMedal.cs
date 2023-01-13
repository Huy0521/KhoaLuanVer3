using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PanelMedal : MonoBehaviour
{
    [SerializeField] Button btn_Back;
    [SerializeField] List<GameObject> listMedal;
    int playstar;
    void Start()
    {
        btn_Back.onClick.AddListener(() =>
        {
            PopupManager.Instance.menu.SetActive(true);
            Destroy(gameObject);
        });
    }
    void CheckMedal()
    {
        for (int i = 0; i < PopupManager.Instance.listmap.tuantu.Length; i++)
        {

        }
    }
}
