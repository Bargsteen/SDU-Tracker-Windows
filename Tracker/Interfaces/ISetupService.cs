using System.Collections.Generic;

namespace Tracker.Interfaces
{
    public interface ISetupService
    {
        void SetupAppByUri(string maybeUri);
    }
}