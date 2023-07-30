using UnityEngine;
using System;
using System.Collections;

namespace Ironsource
{
    public class IronsourceManager : MonoBehaviour
    {
        public void Init(string _appID)
        {
            Debug.Log($"{typeof(IronsourceManager)} Init Called");

            //Dynamic config example
            IronSourceConfig.Instance.setClientSideCallbacks(true);

            string id = IronSource.Agent.getAdvertiserId();
            Debug.Log("unity-script: IronSource.Agent.getAdvertiserId : " + id);

            Debug.Log("unity-script: IronSource.Agent.validateIntegration");
            IronSource.Agent.validateIntegration();

            Debug.Log("unity-script: unity version" + IronSource.unityVersion());

            IronSource.Agent.setConsent(true);
            IronSource.Agent.setMetaData("do_not_sell", "false");
            IronSource.Agent.setMetaData("is_child_directed", "false");

            // Add Banner Events
            IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
            IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;
            IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent;
            IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent;
            IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
            IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;

            // Add Interstitial Events
            IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
            IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
            IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
            IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
            IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
            IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
            IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;

            //Add Rewarded Video Events
            IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
            IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
            IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
            IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
            IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
            IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
            IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
            IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;

            IronSourceEvents.onSdkInitializationCompletedEvent += delegate
            {
                Debug.Log("unity-script: SDK initialization completed");
                LoadBanner();

                //Launch test suite
                IronSource.Agent.launchTestSuite();
            };

            // SDK init
            Debug.Log("unity-script: IronSource.Agent.init");
            IronSource.Agent.init(_appID);
        }

        void OnApplicationPause(bool isPaused)
        {
            Debug.Log("unity-script: OnApplicationPause = " + isPaused);
            IronSource.Agent.onApplicationPause(isPaused);
        }

        #region /************* Banner API *************/
        public void LoadBanner()
        {
            // Load Banner example
            IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
        }

        public void ShowBanner()
        {
            // Eger IronSource.Agent paketi icerisinde ShowBanner gibi bir method varsa buraya konulabilir
        }

        public void HideBanner()
        {
            // Eger IronSource.Agent paketi icerisinde Hide ya da Close banner gibi bir method varsa buraya konulabilir
        }

        //Banner Events
        public event Action BannerAdLoaded;
        public event Action<IronSourceError> BannerAdLoadFailed;
        public event Action BannerAdClicked;
        public event Action BannerAdScreenPresented;
        public event Action BannerAdScreenDismissed;
        public event Action BannerAdLeftApplication;

        void BannerAdLoadedEvent()
        {
            BannerAdLoaded?.Invoke();
            Debug.Log("unity-script: I got BannerAdLoadedEvent");
        }

        void BannerAdLoadFailedEvent(IronSourceError error)
        {
            BannerAdLoadFailed?.Invoke(error);
            Debug.Log("unity-script: I got BannerAdLoadFailedEvent, code: " + error.getCode() + ", description : " + error.getDescription());
        }

        void BannerAdClickedEvent()
        {
            BannerAdClicked?.Invoke();
            Debug.Log("unity-script: I got BannerAdClickedEvent");
        }

        void BannerAdScreenPresentedEvent()
        {
            BannerAdScreenPresented?.Invoke();
            Debug.Log("unity-script: I got BannerAdScreenPresentedEvent");
        }

        void BannerAdScreenDismissedEvent()
        {
            BannerAdScreenDismissed?.Invoke();
            Debug.Log("unity-script: I got BannerAdScreenDismissedEvent");
        }

        void BannerAdLeftApplicationEvent()
        {
            BannerAdLeftApplication?.Invoke();
            Debug.Log("unity-script: I got BannerAdLeftApplicationEvent");
        }
        #endregion

        #region /************* Interstitial API *************/
        public void LoadInterstitial()
        {
            Debug.Log("unity-script: LoadInterstitial");
            IronSource.Agent.loadInterstitial();
        }

        public void ShowInterstitial()
        {
            Debug.Log("unity-script: ShowInterstitial");
            if (IronSource.Agent.isInterstitialReady())
            {
                IronSource.Agent.showInterstitial();
            }
            else
            {
                Debug.Log("unity-script: IronSource.Agent.isInterstitialReady - False");
            }
        }

        /************* Interstitial Delegates *************/
        public event Action InterstitialAdReady;
        public event Action<IronSourceError> InterstitialAdLoadFailed;
        public event Action InterstitialAdShowSucceeded;
        public event Action<IronSourceError> InterstitialAdShowFailed;
        public event Action InterstitialAdClicked;
        public event Action InterstitialAdOpened;
        public event Action InterstitialAdClosed;

        void InterstitialAdReadyEvent()
        {
            InterstitialAdReady?.Invoke();
            Debug.Log("unity-script: I got InterstitialAdReadyEvent");
        }

        void InterstitialAdLoadFailedEvent(IronSourceError error)
        {
            InterstitialAdLoadFailed?.Invoke(error);
            Debug.Log("unity-script: I got InterstitialAdLoadFailedEvent, code: " + error.getCode() + ", description : " + error.getDescription());
        }

        void InterstitialAdShowSucceededEvent()
        {
            InterstitialAdShowSucceeded?.Invoke();
            Debug.Log("unity-script: I got InterstitialAdShowSucceededEvent");
        }

        void InterstitialAdShowFailedEvent(IronSourceError error)
        {
            InterstitialAdShowFailed?.Invoke(error);
            Debug.Log("unity-script: I got InterstitialAdShowFailedEvent, code :  " + error.getCode() + ", description : " + error.getDescription());
        }

        void InterstitialAdClickedEvent()
        {
            InterstitialAdClicked?.Invoke();
            Debug.Log("unity-script: I got InterstitialAdClickedEvent");
        }

        void InterstitialAdOpenedEvent()
        {
            InterstitialAdOpened?.Invoke();
            Debug.Log("unity-script: I got InterstitialAdOpenedEvent");
        }

        void InterstitialAdClosedEvent()
        {
            InterstitialAdClosed?.Invoke();
            Debug.Log("unity-script: I got InterstitialAdClosedEvent");
        }
        #endregion

        #region /************* RewardedVideo API *************/ 
        public void ShowRewardedVideo()
        {
            Debug.Log("unity-script: ShowRewardedVideo");
            if (IronSource.Agent.isRewardedVideoAvailable())
            {
                IronSource.Agent.showRewardedVideo();
            }
            else
            {
                Debug.Log("unity-script: IronSource.Agent.isRewardedVideoAvailable - False");
            }
        }

        /************* RewardedVideo Delegates *************/
        public event Action<bool> RewardedVideoAvailabilityChanged;
        public event Action RewardedVideoAdOpenedt;
        public event Action<IronSourcePlacement> RewardedVideoAdRewarded;
        public event Action RewardedVideoAdClosed;
        public event Action RewardedVideoAdStarted;
        public event Action RewardedVideoAdEnded;
        public event Action<IronSourceError> RewardedVideoAdShowFailed;
        public event Action<IronSourcePlacement> RewardedVideoAdClicked; 

        void RewardedVideoAvailabilityChangedEvent(bool canShowAd)
        {
            RewardedVideoAvailabilityChanged?.Invoke(canShowAd);
            Debug.Log("unity-script: I got RewardedVideoAvailabilityChangedEvent, value = " + canShowAd);
        }

        void RewardedVideoAdOpenedEvent()
        {
            RewardedVideoAdOpenedt?.Invoke();
            Debug.Log("unity-script: I got RewardedVideoAdOpenedEvent");
        }

        void RewardedVideoAdRewardedEvent(IronSourcePlacement ssp)
        {
            RewardedVideoAdRewarded?.Invoke(ssp);
            Debug.Log("unity-script: I got RewardedVideoAdRewardedEvent, amount = " + ssp.getRewardAmount() + " name = " + ssp.getRewardName());
        }

        void RewardedVideoAdClosedEvent()
        {
            RewardedVideoAdClosed?.Invoke();
            Debug.Log("unity-script: I got RewardedVideoAdClosedEvent");
        }

        void RewardedVideoAdStartedEvent()
        {
            RewardedVideoAdStarted?.Invoke();
            Debug.Log("unity-script: I got RewardedVideoAdStartedEvent");
        }

        void RewardedVideoAdEndedEvent()
        {
            RewardedVideoAdEnded?.Invoke();
            Debug.Log("unity-script: I got RewardedVideoAdEndedEvent");
        }

        void RewardedVideoAdShowFailedEvent(IronSourceError error)
        {
            RewardedVideoAdShowFailed?.Invoke(error);
            Debug.Log("unity-script: I got RewardedVideoAdShowFailedEvent, code :  " + error.getCode() + ", description : " + error.getDescription());
        }

        void RewardedVideoAdClickedEvent(IronSourcePlacement ssp)
        {
            RewardedVideoAdClicked?.Invoke(ssp);
            Debug.Log("unity-script: I got RewardedVideoAdClickedEvent, name = " + ssp.getRewardName());
        }
        #endregion
    }
}