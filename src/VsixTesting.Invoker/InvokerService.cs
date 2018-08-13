﻿// Copyright (c) 2018 Jose Torres. All rights reserved. Licensed under the Apache License, Version 2.0. See LICENSE.md file in the project root for full license information.

namespace VsixTesting.Invoker
{
    using System;
    using System.Reflection;

    public class InvokerService
    {
        public object InvokeMethod(string assemblyPath, string @class, string method, object obj, params object[] arguments)
        {
            var assembly = Assembly.LoadFrom(assemblyPath);
            var assemblyPublicKey = BitConverter.ToString(assembly.GetName().GetPublicKey()).Replace("-", string.Empty);

            if (assemblyPublicKey.Equals("0024000004800000940000000602000000240000525341310004000001000100d55b6b77e75142dc724b2e721fcddb95564623d7404f5c98bd595ca11c8e2ee1bcd63f46c24e446be029895312590edf9eb489e9ad5d10442a3ef87b965cc7f8400d829f68b8c8ee8a2c90901b5fb9d38acb0d82c8aae828390b8ff2d21bea1be6b296c3ba41bc57852c18784c7e6c078f2e48ee9b40af8353023ed6667afbb3", StringComparison.OrdinalIgnoreCase))
            {
                return assembly
                    .GetType(@class)
                    .GetMethod(method, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                    .Invoke(obj, arguments);
            }

            return null;
        }
    }
}
