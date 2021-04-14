using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollisionControllerData00", menuName = "Game entity component data/Collision controller data")] 
public class CollisionControllerData : GameEntityComponentData
{
    [SerializeField] ActiveState _collisionsActive;
    [SerializeField] bool _enableSpecificCollisionEvents = false;
    [SerializeField] List<CollisionEventsState> _collisionEventStates;

    public ActiveState CollisionsActive { get => _collisionsActive; }
    public bool EnableSpecificCollisionEvents { get => _enableSpecificCollisionEvents;}
    public List<CollisionEventsState> CollisionEventStates { get => _collisionEventStates; }
}


[System.Serializable]
public class CollisionEventsState
{
    [SerializeField] private CollisionEventType _eventType;
    [SerializeField] private ActiveState _enabled;

    public CollisionEventType EventType { get => _eventType;  }
    public ActiveState Enabled { get => _enabled; }
}
