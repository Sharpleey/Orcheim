using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goon : Enemy
{
    [Header("Spell Parameters")]
    [SerializeField, Min(5)] private float _cooldownWarcry = 20f;
    [SerializeField, Min(2)] private float _radiusWarcry = 8f;

    public bool IsWarcryInCooldown { get; private set; }

    private new void Start()
    {
        base.Start();

        // �������������� ���������
        InitStates();
        // ������ ��������� �����������
        SetStateByDefault();
    }

    /// <summary>
    /// ����� �������������� ���������
    /// </summary>
    private new void InitStates()
    {
        base.InitStates();

        _states[typeof(ChasingState)] = new GoonChasingState(this);
        _states[typeof(GoonAttackState)] = new GoonAttackState(this);
        _states[typeof(InspirationState)] = new InspirationState(this);
    }

    public void CastWarcry()
    {
        if (IsWarcryInCooldown)
            return;

        Debug.Log("������������� ����������� Warcry");

        StartCoroutine(ResetCooldown());

        // ���� ������� ������� � �������
        // ������ �� ��� ������������� ������ ��������� �����
    }

    private IEnumerator ResetCooldown()
    {
        IsWarcryInCooldown = true;

        yield return new WaitForSeconds(_cooldownWarcry);

        IsWarcryInCooldown = false;
    }
}
