using UnityEngine;
using UnityEngine.UI;

namespace Monster.UI
{
    public class PopupSetting : Window
    {
        [Header("POPUP")]
        [SerializeField] Toggle enableBgmToggle;
        [SerializeField] Toggle enableSfxToggle;
        [SerializeField] Toggle enableVibrateToggle;
        [SerializeField] ButtonBase policyButton;
        [SerializeField] ButtonBase termButton;
        [SerializeField] Button btnRate;
        [SerializeField] Button btnClose;
        private void Start()
        {
            policyButton.AddListener(OnPolicyButtonClick);
            termButton.AddListener(OnTermButtonClick);
            btnRate.onClick.AddListener(ShowRate);
            btnClose.onClick.AddListener(OnBackButtonClick);
        }
        void OnEnable()
        {
            enableBgmToggle.isOn = GameService.EnableMusic;
            enableSfxToggle.isOn = GameService.EnableSfx;
            enableVibrateToggle.isOn = GameService.EnableVibrate;

            enableBgmToggle.onValueChanged.AddListener(MusicToggleChanged);
            enableSfxToggle.onValueChanged.AddListener(SfxToggleChanged);
            enableVibrateToggle.onValueChanged.AddListener(VibrateToggleChanged);
        }
        void MusicToggleChanged(bool value)
        {
            GameService.EnableMusic = value;
        }

        void SfxToggleChanged(bool value)
        {
            GameService.EnableSfx = value;
        }
        void VibrateToggleChanged(bool value)
        {
            GameService.EnableVibrate = value;
        }

        void OnBackButtonClick()
        {
            GameService.ClosePopup();
        }
        void ShowRate()
        {
            GameService.ClosePopup();
            GameService.ShowPopup<RatePopup>();
        }
        public void OnPolicyButtonClick()
        {
            Application.OpenURL("https://sites.google.com/view/queen-city-policy");
        }
        public void OnTermButtonClick()
        {
            Application.OpenURL("https://sites.google.com/view/queen-city-policy");
        }
    }
}