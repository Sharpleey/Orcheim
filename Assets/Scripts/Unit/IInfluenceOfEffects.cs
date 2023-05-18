using System;
using System.Collections.Generic;

/// <summary>
/// ѕодвержен эффектам
/// </summary>
public interface IInfluenceOfEffects
{
    /// <summary>
    /// —ловарь активных эффектов установленных на юните 
    /// </summary>
    public Dictionary<Type, Effect> ActiveEffects { get; }

    /// <summary>
    /// ћетод устанавливает и примен€ет эффект на юнита
    /// </summary>
    /// <param name="effect">Ёффект, который хотим установить на юнита</param>
    void SetEffect(Effect effect);

    /// <summary>
    /// ћетод снимает определенный эффект с юнита
    /// </summary>
    /// <param name="effect">Ёффект, который хотим сн€ть</param>
    void RemoveEffect(Effect effect);
}
