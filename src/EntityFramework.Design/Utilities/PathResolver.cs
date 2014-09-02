// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
#if ASPNETCORE50 || ASPNET50
using Microsoft.Framework.Runtime;
using Microsoft.Framework.Runtime.Infrastructure;
#endif

namespace Microsoft.Data.Entity.Design.Utilities
{
    internal static class PathResolver
    {
        private static string ApplicationBaseDirectory
        {
            get
            {
#if ASPNETCORE50 || ASPNET50
                var locator = CallContextServiceLocator.Locator;

                if (locator != null)
                {
                    var appEnv = (IApplicationEnvironment)locator.ServiceProvider.GetService(typeof(IApplicationEnvironment));
                    return appEnv.ApplicationBasePath;
                }
#endif

#if NET451 || ASPNET50
                return AppDomain.CurrentDomain.BaseDirectory;
#else
                return ApplicationContext.BaseDirectory;
#endif
            }
        }

        public static string ResolveAppRelativePath(string path)
        {
            return Path.Combine(ApplicationBaseDirectory, path);
        }
    }
}
