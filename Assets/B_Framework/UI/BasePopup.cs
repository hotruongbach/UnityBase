using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BasePopup : MonoBehaviour
{
    [Header("Base Element")]
    [SerializeField] private Image dimpPanel;
    [SerializeField] public float targetAlPha = 1f;
    [SerializeField] public RectTransform mainPanel;
    public virtual void OpenPopup()
    {
        gameObject.SetActive(true);
        dimpPanel?.DOFade(0, 0);
        if (mainPanel) mainPanel.localScale = Vector3.zero;
        dimpPanel?.DOFade(targetAlPha, 0.5f);
        mainPanel?.DOScale(Vector3.one, 0.5f);
    }
    public virtual void ClosePopup()
    {
        dimpPanel?.DOFade(0, 0.5f);
        mainPanel?.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
    private void OnValidate()
    {
        targetAlPha = Mathf.Clamp01(targetAlPha);
    }
}
