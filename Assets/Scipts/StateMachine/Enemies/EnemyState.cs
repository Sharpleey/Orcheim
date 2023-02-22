using UnityEngine;
/// <summary>
/// Базовый абстрактный класс, от которого наследуются все классы состояния
/// </summary>
public abstract class EnemyState: IState
{
    /// <summary>
    /// Хранит ссылку основной объект класса противника со всеми параметрами и данными. Необходимо для разного рода взаимодействий
    /// </summary>
    protected EnemyUnit enemyUnit;

    /// <summary>
    /// Transform игрока для отслеживания его позиции
    /// </summary>
    protected Transform transformPlayer;

    /// <summary>
    /// Дистанция от врага до игрока
    /// </summary>
    protected float distanceEnemyToPlayer;

    /// <summary>
    /// Маска слоев с одним слоем Enemy, который мы будем искать
    /// </summary>
    private LayerMask collisionMask = 4096;

    /// <summary>
    /// Конструктор класса состояния, необходим для прокидывания связей с данными противника и машины состояний
    /// </summary>
    /// <param name="enemyUnit">Данный и параметры противника</param>
    protected EnemyState(EnemyUnit enemyUnit)
    {
        this.enemyUnit = enemyUnit;
    }
    /// <summary>
    /// Метод вызываемый при входе в состояние
    /// </summary>
    public virtual void Enter()
    {

    }

    /// <summary>
    /// Данный метод необходимо вызвать в методе Update в классе наслдеованного от MonoBehaviour. Пищем логику, ту которую хотели бы выполнить в методе Update 
    /// </summary>
    public virtual void Update()
    {

    }

    /// <summary>
    /// Метод вызываемый при выходе из состояния
    /// </summary>
    public virtual void Exit()
    {

    }

    /// <summary>
    /// Метод ищет игрока по тегу на сцене, получает его трансформ и возвращает его
    /// </summary>
    /// <returns>Transfrom игрока</returns>
    protected Transform GetTransformPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    /// <summary>
    /// Метод ищет ближайших юнитов противника (EnemyUnit) и возвращает их кол-во
    /// </summary>
    /// <returns>Кол-во EnemyUnit в заданном радиусе</returns>
    protected int GetCountAlliesNearby(float searchRadius)
    {
        Vector3 center = enemyUnit.transform.position;

        Collider[] hitColliders = Physics.OverlapSphere(center, searchRadius, collisionMask);

        return hitColliders.Length;
    }

    /// <summary>
    /// Метод для обработки события. Для смены состояния на преследование
    /// </summary>
    /// <param name="wave"></param>
    protected void SetChasingState(int wave)
    {
        enemyUnit.SetState<ChasingState>();
    }
}
