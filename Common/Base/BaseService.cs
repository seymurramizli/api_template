using LazyCache;

namespace Common.Base
{
    public class BaseService
    {
        public IAppCache cache = new CachingService();
    }
}
