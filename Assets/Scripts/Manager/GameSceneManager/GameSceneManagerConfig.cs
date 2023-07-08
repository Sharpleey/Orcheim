using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ManagerConfig/GameSceneManagerConfig", fileName = "GameSceneManagerConfig", order = 0)]
public class GameSceneManagerConfig : ScriptableObject
{
    [SerializeField] private Scene[] _scenes;

    public Scene[] Scenes => _scenes;
}
