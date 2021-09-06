using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayfabManager : MonoBehaviour
{

    string accountID = "empty";


    // Start is called before the first frame update
    void Start()
    {
        Login();

    }

    private void Update()
    {
        //ShowPlayerID();
        //Debug.Log(accountID);
    }
    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = "Abed",
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);

        
    }

    void OnSuccess(LoginResult result)
    {
        ShowPlayerID();
        Debug.Log("successful login/new_account");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("error while logging in");
        Debug.Log(error.GenerateErrorReport());

    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> { new StatisticUpdate { StatisticName = "High Score", Value = score} }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Leadeboardsent");
    }

    void ShowPlayerID()
    {
        GetAccountInfoRequest getAccountInfoRequest = new GetAccountInfoRequest();

        PlayFabClientAPI.GetAccountInfo(getAccountInfoRequest, OnGetAccountSuccess, OnGetAccountError);


    }

    private void OnGetAccountError(PlayFabError obj)
    {
        throw new NotImplementedException();
    }

    private void OnGetAccountSuccess(GetAccountInfoResult getAccountInfoResult)
    {

        accountID = getAccountInfoResult.AccountInfo.CustomIdInfo.CustomId;
        Debug.Log("User ID is:" + accountID);
    }
}
