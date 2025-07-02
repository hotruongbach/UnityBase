using Monster.UI;

namespace Monster.FEATURES.DailyGift
{
    public class DailyGiftButton : ButtonBase
    {
        protected override void OnClick()
        {
            base.OnClick();
            ShowPopupDailyGift();
        }

        private void ShowPopupDailyGift()
        {
            this.ShowPopup<PopupDailyGift>();
        }
    }
}