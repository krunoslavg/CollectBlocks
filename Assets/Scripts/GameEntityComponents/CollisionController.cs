using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CollisionEventType
{
    Neutral,  
    OnCollisionEnter,
    OnTriggerEnter,
    OnCollisionExit,
    OnTriggerExit,
    OnCollisionStay,
    OnTriggerStay
}


public class CollisionController : GameEntityComponent
{
    /***************************************** INSPECTOR VARIABLES ***********************************************/
    [Space(10)]
    [SerializeField] ActiveState _collisionsActive;
    [Space(10)]
    [SerializeField] bool _enableSpecificEvents = false;
    [SerializeField] List<CollisionEventsState> _collisionEventStates;

    [Header("General collision event")]
    [SerializeField] CollisionUnityEvent _generalCollisionEvent;

    [Header("Specific collision events")]
    [SerializeField] OnCollisionEnter _onCollisionEnter;
    [SerializeField] OnTriggerEnter _onTriggerEnter;
    [Space(10)]
    [SerializeField] OnCollisionExit _onCollisionExit;
    [SerializeField] OnTriggerExit _onTriggerExit;
    [Space(10)]
    [SerializeField] OnCollisionStay _onCollisionStay;
    [SerializeField] OnTriggerStay _onTriggerStay;


    /***************************************** PRIVATE VARIABLES ***********************************************/
    private GameEntity _collidedGameEntity; 

    private CollisionEventType _collisionEventType;


    /***************************************** PROPERTIES ********************************************************/
    public GameEntity CollidedGameEntity { get { return _collidedGameEntity; } }
    public bool CollisionsEnabled
    {
        get { return _collisionsActive == ActiveState.On; }
        set
        {
            if (value) _collisionsActive = ActiveState.On;
            else _collisionsActive = ActiveState.Off;
        }
    }
    public CollisionEventType CollisionEventType
    {
        get
        {
            return _collisionEventType;
        }
    }


    /***************************************** UNITY METHODS ***************************************************/
    private void OnEnable()
    {
        _componentType = GameEntityComponentType.CollisionController;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (_collisionsActive == ActiveState.Off || _gameEntity == null)
            return;

        _collidedGameEntity = other.gameObject.GameEntity();

        if (_collidedGameEntity != null)
            TriggerCollisionEvent(CollisionEventType.OnCollisionEnter, _collidedGameEntity);

    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (_collisionsActive == ActiveState.Off || _gameEntity == null)
            return;

        _collidedGameEntity = other.gameObject.GameEntity();

        if (_collidedGameEntity != null)
            TriggerCollisionEvent(CollisionEventType.OnCollisionExit, _collidedGameEntity);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (_collisionsActive == ActiveState.Off || _gameEntity == null)
            return;

        _collidedGameEntity = other.gameObject.GameEntity();

        if (_collidedGameEntity != null)
            TriggerCollisionEvent(CollisionEventType.OnCollisionStay, _collidedGameEntity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_collisionsActive == ActiveState.Off || _gameEntity == null)
            return;

        _collidedGameEntity = other.gameObject.GameEntity();

        if (_collidedGameEntity != null)
            TriggerCollisionEvent(CollisionEventType.OnTriggerEnter, _collidedGameEntity);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (_collisionsActive == ActiveState.Off || _gameEntity == null)
            return;

        _collidedGameEntity = other.gameObject.GameEntity();

        if (_collidedGameEntity != null)
            TriggerCollisionEvent(CollisionEventType.OnTriggerExit, _collidedGameEntity);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (_collisionsActive == ActiveState.Off || _gameEntity == null)
            return;

        _collidedGameEntity = other.gameObject.GameEntity();

        if (_collidedGameEntity != null)
            TriggerCollisionEvent(CollisionEventType.OnTriggerStay, _collidedGameEntity);
    }


    /***************************************** PUBLIC METHODS **************************************************/
    public override void Setup(GameEntityComponentData p_data)
    {
        if (p_data == null)
            return;

        _collisionEventType = CollisionEventType.Neutral;

        CollisionControllerData l_data = null;

        if (!CheckComponentData(p_data, out l_data))
            return;

        base.Setup(p_data);

        _collisionsActive = l_data.CollisionsActive;
        _enableSpecificEvents = l_data.EnableSpecificCollisionEvents;
        _collisionEventStates = l_data.CollisionEventStates;
    }

    public override void Reset()
    {
        _collisionEventType = CollisionEventType.Neutral;
    }


    /***************************************** PRIVATE METHODS **************************************************/
    private void TriggerCollisionEvent(CollisionEventType p_collisionEventType, GameEntity p_collidedEntity)
    {
        _generalCollisionEvent.Invoke(p_collisionEventType, p_collidedEntity);

        if (!_enableSpecificEvents)
            return;

        if (!CheckIfEventEnabled(p_collisionEventType))
            return;

        switch (p_collisionEventType)
        {
            case CollisionEventType.OnCollisionEnter:
                _onCollisionEnter.Invoke(p_collisionEventType, p_collidedEntity);
                break;
            case CollisionEventType.OnTriggerEnter:
                _onTriggerEnter.Invoke(p_collisionEventType, p_collidedEntity);
                break;
            case CollisionEventType.OnTriggerStay:
                _onTriggerStay.Invoke(p_collisionEventType, p_collidedEntity);
                break;
            case CollisionEventType.OnCollisionStay:
                _onCollisionStay.Invoke(p_collisionEventType, p_collidedEntity);
                break;
            case CollisionEventType.OnTriggerExit:
                _onTriggerExit.Invoke(p_collisionEventType, p_collidedEntity);
                break;
            case CollisionEventType.OnCollisionExit:
                _onCollisionExit.Invoke(p_collisionEventType, p_collidedEntity);
                break;
            default:
                break;
        }

        print(p_collidedEntity.name);
    }

    private bool CheckIfEventEnabled(CollisionEventType p_collisionEventType)
    {
        foreach (var l_eventState in _collisionEventStates)
            if (l_eventState.EventType == p_collisionEventType && l_eventState.Enabled == ActiveState.On)
                return true;
        return false;
    }
}



[System.Serializable]
public class CollisionUnityEvent : UnityEvent<CollisionEventType, GameEntity> { }
[System.Serializable] 
public class OnCollisionEnter : UnityEvent<CollisionEventType, GameEntity> { }
[System.Serializable]
public class OnTriggerEnter : UnityEvent<CollisionEventType, GameEntity> { }
[System.Serializable]
public class OnCollisionExit : UnityEvent<CollisionEventType, GameEntity> { }
[System.Serializable]
public class OnTriggerExit : UnityEvent<CollisionEventType, GameEntity> { }
[System.Serializable]
public class OnCollisionStay : UnityEvent<CollisionEventType, GameEntity> { }
[System.Serializable]
public class OnTriggerStay : UnityEvent<CollisionEventType, GameEntity> { }
