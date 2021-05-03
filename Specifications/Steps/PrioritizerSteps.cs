// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using TechTalk.SpecFlow;
using System.Collections.Generic;
using FluentAssertions;
namespace RaaLabs.Edge.Prioritizer.Specs.StepDefinitions
{
    [Binding]
    public class PrioritizerSteps
    {
        private readonly Prioritizer _prioritizer;
        private HashSet<string> _nonPrioritizedTimeseries;
        private HashSet<string> _prioritizedTimeseries;

        public PrioritizerSteps(Prioritizer prioritizer)
        {
            _prioritizer = prioritizer;
            _prioritizedTimeseries = new HashSet<string>();
            _nonPrioritizedTimeseries = new HashSet<string>();
        }

        [When(@"the following timeseries are requested")]
        public void TheFollowingTimeseriesAreRequested(Table table)
        {
            foreach (var row in table.Rows)
            {
                var timeseries = row["timeseries"];

                if (_prioritizer.IsPrioritized(timeseries))
                {
                    _prioritizedTimeseries.Add(timeseries);
                }
                else
                {
                    _nonPrioritizedTimeseries.Add(timeseries);
                }
            }
        }
        [Then(@"the following timeseries will be prioritized")]
        public void ThenTheFollowingTimeseriesWillBePrioritized(Table timeseries)
        {
            foreach (var row in timeseries.Rows)
            {
                _prioritizedTimeseries.Should().Contain(row["timeseries"]);
            }
        }
        [Then(@"the following timeseries will be non prioritized")]
        public void ThenTheFollowingTimeseriesWillBeNonPrioritized(Table timeseries)
        {
            foreach (var row in timeseries.Rows)
            {
                _nonPrioritizedTimeseries.Should().Contain(row["timeseries"]);
            }
        }
    }
}