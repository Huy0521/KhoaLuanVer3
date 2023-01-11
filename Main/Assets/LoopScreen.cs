using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopScreen : MonoBehaviour
{
    private void OnEnable()
    {
        GameController.Instance.chooseBtn = SpecialBtn.loop;
    }
    private void OnDisable()
    {
        GameController.Instance.chooseBtn = SpecialBtn.none;
    }
}
