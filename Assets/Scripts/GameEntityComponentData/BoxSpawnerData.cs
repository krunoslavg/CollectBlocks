using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoxSpawnerData01", menuName = "Game entity component data/Box spawner data")]
public class BoxSpawnerData : GameEntityComponentData
{  
    /***************************************** INSPECTOR VARIABLES ***********************************************/
    [SerializeField] private bool _shouldSpawn = true;    
    [Range(1, 11)]
    [SerializeField] private float _spawnFrequency;
    [SerializeField] private float _spawnOffsetRandom;
    [SerializeField] private GameObject _boxPrefab;
    [SerializeField] private List<GameEntityData> _boxData;

    /***************************************** PROPERTIES ***********************************************/
    public bool ShouldSpawn { get => _shouldSpawn; }
    public float SpawnFrequency { get => _spawnFrequency; }
    public float SpawnOffsetRandom { get => _spawnOffsetRandom; }
    public GameObject BoxPrefab { get => _boxPrefab; }
    public List<GameEntityData> BoxData { get => _boxData; }
}


