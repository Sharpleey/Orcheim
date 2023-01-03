using System.Collections;
using UnityEngine;

public class Goon : EnemyUnit
{
    [Header("Spell Parameters")]
    [SerializeField, Min(5)] private float _cooldownWarcry = 20f;
    [SerializeField, Min(2)] private float _radiusWarcry = 8f;

    public bool IsWarcryInCooldown { get; private set; }

    /// <summary>
    /// ����� �������������� ���������
    /// </summary>
    public override void InitStates()
    {
        base.InitStates();

        States[typeof(ChasingState)] = new GoonChasingState(this);
        States[typeof(GoonAttackState)] = new GoonAttackState(this);
        States[typeof(InspirationState)] = new InspirationState(this);
    }

    /// <summary>
    /// ����� ��� ����� ��������� �� EventAnimation
    /// </summary>
    private void SetChasingState()
    {
        SetState<ChasingState>();
    }

    private IEnumerator ResetCooldown()
    {
        IsWarcryInCooldown = true;

        yield return new WaitForSeconds(_cooldownWarcry);

        IsWarcryInCooldown = false;
    }

    private void CastWarcry()
    {
        if (IsWarcryInCooldown)
            return;

        Debug.Log("������������� ����������� Warcry");

        StartCoroutine(ResetCooldown());

        // ���� ������� ������� � �������
        // ������ �� ��� ������������� ������ ��������� �����
    }
}
