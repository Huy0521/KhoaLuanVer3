using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Notification : MonoBehaviour
{
    public Text titel;
    [SerializeField] private Animator animator;
    private void Start()
    {
        Invoke("changeAnim", 2.0f);
    }
    private void changeAnim()
    {
        animator.Play("EndNoftificationAnime");
        Invoke("Kill", 0.5f);
    }
    private void Kill()
    {
        Destroy(gameObject);
        PopupManager.Instance.notificationIsOn = false;
    }
}
