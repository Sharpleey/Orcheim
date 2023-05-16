using Scipts.Enums;
using UnityEngine;

namespace Scipts.Factory
{
    public interface IEnemyFactory
    {
        public void Load();
        public EnemyUnit Create(EnemyType enemyType, StartStateType startStateType, Transform spawnTransform);
        public EnemyUnit GetNewInstance(EnemyUnit prefab, StartStateType startStateType, Transform spawnTransform);
    }
}