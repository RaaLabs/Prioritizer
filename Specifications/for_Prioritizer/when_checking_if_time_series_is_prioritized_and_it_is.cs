/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Machine.Specifications;

namespace Dolittle.Edge.TimeSeriesPrioritizer.for_Prioritizer
{
    public class when_checking_if_time_series_is_prioritized_and_it_is
    {
        protected static Guid first_prioritized_tag = Guid.NewGuid();
        protected static Guid second_prioritized_tag = Guid.NewGuid();
        protected static Guid third_prioritized_tag = Guid.NewGuid();

        protected static Prioritizer prioritizer;
        protected static bool result;

        Establish context = () => prioritizer = new Prioritizer(new PrioritizerConfiguration(new TimeSeriesId[] {
            first_prioritized_tag, second_prioritized_tag, third_prioritized_tag
        }));

        Because of = () => result = prioritizer.IsPrioritized(second_prioritized_tag);

        It should_be_prioritized = () => result.ShouldBeTrue();       
    }    
}