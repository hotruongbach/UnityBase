using Template.UI;

public class OnlineGiftButton : ButtonBase
{
    protected override void OnClick()
    {
        base.OnClick();
        ShowPopupOnlineGift();
    }

    private void ShowPopupOnlineGift()
    {
        this.ShowPopup<PopupOnlineGift>();
    }
}
