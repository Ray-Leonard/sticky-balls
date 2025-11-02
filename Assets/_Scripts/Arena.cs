using System;
using UnityEngine;


/// <summary>
/// Assumes Arena is circular. 
/// Could have different shaped arenas in the future.
/// </summary>
public class Arena : MonoBehaviour
{
    [SerializeField] private SpriteRenderer innerArenaSpriteRenderer;
    private float _innerRadius;
    public float InnerRadius => _innerRadius;

    private Vector3 _arenaCenter;
    public Vector3 ArenaCenter => _arenaCenter;
    
    private void Awake()
    {
        // get the inner radius of the arena
        _innerRadius = innerArenaSpriteRenderer.bounds.extents.x;
        
        // get the center of the arena
        _arenaCenter = innerArenaSpriteRenderer.bounds.center;
    }
}
