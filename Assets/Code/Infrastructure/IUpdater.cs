using System.Collections.Generic;
using Code.Services;

namespace Code.Infrastructure
{
    public interface IUpdater : IService
    {
        List<IUpdateable> Updateables { get; }
        void Update();
    }
}