using Cinemachine;
using Code.GameplayLogic;
using Code.Services;
using UnityEngine;

namespace Code.Factories.GameplayFactoies
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer(Vector3 position);
        IWeapon CreateWeapon();
        CinemachineVirtualCamera CreatePlayerCamera();
    }
}