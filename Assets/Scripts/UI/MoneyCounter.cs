using Character;
using TMPro;
using UnityEngine;

namespace UI
{
    public class MoneyCounter : MonoBehaviour
    {
        [SerializeField] private Buyer _buyer;
        [SerializeField] private TMP_Text _money;

        private void Awake()
        {
            _buyer.MoneyChanged += ChangeMoney;
        }

        private void OnDisable()
        {
            _buyer.MoneyChanged -= ChangeMoney;
        }

        private void ChangeMoney(int money)
        {
            _money.text = money.ToString();
        }
    }
}