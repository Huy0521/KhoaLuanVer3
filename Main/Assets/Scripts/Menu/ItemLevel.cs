using BaseClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemLevel : MonoBehaviour
{
    [SerializeField] private Text levelNumber;
    [SerializeField] private List<Image> ListStar;
    [SerializeField] private GameObject level;
    [SerializeField] private Sprite Off_leftStar;
    [SerializeField] private Sprite Off_midStar;
    [SerializeField] private Sprite Off_rightStar;
    [SerializeField] private Sprite On_leftStar;
    [SerializeField] private Sprite On_midStar;
    [SerializeField] private Sprite On_rightStar;
    [SerializeField] private Map thislevel; 

    public void configView(Map baichoi,GameObject map)
    {
        levelNumber.text = baichoi.level;
        level = map;
        thislevel = baichoi;
        switch(baichoi.star)
        {
            case 0:
                OnandOff_Star(false, false, false);
                break;
            case 1:
                OnandOff_Star(true, false, false);
                break;
            case 2:
                OnandOff_Star(true, true, false);
                break;
            case 3:
                OnandOff_Star(true, true, true);
                break;
        }
    }
    void OnandOff_Star(bool star1,bool star2,bool star3)
    {
        if(star1)
        {
            ListStar[0].sprite = On_leftStar;
        }
        else if(!star1)
        {
            ListStar[0].sprite = Off_leftStar;
        }
        if (star2)
        {
            ListStar[1].sprite = On_midStar;
        }
        else if (!star2)
        {
            ListStar[1].sprite = Off_midStar;
        }
        if (star3)
        {
            ListStar[2].sprite = On_rightStar;
        }
        else if (!star3)
        {
            ListStar[2].sprite = Off_rightStar;
        }
    }
    public void level_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        PopupManager.Instance.currentLevel = thislevel;
        PopupManager.Instance.currentMap= Instantiate(level);
        Instantiate(PopupManager.Instance.userPlay, PopupManager.Instance.canvas.transform);
    }
}
