using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEntityComponent : MonoBehaviour
{
    /**************************************** INSPECTOR VARIABLES **********************************************/
    [SerializeField] protected bool _initOnEnable;
    [SerializeField] protected bool _isInitialized = false;
    [SerializeField] protected bool _isSetup = false;
    [Space(10)]
    [SerializeField] protected GameEntityComponentType _componentType = GameEntityComponentType.None; // Just default value;



    /**************************************** PROTECTED VARIABLES **********************************************/
    protected GameEntity _gameEntity;
    

    /**************************************** PUBLIC PROPERTIES ************************************************/
    public GameEntity GameEntity  { get => _gameEntity; }
    public GameEntityComponentType ComponentType { get => _componentType; }
    public bool IsInitialized { get => _isInitialized; }
    public bool IsSetup { get => _isSetup; }


    /***************************************** PUBLIC METHODS **************************************************/

    public virtual void Init ()
    {
        _gameEntity = gameObject.GetComponent<GameEntity> ();

        if (_gameEntity == null)
            _gameEntity = gameObject.GetComponentInParent<GameEntity> (); 

        if (_gameEntity != null)
            _isInitialized = true;
    }

    public virtual void Init (params object[] components)
    {
        _gameEntity = (GameEntity)components[0];

        if (_gameEntity == null)
            _gameEntity = gameObject.GetComponent<GameEntity>();

        if (_gameEntity == null)
            _gameEntity = gameObject.GetComponentInParent<GameEntity>();

        if (_gameEntity != null)
            _isInitialized = true;
    }

    public virtual void Setup ()
    {
        _isSetup = true;
    }
    
    public virtual void Setup(GameEntityComponentData p_data)
    {
        if (p_data != null)
            _isSetup = true;
    }

    public virtual void Reset()
    {
    }
    
    public virtual bool CheckComponentData<T>(GameEntityComponentData p_data, out T p_castedData) where T : GameEntityComponentData
    {
        p_castedData = null;

        if (p_data == null)
        {
            Debug.LogError("Game entity: " + _gameEntity.name + "'s component:<"+ this + "> ().Setup():Can't setup! Data object is null!");
            return false;
        }

        T l_data = (T)p_data;

        if (l_data == null)
        {
            Debug.LogError("Game entity: " + _gameEntity.name + "'s component:<" + this + "> ().Setup():Data asset provided, but couldn't get data!!");            
            return false;
        }

        p_castedData = l_data;
        return true;
    }
}

