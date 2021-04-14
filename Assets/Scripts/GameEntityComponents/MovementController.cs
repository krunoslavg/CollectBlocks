using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : GameEntityComponent
{
    /***************************************** INSPECTOR VARIABLES ***********************************************/
    [Header("Data")]
    [SerializeField] private bool _isMoving = false;
    [SerializeField] private bool _shouldMove = true;
    [SerializeField] private float _moveSpeed = 2f; 
    
    [Header("Internal components")]
    [SerializeField] private Rigidbody2D _bodyToMove;


    /***************************************** INSPECTOR VARIABLES ***********************************************/
    private Vector2 _currentDirection = Vector2.zero;
    private bool _canMove = false;


    /***************************************** PROPERTIES *********************************************************/    


    /***************************************** UNITY METHODS ***************************************************/
    private void OnEnable()
    {
        _componentType = GameEntityComponentType.MovementController;
    }

    private void Update()
    {
        MovePathUpdate();   
    }


    /***************************************** PUBLIC METHODS ***************************************************/
    public override void Init()
    {
        base.Init ();

        if (_bodyToMove == null)
            _bodyToMove = GetComponentInChildren<Rigidbody2D> (); 
    }
     
    public override void Setup(GameEntityComponentData p_data)
    {
        base.Setup(p_data);

        _canMove = false;
        _isMoving = false;

        MovementControllerData l_data = null;

        if (!CheckComponentData(p_data, out l_data))
            return;        
 
        _shouldMove = l_data.ShouldMove;
        _moveSpeed = l_data.MoveSpeed;
    }
     

    /***************************************** PUBLIC METHODS ***************************************************/ 
    public void MoveInDirection(int p_direction)
    {
        if (_gameEntity != null && !_gameEntity.IsActive ())
        {
            Debug.Log ("Scene object: " + _gameEntity.name + ": <MovementController> (): Can't start moving! Scene object is toggled off!");
            return;
        }
        if ((DirectionX8) p_direction == DirectionX8.Left)
            _currentDirection = new Vector2(-1f, 0f);
        if ((DirectionX8) p_direction == DirectionX8.Right)
            _currentDirection = new Vector2(1f, 0f);

        _canMove = true;
    }
    
    public void FlipDirection() 
    {
        _currentDirection.x *= -1f;
    }

    public void MoveInRandomDirection() 
    {
        MoveInDirection((Random.Range(0, 100) > 50) ? 0 : 1);   
    }

    public void StopAllMovements()
    {
        _canMove = false;
        _isMoving = false;
    }


    /***************************************** PRIVATE METHODS ***************************************************/
    private void MovePathUpdate()
    {
        if (_shouldMove && _canMove && _moveSpeed > 0f && _currentDirection != Vector2.zero)
        {
            _isMoving = true;
            _bodyToMove.velocity = _currentDirection * _moveSpeed;
        }
        else
            _isMoving = false;
    }
}