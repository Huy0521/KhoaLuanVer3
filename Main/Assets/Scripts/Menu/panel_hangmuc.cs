using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class panel_hangmuc : MonoBehaviour
{
    [SerializeField] private Button btn_Vonglap;
    [SerializeField] private Button btn_Renhanh;
    [SerializeField] private Button btn_Tuantu;
    [SerializeField] private Button btn_Back;
    [SerializeField] private selectLevel_Controller selectLevel;
    void Start()
    {
        btn_Vonglap.gameObject.LeanMoveLocal(new Vector2(btn_Vonglap.transform.localPosition.x+ Random.Range(-10, 10), btn_Vonglap.transform.localPosition.y + 30), Random.Range(1f, 1.6f)).setLoopPingPong();
        btn_Renhanh.gameObject.LeanMoveLocal(new Vector2(btn_Renhanh.transform.localPosition.x+ Random.Range(-10, 10), btn_Renhanh.transform.localPosition.y + 30), Random.Range(1f, 1.6f)).setLoopPingPong();
        btn_Tuantu.gameObject.LeanMoveLocal(new Vector2(btn_Tuantu.transform.localPosition.x+ Random.Range(-10, 10), btn_Tuantu.transform.localPosition.y + 30), Random.Range(1f, 1.6f)).setLoopPingPong();
        btn_Tuantu.onClick.AddListener(tuantu_Click);
        btn_Vonglap.onClick.AddListener(vonglap_Click);
        btn_Renhanh.onClick.AddListener(renhanh_Click);
        btn_Back.onClick.AddListener(()=> 
        {
            AudioManager.Instance.PlaySound(Sound.Button);
            PopupManager.Instance.menu.SetActive(true);
            Destroy(gameObject);
        });
    }
    void tuantu_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        PopupManager.Instance.loaibai = Loaibai.tuantu;
        Instantiate(selectLevel,PopupManager.Instance.canvas.transform);
        Destroy(gameObject);
    }
    void vonglap_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        PopupManager.Instance.loaibai = Loaibai.vonglap;
        Instantiate(selectLevel, PopupManager.Instance.canvas.transform);
        Destroy(gameObject);
    }
    void renhanh_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        PopupManager.Instance.loaibai = Loaibai.renhanh;
        Instantiate(selectLevel, PopupManager.Instance.canvas.transform);
        Destroy(gameObject);

    }
}
