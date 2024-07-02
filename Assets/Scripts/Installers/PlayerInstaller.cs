using Cinemachine;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private GameObject _playerPrefab;

    [SerializeField] private Transform _playerInstancePoint;

    [SerializeField] private CinemachineVirtualCamera _mainVirtualCamera;

    public override void InstallBindings()
    {
        var player = Container.InstantiatePrefab(_playerPrefab, _playerInstancePoint.transform.position, Quaternion.identity, null);

        _mainVirtualCamera.Follow = player.transform;
        _mainVirtualCamera.LookAt = player.transform;

        Container.Bind<PlayerPicker>().FromInstance(player.GetComponent<PlayerPicker>());
    }
}
