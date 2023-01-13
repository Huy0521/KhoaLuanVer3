using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfScreen : MonoBehaviour
{
    private void OnEnable()
    {
        GameController.Instance.chooseBtn = SpecialBtn.ifElse;
    }
    private void OnDisable()
    {
        GameController.Instance.chooseBtn = SpecialBtn.none;
    }
}
