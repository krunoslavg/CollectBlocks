using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementControllerData01", menuName = "Game entity component data/Movement data")]
public class MovementControllerData : GameEntityComponentData
{
    [SerializeField] bool _shouldMove = true;
    [SerializeField] float _moveSpeed = 2f;

    public bool ShouldMove { get => _shouldMove; }
    public float MoveSpeed { get => _moveSpeed; }
}
