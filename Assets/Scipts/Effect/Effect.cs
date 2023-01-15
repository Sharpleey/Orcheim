using System.Collections;
using UnityEngine;

/// <summary>
/// ����������� ����� �������
/// </summary>
public abstract class Effect : INaming, ICloneable
{
    /// <summary>
    /// �������� �������
    /// </summary>
    public abstract string Name { get; }
    
    /// <summary>
    /// �������� �������
    /// </summary>
    public abstract string Description { get; }
    
    /// <summary>
    /// ��� �������, ������������� ��� ����������
    /// </summary>
    public EffectType EffectType { get; protected set; }

    /// <summary>
    /// ������������ �������, ���� ����� ������� ������ ��� ������������, �� �� ��������������
    /// </summary>
    public Duration Duration { get; protected set; } = null;

    /// <summary>
    /// ������� �������� �������, �� ��������� ������ ��������� ������ 1 ���.
    /// </summary>
    public float Frequency { get; protected set; } = 1f;

    /// <summary>
    /// �������� ���������� �� ������������� �������, ����������� � ������� �� ������� ���������� ������
    /// </summary>
    public IEnumerator CoroutineEffect { get; private set; }

    /// <summary>
    /// ������ �����, ��� ������� ���������� ��������� � ������� �������
    /// </summary>
    protected Unit unit;

    protected EnemyUnit enemyUnit;
    protected PlayerUnit playerUnit;

    /// <summary>
    /// ����� ��� ��������� ����������� ������� �������
    /// </summary>
    /// <param name="unit">��� ������ ������� ����� ����� ��������������� ������ �������</param>
    /// <returns>����� �������</returns>
    public Effect DeepCopy(Unit unit)
    {
        Effect other = (Effect)MemberwiseClone();
        other.unit = unit;
        other.CoroutineEffect = other.UpdateEffect();
        return other;
    }

    /// <summary>
    /// ����� ���������� �������, ���������� ��� �������� ��� ������ ��� ��������� �������
    /// </summary>
    public virtual void Enable()
    {
        enemyUnit = unit as EnemyUnit;
        playerUnit = unit as PlayerUnit;
    }

    /// <summary>
    /// �����, � ������� ������ ���������� ��� �������� ��� ���������� ����� � ��������� ��������������
    /// </summary>
    public virtual void Tick()
    {

    }

    /// <summary>
    /// ����� ���������� ��� �������� ������� � ���������.
    /// </summary>
    public virtual void Disable()
    {

    }

    /// <summary>
    /// ����� ��� �������� Update
    /// </summary>
    /// <returns></returns>
    protected IEnumerator UpdateEffect()
    {
        float duration = 0;

        while (true)
        {
            Tick();

            yield return new WaitForSeconds(Frequency);

            duration += Frequency;

            if (duration >= Duration.Actual)
            {
                break;
            }
        }
        
        unit.RemoveEffect(this);
    }
}
