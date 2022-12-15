using System.Collections;
using UnityEngine;

public abstract class Effect : IItem, ICloneable
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
    public abstract EffectType EffectType { get; }
    
    /// <summary>
    /// ������������ �������, ���� ����� ������� ������ ��� ������������, �� �� ��������������
    /// </summary>
    public virtual float Duration { get; set; }
    
    /// <summary>
    /// ������� �������� �������, �� ��������� ������ ��������� ������ 1 ���.
    /// </summary>
    public virtual float Frequency { get; set; } = 1f;

    /// <summary>
    /// �������� ���������� �� ������������� �������, ����������� � ������� �� ������� ���������� ������
    /// </summary>
    public IEnumerator CoroutineEffect { get; private set; }

    /// <summary>
    /// ������ �����, ��� ������� ���������� ��������� � ������� �������
    /// </summary>
    protected Enemy enemy; //TODO ����� �������� �� ������ �� Enemy

    public Effect()
    {
        
    }

    /// <summary>
    /// ����� ��� ��������� ����������� ������� �������
    /// </summary>
    /// <param name="enemy">��� ������ ������� ����� ����� ��������������� ������ �������</param>
    /// <returns>����� �������</returns>
    public Effect DeepCopy(Enemy enemy)
    {
        Effect other = (Effect)MemberwiseClone();
        other.enemy = enemy;
        other.CoroutineEffect = other.UpdateEffect();
        return other;
    }

    /// <summary>
    /// ����� ���������� �������, ���������� ��� �������� ��� ������ ��� ��������� �������
    /// </summary>
    public virtual void Enable()
    {

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

            if (duration >= Duration)
            {
                break;
            }
        }
        
        enemy.RemoveEffect(this);
    }
}
