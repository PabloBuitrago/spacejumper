using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdScript : MonoBehaviour {

    BannerView bannerAd;

    // Use this for initialization
    void Start () {

        if (SceneManager.GetActiveScene().name == "GameScene")
            showBannerAd();
        else
            showBannerAdP();

    }

    private void showBannerAd()
    {
        string adID = "ca-app-pub-6593375758470328/6104128497";

        //***For Testing in the Device***
        AdRequest request = new AdRequest.Builder()
       .AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
       .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")  // My test device.
       .Build();

        //***For Production When Submit App***
        //AdRequest request = new AdRequest.Builder().Build();

        bannerAd = new BannerView(adID, AdSize.SmartBanner, AdPosition.Top);
        bannerAd.LoadAd(request);
    }

    private void showBannerAdP()
    {
        string adID = "ca-app-pub-6593375758470328/7580861690";

        //***For Testing in the Device***
        AdRequest request = new AdRequest.Builder()
       .AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
       .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")  // My test device.
       .Build();

        //***For Production When Submit App***
        //AdRequest request = new AdRequest.Builder().Build();

        bannerAd = new BannerView(adID, AdSize.Banner, AdPosition.Top);
        bannerAd.LoadAd(request);
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnDestroy()
    {
        bannerAd.Destroy();
    }
}
