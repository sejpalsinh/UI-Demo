
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerDataToUi : MonoBehaviour
{
    /*
    * Tasks
    * 1 Show Player basic data on player scroll prefab
    * 2 On delete button click invoke function to subscribers
    * 3 On Show button click invoke function to subscribers
    */
    #region Variables
    [SerializeField] TextMeshProUGUI playerName, playerEmail, playerId, playerDataIndex;
    [SerializeField] Sprite maleImage,femaleImage,otherImage;
    [SerializeField] Image genderImage;
    [SerializeField] Button removeBtn, showBtn;
    private Player thisPlayer;

    // Subscribers :- ShowPlayerData for remove player from scriptable object
    public UnityAction<Player> removePlayer;
    // Subscribers :- ShowPlayerData for showing full data 
    public UnityAction<Player> showPlayer;

    #endregion


    // Show player data to scroll view prefabs from player object
    public void SetPlayerData(Player playerData, int index)
    {
        thisPlayer = playerData;
        if (playerData.playerGender == Gender.Male){ genderImage.sprite = maleImage; }
        else{genderImage.sprite = femaleImage; }
        playerName.text = playerData.playerName;
        playerEmail.text = playerData.playerEmail;
        playerId.text = playerData.playerId.ToString();
        playerDataIndex.text = index.ToString();
    } 

    // Function for delete button 
    public void DeleteData()
    {
        removePlayer.Invoke(thisPlayer);

    }
    // Function for show button 
    public void ShowData()
    {
        showPlayer.Invoke(thisPlayer);
    }
    // Disable when other popup is open
    public void DisableButtons()
    {
        removeBtn.interactable = false;
        showBtn.interactable = false;
    }
    // Disable when other popup is closed
    public void EnableButtons()
    {
        removeBtn.interactable = true;
        showBtn.interactable = true;
    }
}
