using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BaseClass;

public class ItemRoadMap : MonoBehaviour
{
    [SerializeField] private Image middle;
    [SerializeField] private Image bottom;
    [SerializeField] private List<Image> listStar;
    [SerializeField] private TMP_Text txt_manchoi;
    [SerializeField] private Sprite middleActive;
    [SerializeField] private Sprite bottomActive;
    [SerializeField] private Sprite sideStar;
    [SerializeField] private Sprite middleStar;
    [SerializeField] private Sprite leftStar_Active;
    [SerializeField] private Sprite middleStar_Active;
    [SerializeField] private Sprite rightStar_Active;
    [SerializeField] private Map thislevel;
    [SerializeField] private GameObject level;
    [HideInInspector] public GameObject panelSelectlevel;
    public void Item_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        PopupManager.Instance.currentLevel = thislevel;
        PopupManager.Instance.currentMap = Instantiate(level);
        Destroy(panelSelectlevel);
        Instantiate(PopupManager.Instance.userPlay, PopupManager.Instance.canvas.transform);
        PlayerPrefs.SetFloat("LastMapClick",float.Parse(txt_manchoi.text));
    }
    void OnStar(bool star1, bool star2, bool star3)
    {
        if (star1)
        {
            listStar[0].sprite = leftStar_Active;
        }
        if (star2)
        {
            listStar[1].sprite = middleStar_Active;
        }
        if (star3)
        {
            listStar[2].sprite = rightStar_Active;
        }
    }
    
    public void configView(Map baichoi, GameObject map)
    {
        gameObject.LeanMoveLocal(new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + 20), Random.Range(0.8f, 1.4f)).setLoopPingPong();
        txt_manchoi.text = baichoi.level;
        level = map;
        thislevel = baichoi;
       
        switch (baichoi.star)
        {
            case 0:
                OnStar(false, false, false);
                break;
            case 1:
                OnStar(true, false, false);
                break;
            case 2:
                OnStar(true, true, false);
                break;
            case 3:
                OnStar(true, true, true);
                break;
        }
        if(baichoi.star>0)
        {
            middle.sprite = middleActive;
            bottom.sprite = bottomActive;
        }
    }
}
