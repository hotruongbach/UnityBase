using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineGiftDot : MonoBehaviour
{
    [SerializeField] GameObject FocusDot;
    
    public void Focus()
    {
        FocusDot.SetActive(true);
    }

    public void Unfocus()
    {
        FocusDot.SetActive(false);
    }
}
