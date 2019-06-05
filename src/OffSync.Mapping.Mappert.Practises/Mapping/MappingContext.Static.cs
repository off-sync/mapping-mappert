using System.Threading;

namespace OffSync.Mapping.Mappert.Practises.Mapping
{
    public sealed partial class MappingContext
    {
        private static readonly AsyncLocal<IMappingContext> _localMappingContext = new AsyncLocal<IMappingContext>();

        public static IMappingContext Current
        {
            get
            {
                if (_localMappingContext.Value == null)
                {
                    _localMappingContext.Value = new MappingContext();
                }

                return _localMappingContext.Value;
            }
            set
            {
                _localMappingContext.Value = value;
            }
        }
    }
}
