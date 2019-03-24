/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Concepts;

namespace Dolittle.Edge.TimeSeriesPrioritizer
{
    /// <summary>
    /// Represents the concept of a time series
    /// </summary>
    public class TimeSeriesId : ConceptAs<Guid>
    {
        /// <summary>
        /// Implicitly convert from <see cref="Guid"/> to <see cref="TimeSeriesId"/>
        /// </summary>
        /// <param name="value">TimeSeries as <see cref="Guid"/></param>
        public static implicit operator TimeSeriesId(Guid value)
        {
            return new TimeSeriesId {Value = value};
        }
    }
}
