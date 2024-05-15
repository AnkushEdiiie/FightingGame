
using UnityEngine;

[CreateAssetMenu(fileName = "AIData", menuName = "Data/AIData", order = 1)]
public class AIData : ScriptableObject
{
    public float idleSpeed = 0;
    public float walkSpeed = 2;
    public float runSpeed = 5;
    public float transitionDelay = 0.5f;
}

