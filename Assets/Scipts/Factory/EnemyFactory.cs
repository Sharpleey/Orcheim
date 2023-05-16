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
            
            Load();
        }
        
        public void Load()
        {
            _warriorPrefab = Resources.Load(_warriorPath);
            _goonPrefab = Resources.Load(_goonPath);
            _commanderPrefab = Resources.Load(_commanderPath);
        }

        public EnemyUnit Create(EnemyType enemyType, StartStateType startStateType, Transform spawnTransform)
        {
            EnemyUnit enemyUnit = null;
            
            switch (enemyType)
            {
                case EnemyType.Warrior:
                    enemyUnit = InstantiateEnemy(_warriorPrefab, startStateType, spawnTransform);
                    break;
                case EnemyType.Goon:
                    enemyUnit = InstantiateEnemy(_goonPrefab, startStateType, spawnTransform);
                    break;
                case EnemyType.Commander:
                    enemyUnit = InstantiateEnemy(_commanderPrefab, startStateType, spawnTransform);
                    break;
            }

            return enemyUnit;
        }

        public EnemyUnit GetNewInstance(EnemyUnit prefab, StartStateType startStateType, Transform spawnTransform)
        {
            EnemyUnit enemyUnit = _diContainer.InstantiatePrefabForComponent<EnemyUnit>(prefab, spawnTransform.position, spawnTransform.rotation, null);
            enemyUnit.DefaultState = startStateType;
            return enemyUnit;
        }

        private EnemyUnit InstantiateEnemy(Object prefab, StartStateType startStateType, Transform spawnTransform)
        {
            GameObject instantiatePrefab = _diContainer.InstantiatePrefab(prefab, spawnTransform.position, spawnTransform.rotation, null);
            EnemyUnit enemyUnit = instantiatePrefab.GetComponent<EnemyUnit>();
            enemyUnit.DefaultState = startStateType;

            return enemyUnit;
        }
    }
}