/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using RaaLabs.TimeSeries.Modules;
using Machine.Specifications;
using Dolittle.Logging;
using Dolittle.Serialization.Json;
using Moq;
using System.Threading.Tasks;
using It = Machine.Specifications.It;

namespace RaaLabs.TimeSeries.Prioritizer.for_PrioritizerHandler
{
    public class when_receiving_new_data_points
    {
        protected static ILogger logger;
        protected static ISerializer serializer;
        protected static Mock<IPrioritizer> prioritizer;
        protected static Mock<ICommunicationClient> client;

        protected static Guid prioritized_tag = Guid.NewGuid();
        protected static Guid nonprioritized_tag = Guid.NewGuid();

        protected static PrioritizerHandler handler;

        Establish context = () =>
        {
            logger = Mock.Of<ILogger>();
            serializer = Mock.Of<ISerializer>();
            prioritizer = new Mock<IPrioritizer>();

            prioritizer.Setup(_ => _.IsPrioritized(prioritized_tag)).Returns(true);
            prioritizer.Setup(_ => _.IsPrioritized(nonprioritized_tag)).Returns(false);

            client = new Mock<ICommunicationClient>();

            handler = new PrioritizerHandler(serializer, logger, prioritizer.Object, client.Object);
        };

        Because of = () =>
        {
            handler.Handle(new DataPoint<dynamic> { TimeSeries = prioritized_tag, Timestamp = Timestamp.UtcNow, Value = MathF.PI }).Wait();
            handler.Handle(new DataPoint<dynamic> { TimeSeries = nonprioritized_tag, Timestamp = Timestamp.UtcNow, Value = MathF.PI }).Wait();
        };

        It should_send_prioritized_data_points_to_prioritized_output = () => client.Verify(_ => _.SendAsJson("prioritized", Moq.It.Is<DataPoint<dynamic>>(obj => obj.TimeSeries == prioritized_tag)), Times.Once);
        It should_send_nonprioritized_data_points_to_nonprioritized_output = () => client.Verify(_ => _.SendAsJson("nonprioritized", Moq.It.Is<DataPoint<dynamic>>(obj => obj.TimeSeries == nonprioritized_tag)), Times.Once);
    }
}