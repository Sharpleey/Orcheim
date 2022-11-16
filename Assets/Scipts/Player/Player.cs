using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _meleeWeapon;
    [SerializeField] private GameObject _rangeWeapon;

    //private Equipment _equipment;
    //private Spell _spell;

    /// <summary>
    /// ������� ��������� ������
    /// </summary>
    public State CurrentState { get; private protected set; }

    /// <summary>
    /// ������� ��� �������� ���������
    /// </summary>
    private Dictionary<Type, State> _states;

    private GameObject _usedWeapon;

    private void Start()
    {
        _meleeWeapon?.SetActive(false);
        _rangeWeapon?.SetActive(false);

        if (_rangeWeapon)
            ChangeWeapon(_rangeWeapon);
        else
            ChangeWeapon(_meleeWeapon);
    }

    private void Update()
    {
        if (CurrentState != null)
            CurrentState.Update();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon(_meleeWeapon);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeapon(_rangeWeapon);
        }

    }

    private void ChangeWeapon(GameObject weapon)
    {
        if (weapon == _usedWeapon)
            return;

        _usedWeapon?.SetActive(false);
        _usedWeapon = weapon;
        _usedWeapon.SetActive(true);
    }

    /// <summary>
    /// ����� �������������� ���������
    /// </summary>
    private void InitStates()
    {
        _states = new Dictionary<Type, State>();

    }

    /// <summary>
    /// ����� ������������ �������� ����� �����������. �� �������� Exit ��� ������� CurrentState ����� ������� ��� ������ �� newState. � ����� �� �������� Enter ��� newState.
    /// </summary>
    /// <typeparam name="T">��� ������ ���������</typeparam>
    private void SetState<T>() where T : State
    {
        State newState;

        try
        {
            newState = _states[typeof(T)];
        }
        catch
        {
            Debug.Log("��������� " + typeof(T).ToString() + " ����������!");
            return;
        }

        if (CurrentState != null)
            CurrentState.Exit();

        CurrentState = newState;
        CurrentState.Enter();
    }

    public void TakeDamage(int damage)
    {
        Messenger<int>.Broadcast(GlobalGameEvent.PLAYER_DAMAGED, damage);
    }
}
