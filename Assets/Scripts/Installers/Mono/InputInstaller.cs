using System;
using UI.Player;
using UnityEngine;
using Zenject;

namespace Installers.Mono
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private Joystick _joystick;

        [Space, SerializeField] private AttackButton _attackButton;

        [Space, SerializeField] private EmptySpaceRotationSystem _emptySpaceRotationSystem;

        private BaseInput _input;

        public override void InstallBindings()
        {
            _input = new BaseInput();
            _input.Enable();
            
            _emptySpaceRotationSystem.Initialize(_input);

            Container.Bind<BaseInput>().FromInstance(_input).AsSingle();
            Container.Bind<Joystick>().FromInstance(_joystick).AsSingle();
            Container.Bind<AttackButton>().FromInstance(_attackButton).AsSingle();
            Container.Bind<EmptySpaceRotationSystem>().FromInstance(_emptySpaceRotationSystem).AsSingle();
        }

        private void OnDestroy()
        {
            _input.Disable();
        }
    }
}