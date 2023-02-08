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
    void changeAnim()
    {
        animator.Play("EndNoftificationAnime");
        Invoke("Kill",0.5f);
    }
    void Kill()
    {
        Destroy(gameObject);
    }
}
