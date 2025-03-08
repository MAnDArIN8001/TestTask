using System;
using UnityEngine;
using Zenject;

namespace Installers.Mono
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private Joystick _joystick;

        private BaseInput _input;

        public override void InstallBindings()
        {
            _input = new BaseInput();
            _input.Enable();

            Container.Bind<BaseInput>().FromInstance(_input).AsSingle();
            Container.Bind<Joystick>().FromInstance(_joystick).AsSingle();
        }

        private void OnDestroy()
        {
            _input.Disable();
        }
    }
}