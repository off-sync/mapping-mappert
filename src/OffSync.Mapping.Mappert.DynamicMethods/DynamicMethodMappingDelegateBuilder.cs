using System;
using System.Collections.Generic;
using System.Reflection.Emit;

using OffSync.Mapping.Mappert.Practises;
using OffSync.Mapping.Mappert.Practises.MappingRules;

namespace OffSync.Mapping.Mappert.DynamicMethods
{
    public class DynamicMethodMappingDelegateBuilder :
        IMappingDelegateBuilder
    {
        public MappingDelegate<TSource, TTarget> CreateMappingDelegate<TSource, TTarget>(
            IEnumerable<IMappingRule> mappingRules)
        {
            #region Pre-conditions
            if (mappingRules == null)
            {
                throw new ArgumentNullException(nameof(mappingRules));
            }
            #endregion

            var mapMethod = new DynamicMethod(
                $"Map{typeof(TSource).Name}To{typeof(TTarget).Name}",
                null,
                new Type[] { typeof(TSource), typeof(TTarget), typeof(Delegate[]) });

            var builders = new List<Delegate>();

            using (var methodBuilder = new DynamicMethodBuilder(mapMethod))
            {
                foreach (var mappingRule in mappingRules)
                {
                    methodBuilder.AddMappingRule(mappingRule);

                    if (mappingRule.Builder != null)
                    {
                        builders.Add(mappingRule.Builder);
                    }
                }
            }

            var mapDelegate = (MapDelegate<TSource, TTarget>)mapMethod.CreateDelegate(
                typeof(MapDelegate<TSource, TTarget>));

            var mappingDelegate = new DynamicMethodMappingDelegate<TSource, TTarget>(
                mapDelegate,
                builders.ToArray());

            return mappingDelegate.Map;
        }
    }
}
