using Monster.UI;

namespace Monster
{
    public class SettingButton : ButtonBase
    {
        protected override void OnClick()
        {
            base.OnClick();
            ShowSetting();
        }
        private void ShowSetting()
        {
            this.ShowPopup<PopupSetting>();
        }
    }
}