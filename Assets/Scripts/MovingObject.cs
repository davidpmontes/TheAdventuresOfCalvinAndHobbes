using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Controller2D))]

public class MovingObject : MonoBehaviour
{
    private MovingObjectConfig config;
    private Controller2D controller;
    private Vector2 targetVelocity;
    private Vector2 adjustedVelocity;

    private void Awake()
    {
        controller = GetComponent<Controller2D>();
    }

    public Vector2 GetTargetVelocity() => targetVelocity;
    public Vector2 GetAdjustedVelocity() => adjustedVelocity;
    public void AssignConfiguration(MovingObjectConfig newConfig)
    {
        config = newConfig;
    }


    public void Move(Vector2 directionalInput, float speed)
    {
        targetVelocity = directionalInput * speed * Time.deltaTime;
        adjustedVelocity = controller.Move(targetVelocity);
    }
}