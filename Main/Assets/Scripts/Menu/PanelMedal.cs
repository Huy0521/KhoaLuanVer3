using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PanelMedal : MonoBehaviour
{
    [SerializeField] Button btn_Back;
    [SerializeField] List<GameObject> listMedal;
    void Start()
    {
        btn_Back.onClick.AddListener(() =>
        {
            PopupManager.Instance.menu.SetActive(true);
            Destroy(gameObject);
        });
    }
    public void ScaleMiddle()
    {
       /* for(int i=0;i<listMedal.Count;i++)
        {
            if (listMedal[i].transform.localPosition == new Vector3(775, -295.765f, 0))
            {
                LeanTween.scale(listMedal[i], new Vector3(1.2f, 1.2f, 1.2f), 1f);
            }
        }*/
    }
}
