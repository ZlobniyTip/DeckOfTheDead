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

        public virtual void Enter()
        {
            Ñharacter.CharacterShooting.ChangedWeapon += IsChangedWeapon;
        }

        public virtual void Exit()
        {
            Ñharacter.CharacterShooting.ChangedWeapon -= IsChangedWeapon;
        }

        public virtual void Update()
        {
        }

        protected bool IsMoving() => Character.Movement.NavMeshAgent.speed == 0;

        protected bool IsAttacking() => Character.CharacterShooting.IsShooting;

        protected void IsChangedWeapon()
        {
            Exit();
            Enter();
        }
    }
}