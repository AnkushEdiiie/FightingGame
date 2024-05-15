using Unity.VisualScripting;
using UnityEngine;
public interface IPlayer
{
    PlayerDetails PlayerDetails();
    AIDetails AIdetails();
}
[System.Serializable]
public class PlayerDetails
{
    public StateHandler playerHandler;
    public AI_StateHandler AiHandler;
    public string PlayerName;
}

[System.Serializable]
public class AIDetails
{
    public AI_StateHandler AiHandler;
    public StateHandler playerHandler;
    public string PlayerName;
}