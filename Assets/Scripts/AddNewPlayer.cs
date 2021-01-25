using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddNewPlayer : MonoBehaviour
{
    /*
     * Tasks
     * 1 Add new player
     * 2 Validation of inputs
     * 3 Show Error box on invalid input
     * 4 Show Player Added Box on data added
     */
    #region Variables
    [SerializeField] TMP_InputField In_nameTMP, In_emailTMP, In_phoneNoTMP, In_expTMP, In_empIdTMP, In_descriptionTMP;
    [SerializeField] TMP_Dropdown levelDropdown;
    [SerializeField] Toggle maleToggle, femaleToggle;
    [SerializeField] DataHolder dataHolder;
    [SerializeField] Button add_Btn, close_Btn;
    [SerializeField] GameObject playerAddedPopUp,dataErrorPopUp;
    [SerializeField] TextMeshProUGUI errorMsgTmp;
    #endregion


    // Add Player data to scriptable object
    public void AddPlayerData()
    {

        float playerExperience = 0f;
        // Validation for float value
        if (!IsValidFloatForExperiece(In_expTMP.text.ToString() , ref playerExperience))
        {
            return;
        }
        string playerName = In_nameTMP.text;
        string playerEmail = In_emailTMP.text;
        string playerMobileNumber = In_phoneNoTMP.text;
        string playerDiscription = In_descriptionTMP.text;
        string playerId = In_empIdTMP.text;
        // Validation for float all enterd data
        if (!DataIsNotValidate(playerName, playerEmail, playerMobileNumber,playerId, playerDiscription))
        {
            return;
        }
        Gender playerGender = maleToggle.isOn ? Gender.Male : Gender.Female;
        Level playerLevel = (Level)levelDropdown.value;
        Player player = new Player(playerName, playerEmail, playerExperience, playerDiscription, playerId, playerGender, playerLevel, playerMobileNumber);
        
        // Show popup for data added
        ShowPopUp();

        // Check Player level and add data to scriptable object
        if (playerLevel == Level.TEAMLEAD)
        {
            dataHolder.AddTeamLeader(player);
        }
        else if (playerLevel == Level.SENIOR)
        {
            dataHolder.AddSeniorDev(player);
        }
        else
        {
            dataHolder.AddJuniorDev(player);
        }

        // Clear all data from canvas when data added
        ClearInputArea();
    }


    // Float value validation from input
    private bool IsValidFloatForExperiece(string exp, ref float playerExperience)
    {
        try
        {
            playerExperience = (float)Convert.ToDouble(exp);
        }
        catch
        {
            ShowError("Please Enter Valid Experience\n Ex 1 or 2.5 something !");
            return false;
        }
        
        return true;
    }

    // Input data validation
    private bool DataIsNotValidate(string playerName, string playerEmail, string playerMobileNumber,string playerId,string playerDiscription)
    {
        // Name Validation
        if(playerName.Length==0)
        {
            ShowError("Please Enter Valid Name !");
            return false;
        }
        for(int i=0; i<playerName.Length; i++)
        {
            if(Char.IsDigit(playerName[i]))
            {
                ShowError("Please Enter Valid Name !");
                return false;
            }
        }

        // Email Validation

        if (playerEmail.Length == 0)
        {
            ShowError("Please Enter Valid Email !");
            return false;
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(playerEmail);
            if(addr.Address != playerEmail)
            {
                ShowError("Please Enter Valid Email Address !");
                return false;
            }
        }
        catch (FormatException)
        {
            ShowError("Please Enter Valid Email Address !");
            return false;
        }

        // Mobile NUmber Validation
        if (playerMobileNumber.Length != 10)
        {
            ShowError("Please Enter 10 Digit Mobile Number !");
            return false;
        }
        for (int i = 0; i < playerMobileNumber.Length; i++)
        {
            if (!Char.IsDigit(playerMobileNumber[i]))
            {
                ShowError("Please Enter Valid Mobile Number !");
                return false;
            }
        }

        // Id validation
        if (playerId.Length != 4)
        {
            ShowError("Please Enter 4 Digit ID Number !");
            return false;
        }
        for (int i = 0; i < playerId.Length; i++)
        {
            if (!Char.IsDigit(playerId[i]))
            {
                ShowError("Please Enter Valid ID Number !");
                return false;
            }
        }

        // Discription Validation
        if(playerDiscription.Length<1)
        {
            ShowError("Please Enter Valid Discription !");
            return false;
        }

        return true;
    }

    // Clear inputs
    private void ClearInputArea()
    {
        In_nameTMP.text = "";
        In_emailTMP.text = "";
        In_phoneNoTMP.text = "";
        In_expTMP.text = "";
        In_descriptionTMP.text = "";
        In_empIdTMP.text = "";
        levelDropdown.value = 0;
    }

    // Disable on pop up show
    private void MakeDisable()
    {
        In_nameTMP.interactable = false;
        In_emailTMP.interactable = false;
        In_phoneNoTMP.interactable = false;
        In_expTMP.interactable = false;
        In_descriptionTMP.interactable = false;
        In_empIdTMP.interactable = false;
        maleToggle.interactable = false;
        levelDropdown.interactable = false;
        add_Btn.interactable = false;
        close_Btn.interactable = false;
    }

    // Disable on pop up closed
    private void MakeEnable()
    {
        In_nameTMP.interactable = true;
        In_emailTMP.interactable = true;
        In_phoneNoTMP.interactable = true;
        In_expTMP.interactable = true;
        In_descriptionTMP.interactable = true;
        In_empIdTMP.interactable = true;
        maleToggle.interactable = true;
        levelDropdown.interactable = true;
        add_Btn.interactable = true;
        close_Btn.interactable = true;
    }

    // Player added pop up
    public void ShowPopUp()
    {
        playerAddedPopUp.SetActive(true);
        MakeDisable();
    }
    public void ClosePopUp()
    {
        playerAddedPopUp.SetActive(false);
        MakeEnable();
    }

    // Error pop up
    public void ShowError(string erromsg)
    {
        errorMsgTmp.SetText(erromsg);
        dataErrorPopUp.SetActive(true);
        MakeDisable();
    }
    public void CloseError()
    {
        dataErrorPopUp.SetActive(false);
        MakeEnable();
    }

}
