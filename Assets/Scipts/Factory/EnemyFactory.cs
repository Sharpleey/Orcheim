using Scipts.Enums;
using UnityEngine;
using Zenject;

namespace Scipts.Factory
{
    public class EnemyFactory: IEnemyFactory
    {
        private const string _warriorPath = "Warrior";
        private const string _goonPath = "Goon";
        private const string _commanderPath = "Commander";
        
        private readonly DiContainer _diContainer;
        
        private Object _warriorPrefab;
        private Object _goonPrefab;
        private Object _commanderPrefab;

        public EnemyFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public void Load()
        {
            _warriorPrefab = Resources.Load(_warriorPath);
            _goonPrefab = Resources.Load(_goonPath);
            _commanderPrefab = Resources.Load(_commanderPath);
        }

        public void Create(EnemyType enemyType, StartStateType startStateType, Vector3 spawnPosition)
        {
            switch (enemyType)
            {
                case EnemyType.Warrior:
                    InstantiateEnemy(_warriorPrefab, startStateType, spawnPosition);
                    break;
                case EnemyType.Goon:
                    InstantiateEnemy(_goonPrefab, startStateType, spawnPosition);
                    break;
                case EnemyType.Commander:
                    InstantiateEnemy(_commanderPrefab, startStateType, spawnPosition);
                    break;
            }
        }

        private void InstantiateEnemy(Object prefab, StartStateType startStateType, Vector3 spawnPosition)
        {
            GameObject instantiatePrefab = _diContainer.InstantiatePrefab(prefab, spawnPosition, Quaternion.identity, null);
            instantiatePrefab.GetComponent<EnemyUnit>().DefaultState = startStateType;
        }
    }
}