using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyUnitFactory : BaseFactory<EnemyUnit>
{
    private Dictionary<EnemyType, EnemyUnit> _enemyPrefabs;

    public EnemyUnitFactory(DiContainer diContainer) : base(diContainer)
    {
        LoadResources();
    }
    
    public EnemyUnit GetNewInstance(EnemyUnit prefab, StartStateType startStateType, Vector3 position, Quaternion rotation, bool isActive = true, Transform parent = null)
    {
        EnemyUnit enemyUnit = base.GetNewInstance(prefab, position, rotation,  isActive, parent);
        enemyUnit.DefaultState = startStateType;

        return enemyUnit;
    }

    public EnemyUnit GetNewInstance(EnemyType enemyType, StartStateType startStateType, Vector3 position, Quaternion rotation, bool isActive = true, Transform parent = null)
    {
        EnemyUnit enemyUnit = GetNewInstance(_enemyPrefabs[enemyType], startStateType, position, rotation, isActive, parent);
        return enemyUnit;
    }

    private void LoadResources()
    {
        _enemyPrefabs = new Dictionary<EnemyType, EnemyUnit>();
        
        _enemyPrefabs[EnemyType.Warrior] = Resources.Load<EnemyUnit>(HashResourcesPath.WARRIOR_PATH);
        _enemyPrefabs[EnemyType.Goon] = Resources.Load<EnemyUnit>(HashResourcesPath.GOON_PATH);
        _enemyPrefabs[EnemyType.Commander] = Resources.Load<EnemyUnit>(HashResourcesPath.COMMANDER_PATH);
    }
}
