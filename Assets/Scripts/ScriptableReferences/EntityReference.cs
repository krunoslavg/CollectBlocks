using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[CreateAssetMenu(fileName = "EntityRefVar01(SpriteList)", menuName = "Scriptable references/Entity reference variable")]
public class EntityReference : ScriptableReference
{
    [SerializeField] GameEntity _entity; 

    public void SetValue(GameEntity p_value)
    {
        _entity = p_value;
    }

    public GameEntity GetValue()
    { 
        return _entity;
    }
     
}