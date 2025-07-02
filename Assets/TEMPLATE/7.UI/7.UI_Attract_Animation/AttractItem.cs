using DG.Tweening;
using Template;
using Template.Utilities;
using MyBox;
using System;
using UnityEngine;
using UnityEngine.UI;
using Template.Audio;

public class AttractItem : MonoBehaviour
{
    [Separator("REFERENCES")]
    [SerializeField] RectTransform rect;
    [SerializeField] Image image;
    [Separator("CONFIG")]
    [SerializeField] SoundID soundIDAppear;
    [SerializeField] SoundID soundIDCollected;

    //this spawned in disable status

    public void Play(RectTransform root, float radius, Vector3 target,float AppearDuration, float MoveDuration, Ease AppearEase, Ease MoveEase,float delay,Action OnReachTarget = null)
    {
        // Generate a random position within the bounds of the parent
        Vector2 randomPosition = root.GetRandomPositionInRect();
        this.gameObject.SetActive(true);

        Sequence seq = DOTween.Sequence();

        rect.localScale = Vector3.zero;
        rect.anchoredPosition = Vector2.zero;

        GameService.PlaySound(soundIDAppear);

        Tween scaleUpTween = rect.DOScale(Vector3.one, AppearDuration).SetEase(AppearEase);
        Tween buzzOutTween = rect.DOAnchorPos(randomPosition, AppearDuration).SetEase(AppearEase);

        Vector3 center = root.position*0.7f + target*0.3f;
        Vector3[] path = new Vector3[2] { center, target };
        Tween moveTween = rect.DOPath(path, MoveDuration).SetEase(MoveEase);
        seq.SetDelay(delay);
        seq.Append(scaleUpTween);
        seq.Join(buzzOutTween);
        seq.Append(moveTween);

        seq.OnComplete(() =>
        {
            GameService.PlaySound(soundIDCollected);
            ReturnPool();
            OnReachTarget?.Invoke();
        });
    }

    public void ReturnPool()
    {
        this.gameObject.SetActive(false);
        this.rect.anchoredPosition = Vector2.zero;
    }
    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
