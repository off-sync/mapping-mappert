using System;

namespace OffSync.Mapping.Mappert.Practises.Validation
{
    public class MappingRuleValidationResult
    {
        public MappingRuleValidationResults Result { get; set; }

        #region Invalid results
        public string Message { get; set; }

        public Exception Exception { get; set; }
        #endregion

        #region SetBuilder results
        public Delegate Builder { get; set; }
        #endregion
    }
}