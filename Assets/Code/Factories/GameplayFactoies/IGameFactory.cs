using Cinemachine;
using Code.Services;
using UnityEngine;

namespace Code.Factories.GameplayFactoies
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer(Vector3 position);
        CinemachineVirtualCamera CreatePlayerCamera(Transform target);
    }
}