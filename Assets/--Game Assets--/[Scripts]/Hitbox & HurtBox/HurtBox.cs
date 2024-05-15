using UnityEngine;

public class HurtBox : MonoBehaviour
{
    public IGetHandlers _currentPlayer;

    private void Start()
    {
        if (GetComponentInParent<StateHandler>())
        {
            _currentPlayer = FindObjectOfType<DamageHandler>();
        }
        else if (GetComponentInParent<AI_StateHandler>())
        {
            _currentPlayer = FindObjectOfType<DamageHandler>();
        }
    }
}