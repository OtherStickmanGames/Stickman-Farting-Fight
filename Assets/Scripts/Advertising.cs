using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
#endif

public class Advertising : MonoBehaviour
#if UNITY_ANDROID
    , IRewardedVideoAdListener
#endif
{
    public string AdMobId = "ca-app-pub-4512242896042473~4055268926";

    public System.Action onVideoClosed;
    public System.Action xyeta;


    public static int countSession;

    void Start()
    {
#if UNITY_ANDROID
        string appKey = "ccd80dd4e8dd8ba3c16c5b7c14c53de5da30829edb7d1e4f";
        //Appodeal.disableLocationPermissionCheck();
        Appodeal.setRewardedVideoCallbacks(this);
        Appodeal.initialize(appKey, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO);
#endif
    }

    public void Showadablata()
    {
        ShowAds();
    }

    public static void ShowAds()
    {
#if UNITY_ANDROID
        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
        }
        else if (Appodeal.isLoaded(Appodeal.INTERSTITIAL))
        {
            Appodeal.show(Appodeal.INTERSTITIAL);
        }
        else
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
        }
        countSession = 0;

        Appodeal.setAutoCache(Appodeal.REWARDED_VIDEO, false);
        Appodeal.cache(Appodeal.REWARDED_VIDEO);
#endif
    }

    public void onRewardedVideoLoaded(bool precache)
    {
        throw new System.NotImplementedException();
    }

    public void onRewardedVideoFailedToLoad()
    {
        throw new System.NotImplementedException();
    }

    public void onRewardedVideoShowFailed()
    {
        throw new System.NotImplementedException();
    }

    public void onRewardedVideoShown()
    {
        throw new System.NotImplementedException();
    }

    public void onRewardedVideoFinished(double amount, string name)
    {
        throw new System.NotImplementedException();
    }

    public void onRewardedVideoClosed(bool finished)
    {
        onVideoClosed?.Invoke();
    }

    public void onRewardedVideoExpired()
    {
        xyeta?.Invoke();
    }

    public void onRewardedVideoClicked()
    {
        throw new System.NotImplementedException();
    }
    // ============================================================

}
