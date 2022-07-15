using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyUIController : MonoBehaviour
{
    [Header("Taken Damage Text Settings")]
    [SerializeField] private GameObject _prefabTakenDamage;
    [SerializeField] private Transform _enemyCanvas;
    [SerializeField] private float _rateShowingTakenDamage = 2.5f;

    public Dictionary<TypeDamage, Color> TYPE_DAMAGE_COLOR = new Dictionary<TypeDamage, Color>
    {
        { TypeDamage.Physical, new Color(0.9f, 0.9f, 0.9f) },
        { TypeDamage.Fire, new Color(0.81f, 0.32f, 0.07f) },
        { TypeDamage.Electric, new Color(0.04f, 0.92f, 0.98f) }
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShowDamage(int damage, TypeDamage typeDamage)
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
}
