﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.VisualStudio.ProjectSystem.Properties;

namespace Microsoft.VisualStudio.ProjectSystem.VS.Properties
{
    [ExportInterceptingPropertyValueProvider("LocalDebuggerCommand", ExportInterceptingPropertyValueProviderFile.UserFileWithXamlDefaults)]
    internal class LocalDebuggerCommandValueProvider : InterceptingPropertyValueProviderBase
    {
        public const string DefaultCommand = "";

        public override async Task<string> OnGetEvaluatedPropertyValueAsync(string evaluatedPropertyValue, IProjectProperties defaultProperties)
        {
            var command = evaluatedPropertyValue;

            // If the existing command is the empty string, then a custom command has not been set. We therefore want to return dotnet here,
            // and return exec + exe + args in the LocalDebuggerCommandArguments interceptor.
            if (command == DefaultCommand)
                command = await defaultProperties.GetEvaluatedPropertyValueAsync(WindowsLocalDebugger.DotnetPathProperty).ConfigureAwait(false);

            return command;
        }
    }
}
