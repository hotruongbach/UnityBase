using UnityEngine;
using UnityEngine.Events;

public class AdsInvoker : MonoBehaviour
{
    [SerializeField] AdsType type;
    //[SerializeField] AdScreenType adScreenType = AdScreenType.Default;
    public void ShowAds(AdsType type)
    {
#if ADS_ENABLED

#endif
    }
    public void ShowAds()
    {
#if ADS_ENABLED
        if (type == AdsType.AdsScreen)
        {
           
        }

        if (type == AdsType.NativeCollapse)
        {
           
        }
#endif
    }
    public void ShowInter(UnityAction<bool> onSuccess, string position)
    {
#if ADS_ENABLED
        // if (type == AdsType.Inter)
        // {
        //     
        // }
#endif
    }
    public void ShowReward(UnityAction<bool> onFailed, UnityAction<bool> onSuccess, string position)
    {
#if ADS_ENABLED
        // if (type == AdsType.Reward)
        // {
        //   
        // }
#endif
    }
}
public enum AdsType
{
    None = 0,
    AdsScreen = 1,
    Inter = 2,
    Reward = 3,
    NativeCollapse = 4,
}
