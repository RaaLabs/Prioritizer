// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Autofac;
using System.Threading.Tasks;

namespace RaaLabs.Edge.Prioritizer.Specs.Drivers
{
    public class ScopeExposer : IRunAsync
    {
        public ScopeHolder ScopeHolder { get; set; }

        public ScopeExposer(ILifetimeScope scope, ScopeHolder scopeHolder)
        {
            ScopeHolder = scopeHolder;
            ScopeHolder.Scope = scope;
        }

        public async Task Run()
        {
            await Task.CompletedTask;
        }

    }
    public class ScopeHolder
    {
        public ILifetimeScope Scope { get; set; }

    }
}