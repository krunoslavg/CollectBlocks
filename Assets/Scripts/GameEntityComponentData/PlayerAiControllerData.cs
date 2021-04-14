using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAiControllerData", menuName = "Game entity component data/Player AI controller data")]
public class PlayerAiControllerData : GameEntityComponentData
{
    [SerializeField] private EntityReference _boxStorageRedEntity;
    [SerializeField] private EntityReference _boxStorageBlueEntity;

    public EntityReference BoxStorageRedEntity { get => _boxStorageRedEntity; }
    public EntityReference BoxStorageBlueEntity { get => _boxStorageBlueEntity; }
}