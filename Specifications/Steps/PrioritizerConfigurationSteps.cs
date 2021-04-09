
using TechTalk.SpecFlow;
using System.Linq;
using BoDi;
using Serilog;
using System.Collections.Generic;

namespace RaaLabs.Edge.Prioritizer.Specs.StepDefinitions
{
    [Binding]
    public class PrioritizerConfigurationSteps
    {
        private readonly IObjectContainer _container;
        public PrioritizerConfigurationSteps(IObjectContainer container)
        {
            _container = container;
        }
        
        [Given(@"the prioritized timeseries")]
        public void GivenThePrioritizedTimeseries(Table table)
        {
            IEnumerable<string> prioritized = table.Rows.Select(row => row["prioritized"]);
            var prioritizer = new PrioritizerConfiguration(prioritized);
            _container.RegisterInstanceAs<PrioritizerConfiguration>(prioritizer); 
        }

    }
}