using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "All Data", menuName = "Player Data Store")]
public class DataHolder : ScriptableObject
{
    /*
     * Tasks
     * 1 Add and Remove data from List
     * 2 Notify to the subscribers when new Player added
     */
    #region Variables

    public List<Player> teamLeaders;
    public  List<Player> seniorDevloper;
    public  List<Player> juniorDeveloper;
    bool isFileInuse;
    //Subscribers :- ShowPlayeData for Resfreshing data when new player add
    public UnityAction<Level> playerAdded;

    #endregion


    #region Add and Remove Teamlead, Senior,Junior data  
    public void AddTeamLeader(Player player)
    {
        teamLeaders.Add(player);
        if(playerAdded!=null)
        {
            playerAdded.Invoke(Level.TEAMLEAD);
        }
        SaveToFile();
    }
    public   void RemoveTeamLeader(Player player)
    {
        if(teamLeaders.Contains(player))
        {
            teamLeaders.Remove(player);
        }
        SaveToFile();
    }
    public   void AddSeniorDev(Player player)
    {
        seniorDevloper.Add(player);
        if (playerAdded != null)
        {
            playerAdded.Invoke(Level.SENIOR);
        }
        SaveToFile();
    }
    public   void RemoveSeniorDev(Player player)
    {
        if (seniorDevloper.Contains(player))
        {
            seniorDevloper.Remove(player);
        }
        SaveToFile();
    }
    public   void AddJuniorDev(Player player)
    {
        juniorDeveloper.Add(player);
        if (playerAdded != null)
        {
            playerAdded.Invoke(Level.JUNIOR);
        }
        SaveToFile();
    }
    public   void RemoveJuniorDev(Player player)
    {
        if (juniorDeveloper.Contains(player))
        {
            juniorDeveloper.Remove(player);
        }
        SaveToFile();
    }

    #endregion

    #region Save and Load data from file

    void SaveToFile()
    {
        if(isFileInuse)
        {
            return;
        }
        isFileInuse = true;
        string filePath = Path.Combine(Application.persistentDataPath, "uitask.json");
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("File Created !");
            File.Create(filePath);
        }
        var json = JsonUtility.ToJson(this);
        File.WriteAllText(filePath, json);
        isFileInuse = false;
    }


    public void LoadDataFromFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "uitask.json");
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("File not found!");
            return;
        }

        var json = File.ReadAllText(filePath);
        JsonUtility.FromJsonOverwrite(json, this);
    }
    #endregion
}
