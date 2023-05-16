using Scipts.Enums;
using UnityEngine;

namespace Scipts.Factory
{
    public interface IEnemyFactory
    {
        public void Load();
        public void Create(EnemyType enemyType, StartStateType startStateType, Vector3 spawnPosition);
    }
}