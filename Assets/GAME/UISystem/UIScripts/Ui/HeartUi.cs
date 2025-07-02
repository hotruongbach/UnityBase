using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUi : MonoBehaviour
{
    [SerializeField] Text textHeart;
    // Start is called before the first frame update
    void Start()
    {
        OnChangeHeart();
        MonsterEventManager.OnChangeHeart.AddListener(this, OnChangeHeart);
    }
    private void OnDestroy()
    {
        MonsterEventManager.OnChangeHeart.RemoveListener(this, OnChangeHeart);
    }
    void OnChangeHeart(int param = 0)
    {
        textHeart.text = UserDataCache.Heart.ToString();
    }
}
