using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameEntity : MonoBehaviour
{
    /**************************************** INSPECTOR VARIABLES *********************************************/

    [Header("Init and setup")]
    [SerializeField] bool _initOnEnable;
    [SerializeField] float _initDelay = 0.0f;
    [Header("Flags")]
    [SerializeField] bool _isInitialized = false;
    [SerializeField] bool _isSetup = false;

    [Header("State controller and components data")]
    [SerializeField] GameEntityData _gameEntityData;

    [Header("Components")]
    [SerializeField] GameEntityStateController _stateController;
    [SerializeField] List<GameEntityComponent> _gameEntityComponents;



    /***************************************** PRIVATE VARIABLES ***********************************************/
    private Transform _rootTransform;
    private string _startingLayerName;
    private bool l_dataOk = true;


    /***************************************** PUBLIC PROPERTIES **********************************************/
  
    public bool InitOnEnable { get { return _initOnEnable; } set { _initOnEnable = value; } }
    public float InitDelay { get { return _initDelay; } set { _initDelay = value; } }

    public GameEntityData GameEntityData { get { return _gameEntityData; } }
    public Transform RootTransform { get { if (_rootTransform == null ) _rootTransform = transform;  return _rootTransform; } }
    public float RootRotation { get => _rootTransform.eulerAngles.z; set => _rootTransform.rotation = Quaternion.Euler(_rootTransform.eulerAngles.x, _rootTransform.eulerAngles.y, value);  }
    public Vector2 RootPosition { get { return new Vector2(transform.position.x, transform.position.y);  } set { transform.position = new Vector3(value.x, value.y, transform.position.z);  } }
    public GameEntityStateController StateController { get { return _stateController; } }
 
    public bool IsInitialized { get => _isInitialized; }
    public bool IsSetup { get => _isSetup; }

    void OnEnable ()
    { 
        if (_initOnEnable)
        {
            SelfInit (_initDelay);
        }
    }

    /**************************************** PUBLIC METHODS ***************************************************/
    public void SelfInit (float p_delay = 0f)
    {
        Debug.Log("Game entity: " + gameObject.name + ":<GameEntity> ().SelfInit(): InitOnEnable is true. Starting self initialization!!!");
        if (p_delay > 0)
        {
            StartCoroutine(SelfInitRoutine(p_delay));
            return;
        }

        if (!IsActive ())
            SetActive (true);

        Init();
        Setup(_gameEntityData);

        if (_stateController != null)
            _stateController.ToggleMainState(true);
    }

    public void Init ()
    {
        _init ();
    }

    public void Setup()
    { 
        _setup();
    }

    public void Setup (ScriptableObject p_dataObject)
    { 
        if (p_dataObject == null)         
            return;
        
        _gameEntityData = (GameEntityData) p_dataObject;

        _setup();
    }

    public void Reset ()
    {
        //if (_stateController != null)
        //    _stateController.Reset();
    }

    public void SetActive (bool p_value)
    {
        if (!p_value)
            StopAllCoroutines ();
        gameObject.SetActive (p_value);
    }

    public bool IsActive ()
    {
        if (gameObject == null)
            return false;

        return gameObject.IsActive();
    }

    public void DisableAll()
    {
        if (_stateController != null)
            _stateController.StopAllCoroutines();

        foreach (GameEntityComponent l_component in _gameEntityComponents)
            l_component.StopAllCoroutines();
    }

    public T GetStateControllerAsT<T> () where T : GameEntityStateController
    {
        if (_stateController == null)
            return null;
        if (_stateController is T)
            return (T) _stateController;
        else
            return null;
    }

    public T GetStateControllerAsT<T>(StateControllerType p_type) where T : GameEntityStateController
    {
        if (_stateController == null)
            return null;
        if (_stateController.StateControllerType == p_type)
            return (T)_stateController;
        else
            return null;
    }
    
    public T GetEntityDataAsT<T> () where T : GameEntityData
    {
        return (T) _gameEntityData;
    }

    public T GetEntityComponent<T>(GameEntityComponentType p_componentType) where T : GameEntityComponent
    {
        if (_gameEntityComponents != null && _gameEntityComponents.Count > 0)
        {
            foreach (var l_component in _gameEntityComponents)
            {
                if (l_component.ComponentType == p_componentType)
                {
                    return l_component as T;
                }
            }
            return null;
        }
        else
           return null;
    }

    public List<T> GetEntityComponents<T>(GameEntityComponentType p_componentType) where T : GameEntityComponent
    {
        List<T> l_result = new List<T>();

        if (_gameEntityComponents != null && _gameEntityComponents.Count > 0)
        {
            foreach (var l_component in _gameEntityComponents)
            {
                if (l_component.ComponentType == p_componentType)
                {
                    l_result.Add(l_component as T);
                }
            }

            if (l_result != null && l_result.Count > 0)
                return l_result;
            else
                return null;
        }
        else
            return null;
    }


    /***************************************** PRIVATE METHODS ************************************************/
    private IEnumerator SelfInitRoutine(float p_delay)
    {
        yield return new WaitForSeconds(p_delay);

        if (!IsActive())
            SetActive(true);

        Init();
        Setup();

        if (_stateController != null)
            _stateController.ToggleMainState(true);

        yield break;
    }

    private void _init()
    {
        _isInitialized = false;
         
        _rootTransform = GetComponent<Transform> ();
        
        if (_gameEntityComponents.Count > 0)
            _gameEntityComponents.Clear();        

        _gameEntityComponents = GetComponentsInChildren<GameEntityComponent>().CopyArrayToList();
    
        if (_gameEntityComponents == null && _gameEntityComponents.Count < 1)
        {
            Debug.LogError("Entity: " + _rootTransform.name + ":<GameEntity> ()._init(): Can't init game entity! Game entity components are missing!");
            return;
        }

        foreach (GameEntityComponent l_component in _gameEntityComponents)
        {
            l_component.Init();

            if (!l_component.IsInitialized)
            {
                Debug.LogError("Entity: " + _rootTransform.name + ":<GameEntity> ()._init(): Can't init game entity! One of entity components did not init properly!");
                return;
            }            
        }
              
        _stateController = GetComponent<GameEntityStateController> ();

        if (_stateController != null)
        {
            _stateController.Init();

            if (!_stateController.IsInitialized)
            {
                Debug.LogError("Entity: " + _rootTransform.name + ":<GameEntity> ()._init(): Can't init game entity! Game entity state controller did not init properly!");
                return;
            }
        }
        _isInitialized = true;
    }

    private void _setup()
    {
        _isSetup = false;

        if (!_isInitialized)
        {
            Debug.LogError("Entity: " + _rootTransform.name + ":<GameEntity> ()._setup(): Can't setup game entity! Game entity did not init properly!");
            return;
        }

        if (_gameEntityData == null)
        {
            Debug.LogError("Game entity: " + _rootTransform.name + " :<GameEntity> ().Setup(): Can't setup entity data! Data is null!!");
            return;
        }

        if (_gameEntityData.ComponentData == null || _gameEntityData.ComponentData.Count < 1)
        {
            Debug.Log("Game entity: " + _rootTransform.name + " :<GameEntity> ()._setup(): Can't setup component data! Data is null!!");
            l_dataOk = false;
        }

        int l_size = _gameEntityComponents.Count;

        for (int i = 0; i < l_size; i++)
        {
            var l_component = _gameEntityComponents[i];

            if (l_dataOk && i < _gameEntityData.ComponentData.Count && _gameEntityData.ComponentData[i] != null)
                l_component.Setup(_gameEntityData.ComponentData[i]);
            else
                l_component.Setup();

            if (!l_component.IsSetup)
            {
                Debug.Log("Entity: " + gameObject.name + ":<GameEntity> ()._setup(): Can't setup game entity! One of entity components did not setup properly! Failed component: " + l_component);
                return;
            }
        }
 
        if (_stateController != null)
        {
            _stateController.Setup(_gameEntityData);

            if (!_stateController.IsSetup)
            {
                Debug.LogError("Entity: " + _rootTransform.name + ":<GameEntity> ()._init(): Can't setup game entity! State controller did not setup properly!");
                return;         
            }
        } 

        _rootTransform.localScale = _gameEntityData.RootTransformScale;
        _isSetup = true;         
    }
}