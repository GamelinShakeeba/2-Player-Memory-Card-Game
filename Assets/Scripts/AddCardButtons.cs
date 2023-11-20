using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCardButtons : MonoBehaviour
{
    [SerializeField] Transform cardButtonLayout;
    [SerializeField] GameObject cardButton;

    private void Awake()
    {
        for (int i = 0; i < 12; i++)
        {
            GameObject button = Instantiate(cardButton);
            button.name = "" + i;
            button.transform.SetParent(cardButtonLayout);
        }
    }
}
