using UnityEngine;

/// <summary>
///  ласс отвечает за оружие, которое будет использовать персонаж. ќно выбираетс€ рандомно из массмва с оружием. 
/// “о оружие, которое будет использоватьс€ данным типом врага, заносим в массив в инспекторе.
/// </summary>
public class WeaponController : MonoBehaviour
{
    /// <summary>
    /// »спользуемое оружие. ћожно задать в инспеторе или если не задано выбираетс€ из массива с оружием
    /// </summary>
    [SerializeField] private GameObject _usedWeapon;
    /// <summary>
    /// ћассив оружи€, который может использовать данный тип врага
    /// </summary>
    [SerializeField] private GameObject[] _weapons;

    public GameObject UsedWeapon => _usedWeapon;

    private void Awake()
    {
        DisableWeapons();

        if (!_usedWeapon)
        {
            SetRandomWeapon();
        }
    }

    /// <summary>
    /// ћетод устанавливает случайное оружие из списка.
    /// ¬се объекты с оружием должны уже быть приклеплены на место к объекту персонажа (¬рага), но не активны.
    /// ћетод активирует используемое оружие.
    /// </summary>
    private void SetRandomWeapon()
    {
        int indexWeapon = Random.Range(0, _weapons.Length);

        _usedWeapon = _weapons[indexWeapon];

        _usedWeapon.SetActive(true);
    }

    private void DisableWeapons()
    {
        foreach (GameObject weapon in _weapons)
        {
            weapon.SetActive(false);
        }
    }
}
