using UnityEngine;
using UnityEngine.Events;

public class PlayerAiController : GameEntityComponent
{
    /***************************************** INSPECTOR VARIABLES ***********************************************/
    [Header("Flags")] 
    [SerializeField] private EntityReference _boxStorageRed;
    [SerializeField] private EntityReference _boxStorageBlue;
    [Header("Events")]
    [Header("Movement")]
    [SerializeField] private UnityEvent _onMovementRandomDirectionRequested;
    [SerializeField] private UnityEvent _onMovementLeftDirectionRequested;
    [SerializeField] private UnityEvent _onMovementRightDirectionRequested;
    [SerializeField] private UnityEvent _onMovementDirectionFlipRequested;
    [Header("Boxes")]
    [SerializeField] private UnityEvent _onBoxFound;
    [SerializeField] private UnityEvent _onBoxStorageFound;
    [SerializeField] private UnityEvent _onBoxStored;


    /***************************************** PRIVATE VARIABLES ***********************************************/
    private GameEntity _currentBox = null;
     

    /***************************************** UNITY METHODS ***************************************************/
    private void OnEnable()
    {
        _componentType = GameEntityComponentType.PlayerAiController;
    }
 

    /***************************************** PUBLIC METHODS ***************************************************/ 
    public override void Setup(GameEntityComponentData p_data)
    {
        base.Setup(p_data);
         
        PlayerAiControllerData l_data = null;

        if (!CheckComponentData(p_data, out l_data))
            return;

        _boxStorageBlue = l_data.BoxStorageBlueEntity;
        _boxStorageRed = l_data.BoxStorageRedEntity;
    }

    public void StartSearchForBox() 
    { 
        if (_gameEntity.RootPosition.x == 0.0f)
            _onMovementRandomDirectionRequested.Invoke();
        else if (_gameEntity.RootPosition.x < 0.0f)
            _onMovementRightDirectionRequested.Invoke();
        else if (_gameEntity.RootPosition.x > 0.0f)
            _onMovementLeftDirectionRequested.Invoke();
    }

    public void TakeBox()
    {
        _currentBox.GetComponent<BoxCollider2D>().isTrigger = true;
        Destroy(_currentBox.GetComponent<Rigidbody2D>());
        _currentBox.transform.parent = transform;
        _currentBox.RootPosition = _gameEntity.RootPosition + new Vector2(0f, 1f);        
    }

    public void StartSearchingForBoxStorage()
    {
        BoxType l_type = _currentBox.GetEntityComponent<BoxTypeController>(GameEntityComponentType.BoxTypeController).BoxType;
                
        if (l_type == BoxType.Red)
        {
            if (_boxStorageRed.GetValue().RootPosition.x < _gameEntity.RootPosition.x)
                _onMovementLeftDirectionRequested.Invoke();
            else
                _onMovementRightDirectionRequested.Invoke();
        }
        else
        {
            if (_boxStorageBlue.GetValue().RootPosition.x < 0.0f)
                _onMovementLeftDirectionRequested.Invoke();
            else
                _onMovementRightDirectionRequested.Invoke();
        }        
    }

    public void StoreBox()
    {
        Destroy(_currentBox.gameObject);
        _currentBox = null; 
        _onBoxStored.Invoke();
    }


    /***************************************** PUBLIC METHODS ***************************************************/
    public void OnPlayerTriggerEnter(CollisionEventType p_eventType, GameEntity p_collidedEntity)
    {
        if (_gameEntity.StateController.IsEntityInState(StateType.SearchForBox))
        {
            if (p_collidedEntity.StateController.StateControllerType == StateControllerType.BoxStorage)
                _onMovementDirectionFlipRequested.Invoke();
            if (p_collidedEntity.StateController.StateControllerType == StateControllerType.Box)
            {
                _currentBox = p_collidedEntity; 
                _onBoxFound.Invoke();
            }
        }
        if (_gameEntity.StateController.IsEntityInState(StateType.SearchForBoxStorage))
        {
            if (p_collidedEntity.StateController.StateControllerType == StateControllerType.BoxStorage)
                _onBoxStorageFound.Invoke();
        }
    }
}