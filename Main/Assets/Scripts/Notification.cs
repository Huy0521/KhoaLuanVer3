using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Notification : MonoBehaviour
{
    public TMP_Text titel;
    public Image img;
    [SerializeField] private Animator animator;
    private void changeAnim()
    {
        animator.Play("EndNoftificationAnime");
        Invoke("Kill", 0.5f);
    }
    public void SetNotiTime(float time, Sprite sprite)
    {
        if(sprite!=null)
        {
            img.sprite = sprite;
        }
        Invoke("changeAnim", time);
    }
    private void Kill()
    {
        Destroy(gameObject);
        PopupManager.Instance.notificationIsOn = false;
    }
}
