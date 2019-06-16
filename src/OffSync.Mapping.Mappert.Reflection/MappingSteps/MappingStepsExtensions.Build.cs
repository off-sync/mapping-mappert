/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace OffSync.Mapping.Mappert.Reflection.MappingSteps
{
    public static partial class MappingStepsExtensions
    {
        public static object Build(
            this MappingStep mappingStep,
            object[] froms)
        {
            return mappingStep
                .BuilderInvoke
                .Invoke(
                    mappingStep.Builder,
                    froms);
        }
    }
}
