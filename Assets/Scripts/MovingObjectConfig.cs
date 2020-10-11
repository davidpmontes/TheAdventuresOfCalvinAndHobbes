using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MovingObjectConfig", order = 1)]
public class MovingObjectConfig : ScriptableObject
{
    [Tooltip("Walking speed.")]
    public float walkingSpeed;

    [Tooltip("Running speed.")]
    public float runningSpeed;
}