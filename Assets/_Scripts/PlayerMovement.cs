using System;
using UnityEngine;

/// <summary>
/// Assumes players are circular.
/// Assumes the player uses world origin as the center of world. 
/// Class that listens to GameInput and handles player movement within the Arena. 
/// </summary>
public class PlayerMovement: MonoBehaviour
{
    [Header("External References")]
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private Arena arena;
    
    [Header("Player Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;
    
    // the radius of the circular player
    private float _playerRadius;
    // the max radius that player could travel from the center of the arena.
    private float _maxMoveRadius;
    private float _maxMoveRadiusSqr;
    private void Awake()
    {
        // cache player radius (get from sprite bounds)
        _playerRadius = playerSpriteRenderer.bounds.extents.x;
    }

    private void Start()
    {
        // calculate the max move radius
        // doing this on Start to ensure arena not properly initialized yet.
        _maxMoveRadius = arena.InnerRadius - _playerRadius;
        _maxMoveRadiusSqr = _maxMoveRadius * _maxMoveRadius;
    }


    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 moveVector2 = GameInput.Instance.GetPlayerMoveVector();
        Vector3 moveVector = new Vector3(moveVector2.x, moveVector2.y, 0f);
        // clamp the move vector magnitude to 1,
        // but this allows magnitude less than 1 in case of controller input.
        moveVector = Vector3.ClampMagnitude(moveVector, 1f);
        
        // calculate the target position
        Vector3 targetPos = transform.position + moveSpeed * Time.deltaTime * moveVector;
        
        // clamp target pos within the circular arena
        // if the dist from center of arena to targetPos > maxMoveRadius,
        Vector3 dirToTarget = targetPos - arena.ArenaCenter;
        if (dirToTarget.sqrMagnitude > _maxMoveRadiusSqr)
        {
            // find the furthest point the player can travel in the direction to target,
            // and use that as the new targetPos
            targetPos = arena.ArenaCenter + dirToTarget.normalized * _maxMoveRadius;
        }
        
        transform.position = targetPos;
    }
}
