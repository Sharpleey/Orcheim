using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс обеспечивает абстракцию для машины состояний
/// </summary>
public class StateMachine
{ 
    ///// <summary>
    ///// Ссылка на текущее состояние противника
    ///// </summary>
    

    ///// <summary>
    ///// Метод конфигурирует машину состояний, присваивая CurrentState значение startingState и вызывая для него Enter. 
    ///// Это инициализирует машину состояний, в первый раз задавая активное состояние.
    ///// </summary>
    ///// <param name="startingState">Стартовое состояние</param>
    //public void InitializeStartingState(State startingState)
    //{
    //    CurrentState = startingState;
    //    startingState.Enter();
    //}

    ///// <summary>
    ///// Метод обрабатывает переходы между состояниями. Он вызывает Exit для старого CurrentState перед заменой его ссылки на newState. В конце он вызывает Enter для newState.
    ///// </summary>
    ///// <param name="newState">Новое состояние, которое хотим установить</param>
    //public void ChangeState(State newState)
    //{
    //    CurrentState.Exit();

    //    CurrentState = newState;
    //    newState.Enter();
    //}
}
