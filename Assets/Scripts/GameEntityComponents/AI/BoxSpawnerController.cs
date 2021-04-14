using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawnerController : GameEntityComponent
{
    /***************************************** INSPECTOR VARIABLES ***********************************************/
    [SerializeField] private bool _paused = false;
    [SerializeField] private bool _shouldSpawn = true;
    [SerializeField] private float _spawnFrequency;
    [SerializeField] private float _spawnOffsetRandom;
    [SerializeField] private GameObject _boxPrefab;
    [SerializeField] private List<GameEntityData> _boxData;

    /***************************************** INSPECTOR VARIABLES ***********************************************/
    private bool _started = false;

    /***************************************** UNITY METHODS ***************************************************/
    private void OnEnable()
    {
        _componentType = GameEntityComponentType.BoxSpawnerController;
    }
     

    /***************************************** PUBLIC METHODS ***************************************************/
    public override void Setup(GameEntityComponentData p_data)
    {
        base.Setup(p_data);

        BoxSpawnerData l_data = null;

        if (!CheckComponentData(p_data, out l_data))
            return;

        _shouldSpawn = l_data.ShouldSpawn;
        _spawnFrequency = l_data.SpawnFrequency;
        _spawnOffsetRandom = l_data.SpawnFrequency;
        _boxPrefab = l_data.BoxPrefab;
        _boxData = l_data.BoxData;
    }

    public void StartOrContinueSpawning()
    {
        if (!_started)
            StartCoroutine(SpawnRoutine());
        else if (_paused)
            _paused = false;
    }

    public void StopSpawning()
    {
        StopAllCoroutines();

        _started = false;
    }

    public void Pause()
    {
        _paused = true;
    }

    /***************************************** PRIVATE METHODS ***************************************************/
    private IEnumerator SpawnRoutine() 
    {
        if (!_isInitialized || !_isSetup)
            yield break;

        _started = true;

        while (true)
        {
            yield return new WaitWhile(() => !_shouldSpawn || _paused);
            GameEntity l_box = (Instantiate(_boxPrefab, transform.position + new Vector3(Random.Range(-_spawnOffsetRandom, _spawnOffsetRandom), 0f, 0f), Quaternion.identity, null)).GameEntity();
            l_box.Init();
            l_box.Setup(_boxData[Random.Range(0, _boxData.Count)]);
            yield return new WaitForSeconds(_spawnFrequency);
        }
    }
}
