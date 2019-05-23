/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.TimeSeries.Modules;
using Machine.Specifications;

namespace Dolittle.TimeSeries.Prioritizer.for_Prioritizer
{
    public class when_checking_if_time_series_is_prioritized_and_it_is_not
    {
        protected static Prioritizer prioritizer;
        protected static bool result;

        Establish context = () => prioritizer = new Prioritizer(new PrioritizerConfiguration(new Dolittle.TimeSeries.Modules.TimeSeries[0]));

        Because of = () => result = prioritizer.IsPrioritized(Guid.NewGuid());

        It should_not_be_prioritized = () => result.ShouldBeFalse();       
    }
}