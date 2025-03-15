using Weapons;

namespace Character.StateMachine.States
{
    public class MovementState : IState
    {
        private readonly Player Ñharacter;

        protected IStateSwitcher StateSwitcher { get; set; }
        protected Weapon CurrentWeapon { get; set; }

        public MovementState(IStateSwitcher stateSwitcher, Player character)
        {
            StateSwitcher = stateSwitcher;
            Ñharacter = character;
        }

        protected CharacterView CharacterView => Ñharacter.CharacterView;
        protected Player Character => Ñharacter;
        protected float MoveSpeed => Character.Movement.NavMeshAgent.speed;
        protected bool IsAttacking => Character.CharacterShooting.IsShooting;

        public virtual void Enter()
        {
            Ñharacter.CharacterShooting.ChangedWeapon += OnChangeWeapon;
        }

        public virtual void Exit()
        {
            Ñharacter.CharacterShooting.ChangedWeapon -= OnChangeWeapon;
        }

        public virtual void Update()
        {
        }

        protected void OnChangeWeapon()
        {
            Exit();
            Enter();
        }
    }
}