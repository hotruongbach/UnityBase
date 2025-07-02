using System.Collections;
using System.Collections.Generic;
using Monster;
using Monster.UI;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPause : MonoBehaviour
{
    Button thisBtn;
    private void Awake()
    {
        thisBtn = GetComponent<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        thisBtn.onClick.AddListener(ShowPauseGame);
    }

   void ShowPauseGame()
    {
        GameService.ShowPopup<PausePopup>();
    }
}
