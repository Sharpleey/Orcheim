using UnityEngine;

public class EnemyUnitFactory : BaseFactory<EnemyUnit>
{
    public override EnemyUnit GetNewInstance(EnemyUnit prefab, Vector3 position)
    {
        var enemyUnit = base.GetNewInstance(prefab, position);

        enemyUnit.DefaultState = StartStateType.Chasing;

        return enemyUnit;
    }
}
