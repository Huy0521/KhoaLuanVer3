using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class selectLevel_Controller : MonoBehaviour
{
    [SerializeField] private ItemLevel itemLevel;
    [SerializeField] private List<GameObject> ListMap_Tuantu;
    [SerializeField] private List<GameObject> ListMap_Vonglap;
    [SerializeField] private List<GameObject> ListMap_Renhanh;
    [SerializeField] private Button btn_Back;
    [SerializeField] private List<ItemRoadMap> listItem;
    void Start()
    {
        btn_Back.onClick.AddListener(Back_click);
        switch(PopupManager.Instance.loaibai)
        {
            case Loaibai.tuantu:
                for (int i = 0; i < PopupManager.Instance.listmap.tuantu.Length; i++)
                {
                    listItem[i].configView(PopupManager.Instance.listmap.tuantu[i], ListMap_Tuantu[i]);
                    listItem[i].gameObject.SetActive(true);
                }
                break;
            case Loaibai.vonglap:
                for (int i = 0; i < PopupManager.Instance.listmap.vonglap.Length; i++)
                {
                    listItem[i].configView(PopupManager.Instance.listmap.vonglap[i], ListMap_Vonglap[i]); 
                    listItem[i].gameObject.SetActive(true);
                }
                break;
            case Loaibai.renhanh:
                for (int i = 0; i < PopupManager.Instance.listmap.renhanh.Length; i++)
                {
                    listItem[i].configView(PopupManager.Instance.listmap.renhanh[i], ListMap_Renhanh[i]);
                    listItem[i].gameObject.SetActive(true);
                }
                break;
        }

    }
     void Back_click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        Destroy(gameObject);
    }
}
