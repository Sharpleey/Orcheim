using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyUIController : MonoBehaviour
{
    #region Serialize fields
    [Header("Enemy Canvas")]
    [SerializeField] private Transform _enemyCanvas;

    [Header("Taken Damage Text Settings")]
    [SerializeField] private GameObject _prefabTakenDamage;
    [SerializeField] private float _rateShowingTakenDamage = 2.5f;

    #endregion Serialize fields

    #region Public fields
    public Dictionary<TypeDamage, Color> TYPE_DAMAGE_COLOR = new Dictionary<TypeDamage, Color>
    {
        { TypeDamage.Physical, new Color(0.9f, 0.9f, 0.9f) },
        { TypeDamage.Fire, new Color(0.81f, 0.32f, 0.07f) },
        { TypeDamage.Electric, new Color(0.04f, 0.92f, 0.98f) }
    };
    #endregion Public fields

    #region Public methods

    public void ShowPopupDamage(int damage, TypeDamage typeDamage)
    {
        StartCoroutine(ShowDamage(damage, typeDamage));
    }

    private IEnumerator ShowDamage(int damage, TypeDamage typeDamage)
    {
        GameObject takenDamage = Instantiate(_prefabTakenDamage);
        takenDamage.transform.SetParent(_enemyCanvas, false);

        TakenDamageTextController takenDamageTextController = takenDamage.GetComponent<TakenDamageTextController>();

        float newRate = Random.Range(_rateShowingTakenDamage - 0.2f, _rateShowingTakenDamage + 0.2f);

        takenDamageTextController.colorText = TYPE_DAMAGE_COLOR[typeDamage];
        takenDamageTextController.rateShowing = newRate;
        takenDamageTextController.SetText(damage.ToString());
        takenDamageTextController.ShowAndHide();

        // Ключевое слово yield указывает сопрограмме, когда следует остановиться.
        yield return new WaitForSeconds(newRate);

        // Удаляем объект со сцены и очищаем память
        Destroy(takenDamage);
    }
    #endregion Public methods
}
