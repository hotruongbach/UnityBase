using DG.Tweening;
using MyBox;
using System;
using UnityEngine;

namespace Monster.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIAnimationFlyinFlyout : UIAnimation
    {
        [SerializeField, ReadOnly] GameConfig gameConfig;
        /// <summary>
        /// Own CanvasGroup
        /// </summary>
        [SerializeField, ReadOnly] CanvasGroup canvasGroup;

        /// <summary>
        /// UI component will be animate by this script
        /// </summary>
        [SerializeField] RectTransform panel;

        /// <summary>
        /// Select duration type of this UI. Global setting is set from GameConfig scriptable object.
        /// </summary>
        [SerializeField] UIAnimDurationSetting animDurationSetting;

        /// <summary>
        /// Duration of in and out animation, in second
        /// </summary>
        [ConditionalField(nameof(animDurationSetting), false, UIAnimDurationSetting.Custom)][SerializeField] float Duration = 0.3f;
        float globalDuration => gameConfig.PopupAnimTime;

        [Separator("POSITIONS")]
        [Tooltip("Position that panel appear")]
        [SerializeField] Vector2 StartPosition = new Vector2(0,200);
        [Tooltip("Position that panel stay after animation")]
        [SerializeField] Vector2 StayPosition = new Vector2(0,0);
        [Tooltip("Position that panel move to disapear")]
        [SerializeField] Vector2 EndPosition = new Vector2(0, -200);

        protected override void OnValidate()
        {
            base.OnValidate();
            canvasGroup = GetComponent<CanvasGroup>();
#if UNITY_EDITOR
            gameConfig = SOFinder.FindGameConfig();
#endif
        }

        public override void PlayIn(Action onComplete = null)
        {
            float duration = FindSettingDuration(animDurationSetting);

            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1, duration);

            panel.anchoredPosition = StartPosition;
            panel.DOAnchorPos(StayPosition, duration).OnComplete(() =>
            {
                onComplete?.Invoke();
            });
        }

        public override void PlayOut(Action onComplete = null)
        {
            float duration = FindSettingDuration(animDurationSetting);
            canvasGroup.DOFade(0, duration);
            panel.DOAnchorPos(EndPosition, duration).OnComplete(() =>
            {
                onComplete?.Invoke();
            });
        }

        private float FindSettingDuration(UIAnimDurationSetting setting)
        {
            switch (setting)
            {
                case UIAnimDurationSetting.GlobalConfig:
                    return globalDuration;
                case UIAnimDurationSetting.Custom:
                    return Duration;
                default: return 0;
            }
        }
    }
    public enum UIAnimDurationSetting
{
    GlobalConfig = 0,
    Custom = 1,
}
}