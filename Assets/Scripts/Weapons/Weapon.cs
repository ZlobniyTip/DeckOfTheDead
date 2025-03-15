using System;
using Card;
using Other;
using Save;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour, IProduct
    {
        [SerializeField] protected WeaponType _weaponType;

        [SerializeField] private ItemType _type;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [SerializeField] private int _price;
        [SerializeField] private int _index;
        [SerializeField] private float _attackDistance;
        [SerializeField] private int _damage;
        [SerializeField] private float _delayBetweenShots;

        private ItemState _state = null;

        public event Action Shooting;

        public WeaponType WeaponKind => _weaponType;
        public float ShotCooldown => DelayBetweenShots;
        public float AttackRange => AttackDistance;
        public ItemType Type => _type;
        public Sprite Icon => _icon;
        public string Name => _name;
        public int Price => _price;
        public int Index => _index;
        public int DamageValue => Damage;
        public ItemState State => _state ??= new ItemState(ItemStatus.NotPurchased);

        protected AudioSource Audio { get; set; }
        protected bool IsShooting { get; set; } = false;

        protected float AttackDistance => _attackDistance;
        protected int Damage => _damage;
        protected float DelayBetweenShots => _delayBetweenShots;

        private void Start()
        {
            Audio = GetComponent<AudioSource>();
        }

        public virtual int Shoot()
        {
            OnReportImpact();
            Audio.Play();

            return Damage;
        }

        public void StopShooting()
        {
            IsShooting = false;
        }

        public void Init(ItemStatus state, int level)
        {
            State.SetStatus(state);
            State.SetParameters(level);
        }

        public void OnReportImpact()
        {
            Shooting?.Invoke();
        }

        public void ApplyGain(CardView view)
        {
            _damage += view.Card.BonusDamage;
        }
    }
}