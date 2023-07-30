using UnityEngine;
using Ironsource;
using System;

public class MyAdManager : MonoBehaviour
{
    public const string AdMob_APP_Key_Androi = "ca-app-pub-8225089705706111~7844261111";

#if UNITY_ANDROID
    public const string appKey = "18958c465";
#elif UNITY_IPHONE
    public const string appKey = "";
#else
    public const string appKey = "unexpected_platform";
#endif

    [SerializeField] IronsourceManager ironsourceManager;
    [SerializeField] float minimumInterstitialCallingTime = 30f;

    // Use this for initialization
    void Start()
    {
        ironsourceManager.BannerAdLoaded += MyBannerAdLoaded;

        // Init Ironsource
        ironsourceManager.Init(appKey);

        // When the application starts, call a banner ads at the bottom center of the device.
        ironsourceManager.LoadBanner();

        // Load interstitial at startup
        ironsourceManager.LoadInterstitial();

        /// When the player watches the rewarded ads, the package should notify the caller that
        /// whether the video has been watched until the end(which means the player has
        /// deserved the reward) or closed before the end.When the method that triggered the
        /// rewarded ads receives this callback, an amount of in-game coin should be given to the
        /// player if the reward is deserved(A simple coin counter on the top - right side of the screen
        /// will be enough.).
        ironsourceManager.RewardedVideoAdRewarded += MyRewardedVideoAdRewarded;
    }

    public void ManualInit()
    {
        ironsourceManager.Init(appKey);
    }

    bool isBannerON;
    private void MyBannerAdLoaded()
    {
        isBannerON = true;
    }

    public void ToggleBanner()
    {
        if (isBannerON) HideBanner();
        else ShowBanner();
    }

    public void ShowBanner()
    {
        isBannerON = true;
        ironsourceManager.ShowBanner();
    }    

    public void HideBanner()
    {
        isBannerON = false;
        ironsourceManager.HideBanner();
    }

    public void ShowInterstitialButton()
    {
        ShowInterstitial();
    }

    public bool ShowInterstitial()
    {
        // if 30 seconds has not passed since the launch of the application, reject the start call
        // and inform the caller.
        if (Time.realtimeSinceStartup < minimumInterstitialCallingTime)
        {
            Debug.LogWarning($"Interstitial must be called after minimum {minimumInterstitialCallingTime} seconds after app startup");
            return false;
        }

        ironsourceManager.ShowInterstitial();
        return true;
    }

    public void ShowRewarded()
    {
        ironsourceManager.ShowRewardedVideo();
    }

    private void MyRewardedVideoAdRewarded(IronSourcePlacement obj)
    {
        // Reward player with coin
    }
}