using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarRate : MonoBehaviour
{
    [SerializeField] Sprite yellowStar;
    [SerializeField] Sprite blueStar;
    [SerializeField] Image star;

    public void RateStar()
    {
        star.sprite = yellowStar;
    }

    public void ResetStar()
    {
        star.sprite = blueStar;
    }
}
