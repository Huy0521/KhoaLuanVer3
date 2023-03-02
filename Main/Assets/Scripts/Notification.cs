using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Notification : MonoBehaviour
{
    public TMP_Text titel;
    [SerializeField] private Animator animator;
    private void changeAnim()
    {
        animator.Play("EndNoftificationAnime");
        Invoke("Kill", 0.5f);
    }
    public void SetNotiTime(float time)
    {
        Invoke("changeAnim", time);
    }
    private void Kill()
    {
        Destroy(gameObject);
        PopupManager.Instance.notificationIsOn = false;
    }
}
