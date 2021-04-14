using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEntityData01", menuName = "Default game entity data")]
public class GameEntityData : ScriptableObject
{
    [SerializeField] private Vector2 _startingPosition = Vector2.zero;
    [SerializeField] private float _baseMovementSpeed = 2f;
    [SerializeField] private float _baseRotationZ = 0f;
    [SerializeField] private Vector3 _baseRootTransformScale = new Vector3(1f, 1f, 1f);
    [Space(10)]
    [SerializeField] private List<GameEntityComponentData> _componentData;
    [SerializeField] protected List<ScriptableReference> _scriptableReferences;
     

    public Vector2 StartingPosition { get => _startingPosition; }
    public Vector3 RootTransformScale { get => _baseRootTransformScale; }
    public float RootRotation { get => _baseRotationZ; }
    public float MovementSpeed { get => _baseMovementSpeed; } 
    public List<GameEntityComponentData> ComponentData { get => _componentData; }
    public List<ScriptableReference> ScriptableReferences { get => _scriptableReferences; } 
}