using System.Collections.Generic;

namespace Code.Infrastructure
{
    public class Updater : IUpdater
    {
        public List<IUpdateable> Updateables { get; } = new List<IUpdateable>();
        
        public void Update()
        {
            for (int i = 0; i < Updateables.Count; i++)
            {
                Updateables[i].Update();
            }
        }
    }
}