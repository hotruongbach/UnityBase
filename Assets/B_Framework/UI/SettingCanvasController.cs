using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SettingCanvasController : BasePopup
{
    public override void OpenPopup()
    {      
        base.OpenPopup();
    }
    public void CloseSetting()
    {

            ClosePopup();

    }
    public void OnClickPrivacy()
    {
        Application.OpenURL("");
    }

    public void OnClickTermsOfUse()
    {
        Application.OpenURL("");
    }
}
