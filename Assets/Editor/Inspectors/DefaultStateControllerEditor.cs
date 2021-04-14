using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(GameEntityStateController))]
public class GameEntityStateControllerEditor : Editor
{
    public StateType _stateType;
    public Transform _transform;
    private GameEntityStateController _entityStateController; 

    private List<StateType> _options;
    private string[] _optionsArray;
    private int _index = 0;

    void OnEnable()
    {
        _entityStateController = (GameEntityStateController) target;

        if (_entityStateController == null)
            return;

        _transform = _entityStateController.GameEntity.RootTransform; 

        _options = new List<StateType>();

        if (_entityStateController.PossibleStatesStruct != null && _entityStateController.PossibleStatesStruct.Count > 0)
        {
            foreach (GameEntityStateStruct l_state in _entityStateController.PossibleStatesStruct) 
                _options.Add(l_state.State);

            _optionsArray = new string[_options.Count];

            for (int i = 0; i < _options.Count; i++)
                _optionsArray[i] = _options[i].ToString();
        }
    }

    public override void OnInspectorGUI()
    { 
        serializedObject.Update();

        if (_options != null && _optionsArray != null && _options.Count > 0 && _optionsArray.Length > 0)
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();

            _index = EditorGUILayout.Popup("States", _index, _optionsArray, EditorStyles.popup);

            if (GUILayout.Button("Switch state"))
            {
                if (Application.isPlaying && _entityStateController.gameObject.IsActive())
                {
                    _entityStateController.SwitchState(_options[_index]);
                }
            }


            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();


            for (int i = 0; i < 5; i++)
                EditorGUILayout.Space();
        }
        
        DrawDefaultInspector();

        serializedObject.ApplyModifiedProperties();
    }
}


 


