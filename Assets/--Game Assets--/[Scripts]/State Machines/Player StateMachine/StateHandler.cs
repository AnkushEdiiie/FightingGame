using UnityEngine;

public class StateHandler : MonoBehaviour
{
    public StateMachine _stateMachine { get; set; }
    public InputReader _inputReader { get; set; }
    public AnimationController _animationController { get; set; }
    public Transform _playerTransform { get; set; }

    //Multiplayer uses
    public CharacterController _controller { get; set; }
    public Animator _animator { get; set; }

    #region Condition Variables
    [Header("Condition Variables")]
    public bool isAttacking;
    public bool isJumping;
    public bool isCrouching;
    public bool isMove;
    public bool isHit;
    public float moveSpeed { get; set; }
    #endregion

    #region States
    public PlayerIdleState _idleState { get; private set; }
    public PlayerWalkForwardState _walkForwardState { get; private set; }
    public PlayerWalkBackwardState _walkBackwardState { get; private set; }
    public PlayerJumpState _jumpState { get; private set; }
    public PlayerCrouchState _crouchState { get; private set; }
    public PlayerJabState _jabState { get; private set; }
    public PlayerPunchState _punchState { get; private set; }
    public PlayerKickState _kickState { get; private set; }
    public PlayerAxeKickState _axeKickState { get; private set; }
    public PlayerSweepState _sweepState { get; private set; }
    public PlayerLowKickState _lowKickState { get; private set; }
    public PlayerLowPunchState _lowPunchState { get; private set; }
    public PlayerDownSmashState _downSmashState { get; private set; }
    public PlayerJumpForwardState _jumpForwardState { get; private set; }
    public PlayerJumpBackwardState _jumpBackwardState { get; private set; }
    public PlayerHighKickState _highKickState { get; private set; }
    public PlayerHighPunchState _highPunchState { get; private set; }
    public PlayerHighSmashState _highSmashState { get; private set; }
    public PlayerJumpHitReactState _jumpHitReactState { get; private set; }
    public PlayerUppercutState _uppercutState { get; private set; }


    #endregion

    private void Awake()
    {
        _stateMachine = new StateMachine();
        _inputReader = GetComponent<InputReader>();
        _animationController = GetComponent<AnimationController>();

        #region States Initialization

        _idleState = new PlayerIdleState(this, _stateMachine, _inputReader, _animationController);
        _walkForwardState = new PlayerWalkForwardState(this, _stateMachine, _inputReader, _animationController);
        _walkBackwardState = new PlayerWalkBackwardState(this, _stateMachine, _inputReader, _animationController);
        _jumpState = new PlayerJumpState(this, _stateMachine, _inputReader, _animationController);
        _crouchState = new PlayerCrouchState(this, _stateMachine, _inputReader, _animationController);
        _jabState = new PlayerJabState(this, _stateMachine, _inputReader, _animationController);
        _punchState = new PlayerPunchState(this, _stateMachine, _inputReader, _animationController);
        _kickState = new PlayerKickState(this, _stateMachine, _inputReader, _animationController);
        _axeKickState = new PlayerAxeKickState(this, _stateMachine, _inputReader, _animationController);
        _sweepState = new PlayerSweepState(this, _stateMachine, _inputReader, _animationController);
        _lowKickState = new PlayerLowKickState(this, _stateMachine, _inputReader, _animationController);
        _lowPunchState = new PlayerLowPunchState(this, _stateMachine, _inputReader, _animationController);
        _downSmashState = new PlayerDownSmashState(this, _stateMachine, _inputReader, _animationController);
        _jumpForwardState = new PlayerJumpForwardState(this, _stateMachine, _inputReader, _animationController);
        _jumpBackwardState = new PlayerJumpBackwardState(this, _stateMachine, _inputReader, _animationController);
        _highKickState = new PlayerHighKickState(this, _stateMachine, _inputReader, _animationController);
        _highPunchState = new PlayerHighPunchState(this, _stateMachine, _inputReader, _animationController);
        _highSmashState = new PlayerHighSmashState(this, _stateMachine, _inputReader, _animationController);
        _jumpHitReactState = new PlayerJumpHitReactState(this, _stateMachine, _inputReader, _animationController);
        _uppercutState = new PlayerUppercutState(this, _stateMachine, _inputReader, _animationController);

        #endregion

    }
    private void Start()
    {
        _stateMachine.InitializedState(_idleState);
    }
    private void Update()
    {
        _stateMachine._currentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        _stateMachine._currentState.PhysicsUpdate();
    }
    private void LateUpdate()
    {
        transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
    }
}