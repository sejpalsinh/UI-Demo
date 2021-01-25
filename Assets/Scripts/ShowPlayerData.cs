using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class ShowPlayerData : MonoBehaviour
{
    /*
    * Tasks
    * 1 Indentify which list have max number of records and according to that create PlayerDataToUi prefab and disable it
    * 2 Subscribe to PlayerDataToUi for show details and remove function
    * 3 On TeamLead, Senior, Junior button click get data from scriptable object and show to prefabs
    * 4 On Popup show disbale background
    * 5 On player add button show player addpopup
    */
    #region Variables
    [SerializeField] DataHolder dataHolder;
    [SerializeField] PlayerDataToUi playerDataPenel;
    List<PlayerDataToUi> playerDataPool;
    [SerializeField] Button showTeamLeadBtn, showJuniorBtn, showSeniorBtn , addPlayerBtn;
    [SerializeField] TextMeshProUGUI showTeamLeadTMP, showJuniorTMP, showSeniorTMP;
    [SerializeField] AddNewPlayer addNewPlayerCanvas;
    [SerializeField] PlayerDataToDetails playerDataToDetails;
    [SerializeField] AudioSource buttonSound;
    int sizeOfPenelPool;
    public Level loadedView;

    #endregion

    private void Awake()
    {
        dataHolder.LoadDataFromFile();
    }

    void Start()
    {
        playerDataPool = new List<PlayerDataToUi>();

        // Subscribe to DataHolder for refresh data on new player added
        dataHolder.playerAdded += RefreshOnPlayerAdd;

        // Check which list have max number of records
        if (dataHolder.teamLeaders.Count > dataHolder.seniorDevloper.Count)
        {
            if (dataHolder.teamLeaders.Count > dataHolder.juniorDeveloper.Count) { sizeOfPenelPool = dataHolder.teamLeaders.Count; }
            else { sizeOfPenelPool = dataHolder.juniorDeveloper.Count; }
        }
        else if(dataHolder.seniorDevloper.Count > dataHolder.juniorDeveloper.Count) { sizeOfPenelPool = dataHolder.seniorDevloper.Count; }
        else { sizeOfPenelPool = dataHolder.juniorDeveloper.Count; }

        // Create Instantiate Prefabs and disable
        for (int i=0; i<sizeOfPenelPool; i++)
        {
            PlayerDataToUi tempPenel = Instantiate(playerDataPenel, transform);
            tempPenel.removePlayer += RemovePlayerData;
            tempPenel.showPlayer += playerDataToDetails.ShowPlayerDataToDetails;
            tempPenel.gameObject.SetActive(false);
            playerDataPool.Add(tempPenel);
        }

        // Default show TeamLead data
        ShowTeamLeadData();
    }


    // On add player if number of player is greater than sizeOfPenelPool then add new prefab 
    public void AddExtraInstance()
    {
        PlayerDataToUi tempPenel = Instantiate(playerDataPenel, transform);
        tempPenel.removePlayer += RemovePlayerData;
        tempPenel.showPlayer += playerDataToDetails.ShowPlayerDataToDetails;
        tempPenel.gameObject.SetActive(false);
        tempPenel.DisableButtons();
        playerDataPool.Add(tempPenel);
        sizeOfPenelPool++;
    }
    
    // Show Junior data
    public void ShowJuniorData() 
    {
        // Cheange color of button background and text
        showJuniorBtn.image.color = Color.black;
        showJuniorTMP.color = Color.white;

        showTeamLeadBtn.image.color = Color.white;
        showTeamLeadTMP.color = Color.black;

        showSeniorBtn.image.color = Color.white;
        showSeniorTMP.color = Color.black;

        // If number of data is Greater than sizeOfPenelPool then add new prefab and add 
        if (dataHolder.juniorDeveloper.Count > sizeOfPenelPool){ AddExtraInstance(); }
        ShowData(dataHolder.juniorDeveloper);

        // Chenge loaded view flag
        loadedView = Level.JUNIOR;
    }
    public void ShowSeniorData() 
    {
        // Cheange color of button background and text
        showJuniorBtn.image.color = Color.white;
        showJuniorTMP.color = Color.black;

        showTeamLeadBtn.image.color = Color.white;
        showTeamLeadTMP.color = Color.black;

        showSeniorBtn.image.color = Color.black;
        showSeniorTMP.color = Color.white;

        // If number of data is Greater than sizeOfPenelPool then add new prefab and add 
        if (dataHolder.seniorDevloper.Count > sizeOfPenelPool) { AddExtraInstance(); }
        ShowData(dataHolder.seniorDevloper);

        // Chenge loaded view flag
        loadedView = Level.SENIOR;
    }
    public void ShowTeamLeadData() 
    {
        // Cheange color of button background and text
        showJuniorBtn.image.color = Color.white;
        showJuniorTMP.color = Color.black;

        showTeamLeadBtn.image.color = Color.black;
        showTeamLeadTMP.color = Color.white;

        showSeniorBtn.image.color = Color.white;
        showSeniorTMP.color = Color.black;

        // If number of data is Greater than sizeOfPenelPool then add new prefab and add 
        if (dataHolder.teamLeaders.Count > sizeOfPenelPool) { AddExtraInstance(); }
        ShowData(dataHolder.teamLeaders);

        // Chenge loaded view flag
        loadedView = Level.TEAMLEAD;
    }


    // Enable prefabs and set player data 
    public void ShowData(List<Player> players)
    {
        int i = 0;
        foreach (Player player in players)
        {
            playerDataPool[i].SetPlayerData(player, i + 1);
            playerDataPool[i].EnableButtons();
            playerDataPool[i].gameObject.SetActive(true);
            i++;
        }
        while (i < playerDataPool.Count)
        {
            playerDataPool[i].gameObject.SetActive(false);
            i++;
        }
    }

    // Remove data and refresh screen
    public void RemovePlayerData(Player player)
    {
        PlaySound();
        if (player.playerLevel == Level.TEAMLEAD)
        {
            dataHolder.RemoveTeamLeader(player);
            ShowTeamLeadData();
        }
        else if (player.playerLevel == Level.SENIOR) 
        { 
            dataHolder.RemoveSeniorDev(player);
            ShowSeniorData();
        }
        else
        { 
            dataHolder.RemoveJuniorDev(player);
            ShowJuniorData();
        }
    }

    // If added player and loaded screen is same then refresh
    public void RefreshOnPlayerAdd(Level playerLevel)
    {
        if(playerLevel == loadedView)
        {
            if (playerLevel == Level.TEAMLEAD) { ShowTeamLeadData();  }
            else if (playerLevel == Level.SENIOR) { ShowSeniorData(); }
            else { ShowJuniorData(); }
        }
    }

    // Pop Up add player canvas and disable background
    public void ShowAddPlayerCanvas()
    {

        addNewPlayerCanvas.gameObject.SetActive(true);
        DisableAllButtons();
    }
    // Close Add Player PopUp and enable background
    public void HideAddPlayerCanvas()
    {
        addNewPlayerCanvas.gameObject.SetActive(false);
        EnableAllButtons();
    }

    // Disable on PopUp
    public void DisableAllButtons()
    {
        showTeamLeadBtn.interactable = false;
        showJuniorBtn.interactable = false;
        showSeniorBtn.interactable = false;
        addPlayerBtn.interactable = false;
        foreach(PlayerDataToUi playerDataToUi in playerDataPool)
        {
            playerDataToUi.DisableButtons();
        }
    }
    //Enable on pop up close
    public void EnableAllButtons()
    {
        showTeamLeadBtn.interactable = true;
        showJuniorBtn.interactable = true;
        showSeniorBtn.interactable = true;
        addPlayerBtn.interactable = true;
        foreach (PlayerDataToUi playerDataToUi in playerDataPool)
        {
            playerDataToUi.EnableButtons();
        }
    }

    public void PlaySound()
    {
        buttonSound.Play();
    }

}
