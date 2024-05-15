using UnityEngine;
public class DamageHandler : MonoBehaviour, IGetHandlers
{
    [SerializeField] ParticleSystem[] hitEffects;

    public StateHandler _stateHandlerPlayerA;
    public StateHandler _stateHandlerPlayerB;

    public AI_StateHandler _aiStateHandler;

    public HandlersDetails _details;

    private void Start()
    {
        if(GameManager_Old.instance.GameMode == GameType.P1vsP2)
        {
            _stateHandlerPlayerA = GameObject.FindGameObjectWithTag("P1").GetComponent<StateHandler>();
            _stateHandlerPlayerB = GameObject.FindGameObjectWithTag("P2").GetComponent<StateHandler>();

            _details._Player_A_Handler = _stateHandlerPlayerA;
            _details._Player_B_Handler = _stateHandlerPlayerB;
            _details._damageHandler = this;
        }
        else if(GameManager_Old.instance.GameMode == GameType.P1vsComp)
        {
            _stateHandlerPlayerA = GameObject.FindGameObjectWithTag("P1").GetComponent<StateHandler>();
            _aiStateHandler = GameObject.FindGameObjectWithTag("P2").GetComponent<AI_StateHandler>();

            _details._Player_A_Handler = _stateHandlerPlayerA;
            _details._AI_Handler = _aiStateHandler;
            _details._damageHandler = this;
        }
    }

    HandlersDetails IGetHandlers._details()
    {
        return _details;
    }

    private void PlayHitEffect(int effectIndex, Vector3 position)
    {
        GameObject hitParticle = Instantiate(hitEffects[effectIndex].gameObject, position, Quaternion.identity);
        Destroy(hitParticle, 1f);
    }

    public void SetDamageToBothPlayer(StateHandler PlayerA, StateHandler PlayerB, int playerIndex, Vector3 position)
    {
        if (PlayerA == null || PlayerB == null)
            return;

        if(PlayerA._stateMachine._currentState == PlayerA._jabState)
        {
            PlayHitEffect(0, position);
            HealthManager.instance.TakeDamage(playerIndex, 5);
        }
        else if (PlayerA._stateMachine._currentState == PlayerA._punchState)
        {
            PlayHitEffect(2, position);
            HealthManager.instance.TakeDamage(playerIndex, 10);
        }
        else if (PlayerA._stateMachine._currentState == PlayerA._kickState)
        {
            PlayHitEffect(2, position);
            HealthManager.instance.TakeDamage(playerIndex, 5);
        }
        else if (PlayerA._stateMachine._currentState == PlayerA._axeKickState)
        {
            PlayHitEffect(2, position);
            HealthManager.instance.TakeDamage(playerIndex, 10);
        }
        else if (PlayerA._stateMachine._currentState == PlayerA._sweepState)
        {
            PlayHitEffect(1, position);
            HealthManager.instance.TakeDamage(playerIndex, 10);
        }
        else if (PlayerA._stateMachine._currentState == PlayerA._uppercutState)
        {
            PlayHitEffect(1, position);
            HealthManager.instance.TakeDamage(playerIndex, 5);
        }
    }

    public void SetDamageToPlayer(AI_StateHandler AI, StateHandler Player, int playerIndex, Vector3 position)
    {
        if (AI == null)
            return;

        if (AI.stateMachine.CurrentState == AI.LightPunchState)
        {
            if (Player._stateMachine._currentState == Player._walkBackwardState)
            {
                //player.stateMachine.ChangeState(player.BlockState);
                //PlayHitEffect(1);
            }
            else
            {
                //player.stateMachine.ChangeState(player.LightHitState);
                PlayHitEffect(0, position);
                HealthManager.instance.TakeDamage(playerIndex, 5);
            }
        }


        else if (AI.stateMachine.CurrentState == AI.HardPunchState)
        {
            if (Player._stateMachine._currentState == Player._walkBackwardState)
            {
                //player.stateMachine.ChangeState(player.BlockState);
                PlayHitEffect(1, position);
            }
            else
            {
                //player.stateMachine.ChangeState(player.LightHitState);
                PlayHitEffect(2, position);
                HealthManager.instance.TakeDamage(playerIndex, 10);
            }
        }


        else if (AI.stateMachine.CurrentState == AI.LightKickState)
        {
            if (Player._stateMachine._currentState == Player._walkBackwardState)
            {
                //player.stateMachine.ChangeState(player.BlockState);
                //PlayHitEffect(1);
            }
            else
            {
                //player.stateMachine.ChangeState(player.KnockdownState);
                PlayHitEffect(2, position);
                HealthManager.instance.TakeDamage(playerIndex, 10);
            }
        }
    }

    public void SetDamageToAI(StateHandler Player, AI_StateHandler AI, int playerIndex, Vector3 position)
    {
        if (Player == null)
            return;

        if (Player._stateMachine._currentState == Player._jabState)
        {
            AI.stateMachine.ChangeState(AI.LightHitState);
            PlayHitEffect(2,position);
            HealthManager.instance.TakeDamage(playerIndex, 5);
        }
        else if (Player._stateMachine._currentState == Player._punchState)
        {
            AI.stateMachine.ChangeState(AI.LightHitState);
            PlayHitEffect(1, position);
            HealthManager.instance.TakeDamage(playerIndex, 10);
        }
        else if (Player._stateMachine._currentState == Player._axeKickState)
        {
            AI.stateMachine.ChangeState(AI.KnockdownState);
            PlayHitEffect(2, position);
            HealthManager.instance.TakeDamage(playerIndex, 10);
        }
        else if (Player._stateMachine._currentState == Player._kickState)
        {
            AI.stateMachine.ChangeState(AI.KnockdownState);
            PlayHitEffect(1, position);
            HealthManager.instance.TakeDamage(playerIndex, 10);
        }
        else if (Player._stateMachine._currentState == Player._uppercutState)
        {
            AI.stateMachine.ChangeState(AI.KnockdownState);
            PlayHitEffect(2, position);
            HealthManager.instance.TakeDamage(playerIndex, 10);
        }
    }
}