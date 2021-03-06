﻿using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Controller2D))]

public class MovingObject : MonoBehaviour
{
    private MovingObjectConfig config;
    private Controller2D controller;
    public Vector2 targetVelocity;
    private Vector2 adjustedVelocity;

    private void Awake()
    {
        controller = GetComponent<Controller2D>();
    }

    public Vector2 GetTargetVelocity() => targetVelocity;
    public Vector2 GetAdjustedVelocity() => adjustedVelocity;
    public void AssignConfiguration(MovingObjectConfig newConfig) => config = newConfig;
    public void MoveJittery(Vector2 directionalInput, float speed, ref Vector2 jitteryLocation, GameObject tankSprites)
    {
        targetVelocity = directionalInput * speed * Time.deltaTime;
        adjustedVelocity = controller.CalculateAdjustedVelocity(targetVelocity);
        jitteryLocation += adjustedVelocity;
        tankSprites.transform.position = PixelPerfectClamp.GetPixelPerfectClampV3(jitteryLocation, 16);
    }

    public void MoveSmoothly(Vector2 moveInput)
    {
        targetVelocity = moveInput * Time.deltaTime;
        adjustedVelocity = controller.CalculateAdjustedVelocity(targetVelocity);
        transform.position += (Vector3)adjustedVelocity;
    }
}