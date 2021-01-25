
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerDataToDetails : MonoBehaviour
{
    /*
    * Tasks
    * 1 Show Player details on full screen
    * 2 Close on back button
    */
    #region Variables

    [SerializeField] TextMeshProUGUI nameTMP, emailTMP, expTMP, mobileTMP, descriptionTMP, idTMP, lvlTMP;
    [SerializeField] Image genderIMG;
    [SerializeField] Sprite maleIMG, femaleIMG;

    #endregion

    // Set data to Ui Elements from player Object
    public void ShowPlayerDataToDetails(Player player)
    {
        nameTMP.SetText(player.playerName);
        emailTMP.SetText(player.playerEmail);
        expTMP.SetText(((int)player.playerExperience).ToString() + "Year +");
        mobileTMP.SetText(player.playerMobileNumber);
        descriptionTMP.SetText(player.playerDiscription);
        idTMP.SetText(player.playerId);
        lvlTMP.SetText(player.playerLevel.ToString());
        if(player.playerGender == Gender.Male)
        {
            genderIMG.sprite = maleIMG;
        }
        else
        {
            genderIMG.sprite = femaleIMG;
        }
        gameObject.SetActive(true);
    }


    // Disable screen | Go back 
    public void BackToMainScreen()
    {
        gameObject.SetActive(false);
    }
}
