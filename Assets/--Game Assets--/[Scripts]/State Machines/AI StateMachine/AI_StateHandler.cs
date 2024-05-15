using UnityEngine;

public class AI_StateHandler : MonoBehaviour
{
    public AIStateMachine stateMachine { get; set; }
    public CharacterController controller { get; set; }
    public Animator AiAnim { get; set; }
    public Transform player { get; set; }

    public DamageHandler _damageHandler;

    public float VelocityX;
    public float VelocityZ;
    public float acceleration = 5f;
    public float decceleration = 6f;

    [Header("Data")]
    [SerializeField] private AIData m_AIData;

    #region State Variable
    public AIIdleState IdleState { get; private set; }
    public AIWalkState WalkState { get; private set; }  
    public AILightKickState LightKickState { get; private set; }
    public AIKnockdownState KnockdownState { get; private set; }
    public AILightHitState LightHitState { get; private set; }
    public AIHardPunchState HardPunchState { get; private set; }
    public AILightPunchState LightPunchState { get; private set; }

    #endregion

    #region Condition Variables
    [Header("Condition Variables")]
    public bool isAttacking;
    public bool isJumping;
    public bool isCrouching;
    public bool isMove;
    #endregion

    private void Awake()
    {
        AiAnim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        stateMachine = new AIStateMachine();

        _damageHandler = FindObjectOfType<DamageHandler>();

        #region Initializing State Variable
        IdleState = new AIIdleState(this, stateMachine, m_AIData);
        WalkState = new AIWalkState(this, stateMachine, m_AIData);
        HardPunchState = new AIHardPunchState(this, stateMachine, m_AIData);
        LightKickState = new AILightKickState(this, stateMachine, m_AIData);
        KnockdownState = new AIKnockdownState(this, stateMachine, m_AIData);
        LightHitState = new AILightHitState(this, stateMachine, m_AIData);
        LightPunchState = new AILightPunchState(this, stateMachine, m_AIData);
        #endregion 
    }

    public void Start()
    {
        if (GameObject.FindGameObjectWithTag("P1"))
        {
            player = GameObject.FindGameObjectWithTag("P1").transform;
        }
        stateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }
    private void LateUpdate()
    {
        transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
    }
}