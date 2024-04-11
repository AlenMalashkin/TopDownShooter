using Cinemachine;
using UnityEngine;

namespace Code.Factories.GameplayFactoies
{
    public interface IPlayerFactory : IFactory
    {
        GameObject CreatePlayer(Vector3 position);
        CinemachineVirtualCamera CreatePlayerCamera();
    }
}