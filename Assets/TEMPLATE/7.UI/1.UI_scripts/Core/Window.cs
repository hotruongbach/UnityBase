using System;
using Monster;
using Monster.UI;
using MyBox;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

namespace Monster.UI
{
    [RequireComponent(typeof(CanvasGroup), typeof(Canvas), typeof(GraphicRaycaster))]
    [RequireComponent(typeof(AdsInvoker))]
    public abstract class Window : MonoBehaviour
    {
        [SerializeField, HideInInspector]
        protected Canvas m_canvas;

        [SerializeField, HideInInspector]
        AdsInvoker adsInvoker;

        [SerializeField]
        bool useCanvasControll = true;

        [SerializeField]
        public bool keepHistory = true;

        // [Header("UI ANIMATION CONFIG")]
        // [SerializeField] AnimationType animType = AnimationType.None;
        // [ConditionalField(nameof(animType), false, AnimationType.Script)][SerializeField]   protected UIAnimation uiAnimation;
        // [ConditionalField(nameof(animType), false, AnimationType.Timeline)][SerializeField] protected PlayableDirector director;
        // [ConditionalField(nameof(animType), false, AnimationType.Timeline)][SerializeField] protected TimelineAsset timelineIn;
        // [ConditionalField(nameof(animType), false, AnimationType.Timeline)][SerializeField] protected TimelineAsset timelineOut;

        protected Action showCompleteCallback;
        protected Action hideCompleteCallback;
        protected bool IsNewSpawn = true;
        bool IsHiding = false;
        public bool IsShowing =>
            useCanvasControll ? m_canvas.enabled : gameObject.activeInHierarchy;

        private void OnValidate()
        {
            // if (director != null)
            // {
            //     director.timeUpdateMode = DirectorUpdateMode.UnscaledGameTime;
            //     director.playOnAwake = false;
            // }

            m_canvas = GetComponent<Canvas>();
            adsInvoker = GetComponent<AdsInvoker>();
        }

        private void Awake()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes the Window
        /// </summary>
        ///
        public virtual void Initialize() { }

        public virtual void Show(bool show)
        {
            if (useCanvasControll)
            {
                m_canvas.enabled = show;
            }
            else
            {
                gameObject.SetActive(show);
            }
        }

        /// <summary>
        /// Show UI to user/ Set active true to this game object, play show animation
        /// </summary>
        public virtual void Show(Action onComplete = null, object param = null)
        {
            showCompleteCallback = onComplete;
            Show(true);
            transform.SetAsLastSibling();

            PlayAnim(onComplete);
            adsInvoker.ShowAds();
        }

        /// <summary>
        /// Call when close upper popup to reveal this UI
        /// </summary>
        /// <param name="onComplete"></param>
        public virtual void OnReveal(Action onComplete = null, object param = null)
        {
            Show(true);
            transform.SetAsLastSibling();

            adsInvoker.ShowAds();

            OnAnimationComplete();
            onComplete?.Invoke();
        }

        void PlayAnim(Action onComplete = null)
        {
            // switch (animType)
            // {
            //     case AnimationType.None:
            //         showCompleteCallback?.Invoke();
            //         OnAnimationComplete();
            //         break;
            //     case AnimationType.Script:
            //         uiAnimation.PlayIn(OnComplete);
            //         void OnComplete()
            //         {
            //             onComplete?.Invoke();
            //             OnAnimationComplete();
            //         }
            //         break;
            //     case AnimationType.Timeline:
            //         director.stopped += OnDirectorInComplete;
            //         director.playableAsset = timelineIn;
            //         director?.Play();
            //         break;
            // }
        }

        protected void OnDirectorInComplete(PlayableDirector director)
        {
            showCompleteCallback?.Invoke();
            OnAnimationComplete();
            director.stopped -= OnDirectorInComplete;
        }

        protected void OnDirectorOutComplete(PlayableDirector director)
        {
            hideCompleteCallback?.Invoke();
            Show(false);
            director.stopped -= OnDirectorOutComplete;
            IsHiding = false;
        }

        /// <summary>
        /// Called after animation/director complete
        /// </summary>
        /// <param name="OnAdsShowSuccess"></param>
        /// <param name="OnAdsShowFailed"></param>
        public virtual void OnAnimationComplete(
            Action OnAdsShowSuccess = null,
            Action OnAdsShowFailed = null
        ) { }

        /// <summary>
        /// Hides the Window
        /// </summary>
        public virtual void Hide(Action onComplete = null)
        {
            if (!this.gameObject.activeInHierarchy || !m_canvas.enabled)
                return;
            if (IsHiding)
                return;
            IsHiding = true;

            // if (animType == AnimationType.Script)
            // {
            //     Action OnComplete = () =>
            //     {
            //         Show(false);
            //         IsHiding = false;
            //         onComplete?.Invoke();
            //     };
            //     uiAnimation.PlayOut(OnComplete);
            // }
            // else if (animType == AnimationType.Timeline)
            // {
            //     //set callback for director
            //     hideCompleteCallback = onComplete;

            //     director.stopped += OnDirectorOutComplete;
            //     director.playableAsset = timelineOut;
            //     director?.Play();
            // }
            // else
            // {
            //     IsHiding = false;
            //     Show(false);
            //     onComplete?.Invoke();
            // }
            IsHiding = false;
            Show(false);
            onComplete?.Invoke();
            ResetData();
        }

        public void HideImmediate()
        {
            Hide(null);
        }

        /// <summary>
        /// ResetData for show next time
        /// </summary>
        public virtual void ResetData() { }

        public void ShowInter(UnityAction<bool> onSuccess, string position)
        {
            adsInvoker.ShowInter(onSuccess, position);
        }

        public void ShowRewardAds(
            UnityAction<bool> onFailed,
            UnityAction<bool> onSuccess,
            string position
        )
        {
            adsInvoker.ShowReward(onFailed, onSuccess, position);
        }

        public void RegistNewUIAnimation(UIAnimation component)
        {
            // this.uiAnimation = component;
        }
    }

    public enum AnimationType
    {
        None = 0,
        Script = 1,
        Timeline = 2,
    }
}

public static class WindowExtension
{
    public static void ShowPopup<T>(this MonoBehaviour mono, Action onComplete = null)
        where T : Window
    {
        GameService.ShowPopup<T>(onComplete);
    }

    public static void ShowView<T>(
        this MonoBehaviour mono,
        bool keepInHistory = true,
        Action onComplete = null
    )
        where T : Window
    {
        GameService.ShowView<T>(onComplete);
    }

    /// <summary>
    /// Close popup is showing on screen.
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="onComplete"></param>
    public static void ClosePopup(this MonoBehaviour mono, Action onComplete = null)
    {
        GameService.ClosePopup(onComplete);
    }
}
