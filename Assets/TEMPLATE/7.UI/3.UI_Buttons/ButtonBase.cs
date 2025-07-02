using Monster.Audio;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Monster.UI
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Animator))]
    public class ButtonBase : MonoBehaviour
    {
        protected Button button;
        protected Animator animator;
        [SerializeField] ButtonTransitionType transition = ButtonTransitionType.Animation;
        [SerializeField] SoundID soundID;

        protected virtual void Awake()
        {
            button = GetComponent<Button>();
            if (transition == ButtonTransitionType.Animation)
            {
                animator = GetComponent<Animator>();

                button.transition = Selectable.Transition.Animation;
                animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            }

            button.onClick.AddListener(OnClick);
        }
        protected virtual void OnDestroy()
        {
            button.onClick.RemoveAllListeners();
        }
        protected virtual void OnClick()
        {
            PlaySound();
        }
        public void AddListener(UnityAction action)
        {

            if (button == null) button = GetComponent<Button>();
            button.onClick.AddListener(action);
        }
        protected virtual void PlaySound()
        {
            GameService.PlaySound(soundID);
        }

        public void SetInteract(bool isInteract)
        {
            button.interactable = isInteract;
        }
        public void ClosePopup()
        {
            Action OnPopupClose = null;
            this.ClosePopup(OnPopupClose);
        }
    }
}

public enum ButtonTransitionType
{
    None = 0,
    ColorTint = 1,
    Animation = 2,
    SpriteSwap = 3,
}