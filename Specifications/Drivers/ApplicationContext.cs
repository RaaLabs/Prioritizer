// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Autofac;
using RaaLabs.Edge.Modules.EdgeHub;
using RaaLabs.Edge.Modules.EventHandling;
using RaaLabs.Edge.Modules.Configuration;
using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.IO.Abstractions;

namespace RaaLabs.Edge.Prioritizer.Specs.Drivers
{
    public class ApplicationContext
    {
        public ApplicationBuilder ApplicationBuilder { get; private set; }
        public Application Application { get; private set; }
        public MockFileSystem FileSystem { get; private set; }
        public ScopeHolder ScopeHolder { get; private set; }
        public ILifetimeScope Scope { get; private set; }

        public IDictionary<string, object> Instances { get; private set; }

        public ApplicationContext()
        {
            ApplicationBuilder = new ApplicationBuilder();
            Instances = new Dictionary<string, object>();
            FileSystem = new MockFileSystem();
            ScopeHolder = new ScopeHolder();

            Application = new ApplicationBuilder()
                .WithModule<EventHandling>()
                .WithModule<EdgeHub>()
                .WithModule<Configuration>()
                .WithHandler<PrioritizerHandler>()
                .WithType<Prioritizer>()
                .WithManualRegistration(builder => builder.RegisterInstance(FileSystem).As<IFileSystem>())
                .WithManualRegistration(builder => builder.RegisterInstance(ScopeHolder))
                .WithTask<ScopeExposer>()
                .Build();
        }
        public ILifetimeScope StartScope()
        {
            var container = Application.Container;
            Scope = container.BeginLifetimeScope();

            return Scope;
        }


        public void Run()
        {
            Application.Run();
        }

        public object ResolveInstance(string name, Type type)
        {
            var instance = Scope.Resolve(type);
            Instances.Add(name, instance);

            return instance;
        }

        public void AddConfigurationFile(string filePath, string content)
        {
            FileSystem.AddFile(filePath, content);
        }
    }
}
