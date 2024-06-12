using System.Collections.Generic;

namespace Code.UI.Windows
{
    public interface ILocalizable
    {
        void Localize(Dictionary<string, string> localization);
    }
}