using UnityEngine;

/// <summary>
/// Класс отвечает за оружие, которое будет использовать персонаж. Оно выбирается рандомно из массмва с оружием. 
/// То оружие, которое будет использоваться данным типом врага, заносим в массив в инспекторе.
/// </summary>
public class EnemyWeaponController : MonoBehaviour
{
    /// <summary>
    /// Родительский объект, к которому прикрепляется оружие
    /// </summary>
    [SerializeField] private Transform _weaponPlace;

    /// <summary>
    /// Используемое оружие. Можно задать в инспеторе или если не задано выбирается из массива с оружием
    /// </summary>
    [SerializeField] private GameObject _usedWeapon;

    /// <summary>
    /// Массив оружия, который может использовать данный тип врага
    /// </summary>
    [SerializeField] private GameObject[] _weapons;

    private CapsuleCollider _usedWeaponTriggerCollider;
    private Rigidbody _rigidbodyUsedWeapon;

    private Vector3 _weaponPositionOnWeaponPlace;
    private Quaternion _weaponRotationOnWeaponPlace;

    private void Start()
    {
        DisableWeapons();

        if (!_usedWeapon)
        {
            SetRandomWeapon();
        }
    }

    /// <summary>
    /// Метод устанавливает случайное оружие из списка.
    /// Все объекты с оружием должны уже быть приклеплены на место к объекту персонажа (Врага), но не активны.
    /// Метод активирует используемое оружие.
    /// </summary>
    private void SetRandomWeapon()
    {
        // Выбираем и устанавливаем случайное оружие
        int indexWeapon = Random.Range(0, _weapons.Length);
        _usedWeapon = _weapons[indexWeapon];

        // Делаем активным используемое оружие
        _usedWeapon.SetActive(true);

        // Получаем коллайдер оружия отвечаемый за нанесение урона
        _usedWeaponTriggerCollider = _usedWeapon.GetComponentInChildren<CapsuleCollider>();

        // Получаем Rigidbody используемого оружия для дальнейшего использования
        _rigidbodyUsedWeapon = _usedWeapon.GetComponent<Rigidbody>();

        // Делаем изначально коллайдер неактивным
        if (_usedWeaponTriggerCollider)
            _usedWeaponTriggerCollider.enabled = false;
    }

    /// <summary>
    /// Метод делает не активными все оружия
    /// </summary>
    private void DisableWeapons()
    {
        foreach (GameObject weapon in _weapons)
        {
            weapon.SetActive(false);
        }
    }

    /// <summary>
    /// Метод для события анимации атаки. Используется чтобы в определенные моменты атаки включать и отключать 
    /// возможность нанесения врагом урона
    /// </summary>
    private void EnableDealingDamage(ObjectState state)
    {
        _usedWeaponTriggerCollider.enabled = state == ObjectState.Enabled;
    }

    /// <summary>
    /// Метод отвязывает оружие от модели противника и делает его физичным или наоборот возращает оружие на место
    /// </summary>
    /// <param name="isMakePhysical">Сделать физичным</param>
    public void MakeWeaponPhysical(bool isMakePhysical)
    {
        if(isMakePhysical)
        {
            // Запоминаем позицию и поворот оружия до его отсоединения от врага
            _weaponPositionOnWeaponPlace = _usedWeapon.transform.position;
            _weaponRotationOnWeaponPlace = _usedWeapon.transform.rotation;

            // Отсоединяем оружие от врага  
            _usedWeapon.transform.parent = null;

            // Делаем его не статичным
            _rigidbodyUsedWeapon.isKinematic = false;

            return;
        }

        // Возращаем оружие на место
        _usedWeapon.transform.parent = _weaponPlace;
        _usedWeapon.transform.position = _weaponPositionOnWeaponPlace;
        _usedWeapon.transform.rotation = _weaponRotationOnWeaponPlace;
        _rigidbodyUsedWeapon.isKinematic = true;
    }
}
