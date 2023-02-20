using UnityEngine;

public class EnemyUnitFactory : BaseFactory<EnemyUnit>
{
    public override EnemyUnit GetNewInstance(EnemyUnit obj, Vector3 position)
    {
        var enemyUnit = base.GetNewInstance(obj, position);

        enemyUnit.DefaultState = StartStateType.Chasing;

        return enemyUnit;
    }
}
