Feature: System integration
    Testing system integration

    Background: A running application
        Given a config file with the following mapping
            | prioritized                          |
            | b9cd4c34-6d3b-4f74-9be9-f44b4fab2d4c |
            | 84acff21-e288-4111-af50-79e4224535a0 |
            | 5c9d2bf0-7740-4551-9600-ce074398604e |
            | 411e52ac-a912-43d8-a399-8b1595aeac02 |
        And the application is running

    Scenario: Incoming event
        When the following events are received
            | Timeseries                           | Value   | Timestamp     |
            | b9cd4c34-6d3b-4f74-9be9-f44b4fab2d4c | 12.1    | 1616400494883 |
            | 84acff21-e288-4111-af50-79e4224535a0 | 12.1    | 1616400494882 |
            | 5c9d2bf0-7740-4551-9600-ce074398604e | 11232.1 | 1616400494881 |
            | sauron                               | 12      | 1616400494885 |
            | gandalf                              | 12.1    | 1616400494883 |
        Then the following events are produced
            | Timeseries                           | Value   | Timestamp     | OutputName     |
            | b9cd4c34-6d3b-4f74-9be9-f44b4fab2d4c | 12.1    | 1616400494883 | prioritized    |
            | 84acff21-e288-4111-af50-79e4224535a0 | 12.1    | 1616400494882 | prioritized    |
            | 5c9d2bf0-7740-4551-9600-ce074398604e | 11232.1 | 1616400494881 | prioritized    |
            | sauron                               | 12      | 1616400494885 | nonprioritized |
            | gandalf                              | 12.1    | 1616400494883 | nonprioritized |
