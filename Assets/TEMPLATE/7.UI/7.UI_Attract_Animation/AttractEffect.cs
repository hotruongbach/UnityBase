using DG.Tweening;
using Monster;
using Monster.Utilities;
using MyBox;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AttractEffect : MonoBehaviour
{
    [SerializeField] RectTransform rect;
    [SerializeField] Attractor target;
    [SerializeField] AttractItem prefab;
    [SerializeField] ParticleSystem particle;

    [Separator("ANIM CONFIG")]
    [SerializeField] int numberOfElement;
    [SerializeField] float AppearDuration = 0.65f;
    [SerializeField] float MoveDuration = 1f;
    [SerializeField] Ease AppearEase = Ease.OutExpo;
    [SerializeField] Ease MoveEase = Ease.InFlash;
    [SerializeField] float interval = 0.05f;
    [Separator]
    [ReadOnly] public bool IsAnimating = false;
    Camera mainCam;
    List<AttractItem> listElement = new List<AttractItem>();
    private void Awake()
    {
        mainCam = Camera.main;
        for (int i = 0; i < numberOfElement; i++)
        {
            var newElement = Instantiate(prefab, this.transform);
            newElement.gameObject.SetActive(false);
            listElement.Add(newElement);
        }
    }
    public void SetPosition(Component sender)
    {
        if (sender == null) rect.position = Vector3.zero;
        else
        {
            if (sender.GetComponent<RectTransform>() != null)
            {
                transform.position = sender.transform.position;
            }
            else
            {
                transform.position = mainCam.WorldToScreenPoint(sender.transform.position);
            }
        }
    }
    public void SetAttractor(Attractor attractor)
    {
        this.target = attractor;
    }

    public void Play(Action Oncomplete = null)
    {
        IsAnimating = true;
        particle?.Stop();
        particle?.Play();
        for (int i = 0; i < listElement.Count; i++)
        {
            listElement[i].Play(rect, 0.1f, target.Position,
                AppearDuration, MoveDuration,
                AppearEase, MoveEase,
                i * interval,
                OnElementReachTarget);
        }

        this.StartDelayAction(AppearDuration + MoveDuration + (listElement.Count - 1) * interval, () =>
        {
            OnAnimComplete();
            Oncomplete?.Invoke();
        });
    }

    public void SetElementSprite(Sprite sprite)
    {
        foreach(var item in listElement)
        {
            item.SetSprite(sprite);
        }
    }

    private void OnElementReachTarget()
    {
        target.OnAttractCompleted();
    }
    private void OnAnimComplete()
    {
        Bug.Log("Animation done");
        IsAnimating = false;
    }

    
}

[Serializable]
public class EffectTriggerer 
{
    [SerializeField] Transform effectRoot;
    [SerializeField] AttractEffect effectPrefabs;
    List<AttractEffect> effectPool = new List<AttractEffect>();
    Sprite elementSprite;

    public void Play(Component spawnArea, Attractor attractor,Action OnComplete)
    {
        var coinEffect = ReuseCoinEffect(attractor);

        if (elementSprite != null){
            coinEffect.SetElementSprite(elementSprite);
        }

        coinEffect.SetPosition(spawnArea);
        coinEffect.Play(OnComplete);
    }
    private AttractEffect ReuseCoinEffect(Attractor attractor)
    {
        AttractEffect effect = effectPool.Find(x => x.IsAnimating == false);

        if (effect == null)
        {
            effect = GameObject.Instantiate(effectPrefabs, effectRoot);
            effect.SetAttractor(attractor);
            effectPool.Add(effect);
        }

        return effect;
    }
    public void SetSprite(Sprite sprite)
    {
        elementSprite = sprite;
    }
}
