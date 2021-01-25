using System;
[Serializable]
public enum Gender
{
    Male,
    Female
}
[Serializable]
public enum Level
{
    TEAMLEAD,
    SENIOR,
    JUNIOR
}
[Serializable]
public class Player{
    public string playerName;
    public string playerEmail;
    public string playerMobileNumber;
    public float playerExperience;
    public string playerDiscription;
    public string playerId;
    public Gender playerGender;
    public Level playerLevel;

    public Player(string playerName,string playerEmail,float playerExperience,string playerDiscription,string playerId,Gender playerGender,Level playerLevel,string playerMobileNumber)
    {
        this.playerName = playerName;
        this.playerEmail = playerEmail;
        this.playerExperience = playerExperience;
        this.playerDiscription = playerDiscription;
        this.playerId = playerId;
        this.playerGender = playerGender;
        this.playerLevel = playerLevel;
        this.playerMobileNumber = playerMobileNumber;
    }
}
