using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEntityStateController : MonoBehaviour
{
    /***************************************** INSPECTOR VARIABLES *********************************************/
    [Header("Values")]
    [SerializeField] private bool _displayStateGui = false;
    [SerializeField] private bool _isInitialized = false;
    [SerializeField] private bool _isSetup = false;
    [SerializeField] private bool _canSwitchStates = true;
    [SerializeField] private bool _canExternSystemSwitchState = true;
    [Header("State controller flag")]
    [SerializeField] protected StateControllerType _stateControllerType;

    /***************************************** PROTECTED INSPECTOR VARIABLES ***********************************************/
    [Header("States")]
    [SerializeField] protected StateType _mainState;
    [SerializeField] protected StateType _currentState;
    [SerializeField] protected StateType _previousState;
    [SerializeField] protected List<GameEntityStateStruct> _possibleStatesStruct;

    
    /***************************************** PROTECTED VARIABLES ***********************************************/
    protected GameObject _stateDisplayGO;
    protected GameEntity _gameEntity;

    /***************************************** PRIVATE VARIABLES ***********************************************/
    private Dictionary<string, StateType> _stateTypeDict = new Dictionary<string, StateType> { };

    /***************************************** PUBLIC PROPERTIES ***********************************************/
    public GameEntity GameEntity
    {
        get
        {
            if (_gameEntity != null)
                return _gameEntity;
            else
                return _gameEntity = GetComponent<GameEntity> ();
        }
    } 
    public StateControllerType StateControllerType
    {
        get
        {
            return _stateControllerType;
        } 
    }
    public List<GameEntityStateStruct> PossibleStatesStruct
    {
        get
        {
            return _possibleStatesStruct;
        }
    }
    public bool IsInitialized { get => _isInitialized; }
    public bool IsSetup { get => _isSetup; }
    public bool CanExternSystemSwitchState { get => _canExternSystemSwitchState; }


    /************************************ PUBLIC VIRTUAL METHODS ***********************************************/
    public virtual void Init()
    {
        _isInitialized = false;

        _gameEntity = gameObject.GetComponent<GameEntity>();
        _stateTypeDict.Add("Disabled", StateType.Disabled);
        _stateTypeDict.Add("Ready", StateType.Ready);
        _stateTypeDict.Add("Spawning", StateType.Spawning);
        _stateTypeDict.Add("Paused", StateType.Paused);
        _stateTypeDict.Add("SearchForBox", StateType.SearchForBox);
        _stateTypeDict.Add("SearchForBoxStorage", StateType.SearchForBoxStorage);
        _stateTypeDict.Add("StoreBox", StateType.StoreBox);
        _stateTypeDict.Add("TakeBox", StateType.TakeBox);

        if (_gameEntity == null)
            _gameEntity = gameObject.GetComponentInParent<GameEntity>();
     
        if (_gameEntity == null)
        {
            Debug.LogError("GameEntity: " + _gameEntity.name + ":<GameEntityStateController> ().Setup():Game entity is null!");
            return;
        }
        _isInitialized = true;
    }

    public virtual void Setup(ScriptableObject p_sourceDataObject)
    {
        _isSetup = false;

        if (p_sourceDataObject == null)
        {
            Debug.LogError("GameEntity: " + _gameEntity.name + ":<GameEntityStateController> ().Setup():Data object is null!");
            return;
        }
        _isSetup = true;
    }
    
    public virtual void ToggleMainState (bool p_value)
    {
        if (_gameEntity == null)
            return;

        if (p_value)
        {
            if (!_gameEntity.IsActive ())
                _gameEntity.SetActive (true);

            SwitchState (_mainState);
        }
        else
            SwitchState (StateType.Disabled);
    }

    public virtual void SetInitialState()
    {
        SwitchState (StateType.Disabled);
    }
   
    public virtual void SwitchState(string p_stateType)
    {
        SwitchState(_stateTypeDict[p_stateType], null);
    }

    public virtual void SwitchState(StateType p_stateType)
    {
        SwitchState(p_stateType, null);
    }

    public virtual void SwitchState(StateType p_stateType, params object[] p_params)
    {
        if (!_canSwitchStates)
            return;

        if (_possibleStatesStruct == null || _possibleStatesStruct.Count < 1)
            return;

        foreach (GameEntityStateStruct l_possibleStateStruct in _possibleStatesStruct)
        {
            if (l_possibleStateStruct.State == p_stateType)
            {
                _previousState = _currentState;
                _currentState = l_possibleStateStruct.State;

                if (l_possibleStateStruct.EventToInvoke != null)
                    l_possibleStateStruct.EventToInvoke.Invoke ();
                break;
            }
        }
        if (_displayStateGui && _stateDisplayGO != null)
        {
            _stateDisplayGO.name = _currentState.ToString();
        } 
    }
       
    public virtual bool CheckEntityData<T>(GameEntityData p_data, out T p_castedData) where T : GameEntityData
    {
        p_castedData = null;

        if (p_data == null)
        {
            Debug.LogError("Game entity: " + _gameEntity.name + ":<GameEntityStateController> ().Setup(): Can't setup! Data object is null!");
            return false;
        }

        T l_data = (T)p_data;

        if (l_data == null)
        {
            Debug.LogError("Game entity: " + _gameEntity.name + ":<GameEntityStateController> ().Setup(): Data asset provided, but couldn't get data!!");
            return false;
        }

        p_castedData = l_data;
        return true;
    }

    public bool IsEntityInState(StateType p_stateType)
    { 
        if (_currentState != p_stateType)
            return false;
        else
            return true;
    }

    public void ToggleStateSwitching(bool p_value) 
    {
        _canSwitchStates = p_value;
    }

    public void ToggleExternStateSwitching(bool p_value)
    {
        _canExternSystemSwitchState = p_value;
    }
}

[System.Serializable]
public struct GameEntityStateStruct
{
    [SerializeField] StateType _state;
    [SerializeField] UnityEvent _unityEvent;

    public StateType State { get => _state; }
    public UnityEvent EventToInvoke { get => _unityEvent; }
}

