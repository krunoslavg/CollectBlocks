[System.Serializable]
public enum ActiveState { On, Off }

[System.Serializable]
public enum DirectionX8 { Left, Right }

[System.Serializable]
public enum StateControllerType { Default, Player, Box, BoxStorage }

[System.Serializable]
public enum StateType
{
    Disabled,
    Ready,
    Paused, 
    Spawning,
    SearchForBox,
    TakeBox,
    SearchForBoxStorage,
    StoreBox
}

[System.Serializable]
public enum BoxType { Red, Blue }

[System.Serializable]
public enum GameEntityComponentType
{
    None,
    CollisionController, 
    MovementController,
    BoxSpawnerController,
    PlayerAiController,
    BoxTypeController
}