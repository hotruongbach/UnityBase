using System.Collections;
using System.Collections.Generic;
using Template;
using Template.UI;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRestart : MonoBehaviour
{
    Button thisBtn;
    private void Awake()
    {
        thisBtn = GetComponent<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        thisBtn.onClick.AddListener(ShowPopupRestart);
    }

    void ShowPopupRestart()
    {
        GameService.ShowPopup<RestartPopup>();
    }
}
