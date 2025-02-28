using Card;
using Other;
using Save;
using System;
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

        [SerializeField] protected float AttackDistance;
        [SerializeField] protected int Damage;
        [SerializeField] protected float DelayBetweenShots;

        [NonSerialized] private ItemState _state = null;

        protected AudioSource Audio;
        protected bool IsShooting = false;
        protected bool IsCritical = false;
        protected int MultiplyDamage = 3;

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

        private void Start()
        {
            Audio = GetComponent<AudioSource>();
        }

        public virtual int Shoot()
        {
            ReportImpact();
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

        public void ReportImpact()
        {
            Shooting?.Invoke();
        }

        public void ApplyGain(CardView view)
        {
            Damage += view.Card.BonusDamage;
        }
    }
}