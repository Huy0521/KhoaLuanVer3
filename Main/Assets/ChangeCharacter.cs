using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeCharacter : MonoBehaviour
{
    [SerializeField] private Button btnRight;
    [SerializeField] private Button btnLeft;
    [SerializeField] private List<GameObject> listCharacter;

   private void Start()
    {
        btnRight.onClick.AddListener(Right_click);
        btnLeft.onClick.AddListener(Left_click);
    }
    private void Right_click()
    {
        int onActiveCharacter = 0;
        for(int i=0;i<listCharacter.Count;i++)
        {
            if (listCharacter[i].activeSelf==true)
            {
                onActiveCharacter = i;
                
            }
        }
        for (int j = 0; j < listCharacter.Count; j++)
        {
            listCharacter[j].SetActive(false);
        }
        if (onActiveCharacter< listCharacter.Count - 1)
        {
            listCharacter[onActiveCharacter + 1].SetActive(true);
        }
        else
        {
            listCharacter[0].SetActive(true);
        }
    }
    private void Left_click()
    {
        int onActiveCharacter = 0;
        for (int i = 0; i < listCharacter.Count; i++)
        {
            if (listCharacter[i].activeSelf == true)
            {
                onActiveCharacter = i;

            }
        }
        for (int j = 0; j < listCharacter.Count; j++)
        {
            listCharacter[j].SetActive(false);
        }
        if (onActiveCharacter > 0)
        {
            listCharacter[onActiveCharacter - 1].SetActive(true);
        }
        else
        {
            listCharacter[2].SetActive(true);
        }
    }
}
