using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Базовый абстрактный класс, от которого наследуются все классы состояния
/// </summary>
public abstract class State
{
    /// <summary>
    /// Хранит ссылку основной объект класса противника со всеми параметрами и данными. Необходимо для разного рода взаимодействий
    /// </summary>
    protected SwordsmanEnemy _enemy;

    /// <summary>
    /// Конструктор класса состояния, необходим для прокидывания связей с данными противника и машины состояний
    /// </summary>
    /// <param name="enemy">Данный и параметры противника</param>
    /// <param name="stateMachineEnemy">Машина состоянйи противника</param>
    protected State(SwordsmanEnemy enemy)
    {
        _enemy = enemy;
        //_stateMachine = stateMachine;
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
    /// Данный метод необходимо вызвать в методе FixedUpdate в классе наслдеованного от MonoBehaviour. Пищем логику, ту которую хотели бы выполнить в методе FixedUpdate 
    /// </summary>
    public virtual void FixedUpdate()
    {

    }

    /// <summary>
    /// Метод вызываемый при выходе из состояния
    /// </summary>
    public virtual void Exit()
    {

    }

    public virtual bool IsAnimationPlaying(string animationName)
    {
        // Берем информацию о состоянии
        var animatorStateInfo = _enemy.Animator.GetCurrentAnimatorStateInfo(0);
        // Смотрим, есть ли в нем имя какой-то анимации, то возвращаем true
        if (animatorStateInfo.IsName(animationName))
            return true;

        return false;
    }
}
