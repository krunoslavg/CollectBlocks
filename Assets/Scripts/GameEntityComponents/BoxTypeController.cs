using UnityEngine;

public class BoxTypeController : GameEntityComponent
{
    [SerializeField] private BoxType _boxType;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public BoxType BoxType { get => _boxType; }

    private void OnEnable()
    {
        _componentType = GameEntityComponentType.BoxTypeController;
    }


    public override void Setup(GameEntityComponentData p_data)
    {
        base.Setup(p_data);


        BoxTypeControllerData l_data = null;

        if (!CheckComponentData(p_data, out l_data))
            return;

        _boxType = l_data.BoxType;

        _spriteRenderer.material.color = (_boxType == BoxType.Red) ? Color.red : Color.blue;
    }
}
