using DumpingWiki.Model;

using System.Collections.Generic;

namespace DumpingWiki.Core.Interface
{
    public interface IDataService
    {
        public List<CompiledData> Extract(string pathFiles);
    }
}
