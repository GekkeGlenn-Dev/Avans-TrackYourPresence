using TrackYourPresenceAPI.Data;

namespace TrackYourPresenceAPI.Services
{
    public abstract class AbstractBaseService
    {
        protected DataContext Context { get; }

        protected AbstractBaseService(DataContext context)
        {
            Context = context;
        }
    }
}