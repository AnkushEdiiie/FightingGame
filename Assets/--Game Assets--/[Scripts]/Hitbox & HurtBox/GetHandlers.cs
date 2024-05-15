[System.Serializable]
public class HandlersDetails
{
    public StateHandler _Player_A_Handler;
    public StateHandler _Player_B_Handler;

    public AI_StateHandler _AI_Handler;

    public DamageHandler _damageHandler;
}
public interface IGetHandlers
{
    HandlersDetails _details();
}