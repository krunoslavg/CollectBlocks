using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoxTypeControllerData01", menuName = "Game entity component data/Box type controller data")]
public class BoxTypeControllerData : GameEntityComponentData
{
    [SerializeField] private BoxType _boxType;

    public BoxType BoxType { get => _boxType; }
}
