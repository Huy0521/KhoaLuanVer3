using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemMedal : MonoBehaviour
{
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text description;
    [SerializeField] Image imgMedal;
    public void configView(string txt_Title, string txt_Des)
    {
        imgMedal.color = new Color(1,1,1);
        title.text = txt_Title;
        description.text = txt_Des;
    }

}
