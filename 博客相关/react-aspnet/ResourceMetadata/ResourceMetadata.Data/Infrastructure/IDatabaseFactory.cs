using System;

namespace ResourceMetadata.Data.Infrastructure 
{
    public interface IDatabaseFactory : IDisposable
    {
        ResourceManagerEntities Get();
    }
}
