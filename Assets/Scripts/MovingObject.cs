using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Controller2D))]

public class MovingObject : MonoBehaviour
{
    private MovingObjectConfig config;
    private Controller2D controller;
    private Vector2 velocity;
    private float velocityXSmoothing;
    private float velocityYSmoothing;

    private void Awake()
    {
        controller = GetComponent<Controller2D>();
    }

    public Vector2 GetVelocity() => velocity;
    public void AssignConfiguration(MovingObjectConfig newConfig)
    {
        config = newConfig;
    }

    public void CalculateVelocity(Vector2 directionalInput)
    {
        float targetVelocityX = directionalInput.x * config.walkingSpeed;
        float targetVelocityY = directionalInput.y * config.walkingSpeed;

        velocity.x = Mathf.SmoothDamp(velocity.x,
                              targetVelocityX,
                              ref velocityXSmoothing,
                              Time.deltaTime);

        velocity.y = Mathf.SmoothDamp(velocity.y,
                              targetVelocityY,
                              ref velocityYSmoothing,
                              Time.deltaTime);
    }

    public void Move(float speed)
    {
        controller.Move(velocity * speed * Time.deltaTime);
    }
}