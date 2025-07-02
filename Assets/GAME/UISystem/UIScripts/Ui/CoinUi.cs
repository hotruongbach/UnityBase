using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUi : MonoBehaviour
{
    [SerializeField] Text textCoin;
    // Start is called before the first frame update
    void Start()
    {
        OnChangeCoin();
        TemplateEventManager.OnChangeCoin.AddListener(this, OnChangeCoin);
    }
    private void OnDestroy()
    {
        TemplateEventManager.OnChangeCoin.RemoveListener(this, OnChangeCoin);
    }
    void OnChangeCoin(int param = 0)
    {
        textCoin.text = UserDataCache.Coin.ToString();
    }
}
