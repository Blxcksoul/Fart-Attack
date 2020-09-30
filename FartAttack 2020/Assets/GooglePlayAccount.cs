using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System.Text;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;
using System;

public class GooglePlayAccount : MonoBehaviour
{
    public Text Status;
    public string saveData;
    public static string[] dataArray;
    public string TestVariable;
    public static GooglePlayAccount instance { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
    }

    public void Login()
    {
        string playerId = Social.localUser.userName;

        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Status.text = "Logged in as " + Social.localUser.userName;
            }
            else
            {
                Status.text = "Account not authorised";
            }
        });
    }

    public void Logout()
    {
        PlayGamesPlatform.Instance.SignOut();
        Status.text = "Status: Not logged in";
    }

    public void Guest()
    {
        Status.text = "Logged in as Guest";
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("Level Select");
    }

    private bool isSaving;

    public void OpenSave(bool saving, int slotIndex)
    {
        Debug.Log("Open Save");

        if (Social.localUser.authenticated)
        {
            isSaving = saving;
            if (slotIndex == 1)
            {
                ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution("SaveSlot1", GooglePlayGames.BasicApi.DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, SaveGameOpened);
            }
            else if (slotIndex == 2)
            {
                ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution("SaveSlot2", GooglePlayGames.BasicApi.DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, SaveGameOpened);
            }
            else if (slotIndex == 3)
            {
                ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution("SaveSlot3", GooglePlayGames.BasicApi.DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, SaveGameOpened);
            }
        }
    }

    private void SaveGameOpened(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            Debug.Log("SaveGameOpened");

            if (isSaving) //writting
            {
                byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(GetSaveString());
                SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().WithUpdatedDescription("Saved at" + DateTime.Now.ToString()).Build();
                
                ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(meta, update, data, SaveUpdate);
            }
            else //reading
            {
                ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(meta, SaveRead);
            }
        }
    }

    private void SaveRead(SavedGameRequestStatus status, byte[] data)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            saveData = System.Text.ASCIIEncoding.ASCII.GetString(data);
            LoadSaveString();
        }
    }

    private void SaveUpdate(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        Debug.Log(status);
    }

    private string GetSaveString()
    {
        string r = TestVariable;

        return r;
    }

    public void LoadSaveString()
    {
        dataArray = saveData.Split('|');
    }
}
