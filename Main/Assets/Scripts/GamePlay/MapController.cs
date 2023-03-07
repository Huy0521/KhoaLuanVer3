using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private PlayerController astronaut;
    [SerializeField] private PlayerController cat;
    void Start()
    {
        gameObject.transform.position = new Vector3(-15,-0.32f,0);
        LeanTween.moveLocalX(gameObject, -4f, 0.65f).setEaseOutQuad();
        if (PopupManager.Instance.character == Character.astronaut)
        {
            cat.gameObject.SetActive(false);
            astronaut.gameObject.SetActive(true);
            PopupManager.Instance.playerController = astronaut;
        }
        else if (PopupManager.Instance.character == Character.cat)
        {
            cat.gameObject.SetActive(true);
            astronaut.gameObject.SetActive(false);
            PopupManager.Instance.playerController = cat;
        }
    }
}
