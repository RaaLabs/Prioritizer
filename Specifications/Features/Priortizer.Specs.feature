Feature: Prioritizer
    Check if prioritizer is working

    Background:
        Given the prioritized timeseries
            | prioritized                          |
            | b9cd4c34-6d3b-4f74-9be9-f44b4fab2d4c |
            | 84acff21-e288-4111-af50-79e4224535a0 |
            | 5c9d2bf0-7740-4551-9600-ce074398604e |
            | 411e52ac-a912-43d8-a399-8b1595aeac02 |

    Scenario: Check if timeseries is prioritized
        When the following timeseries are requested
            | timeseries                           |
            | b9cd4c34-6d3b-4f74-9be9-f44b4fab2d4c |
            | 84acff21-e288-4111-af50-79e4224535a0 |
            | nonprior-itiz-ed51-9600-ce074398604e |
            | nonprior-itiz-edd8-a399-8b1595aeac02 |
        Then the following timeseries will be prioritized
            | timeseries                           |
            | b9cd4c34-6d3b-4f74-9be9-f44b4fab2d4c |
            | 84acff21-e288-4111-af50-79e4224535a0 |
        And the following timeseries will be non prioritized
            | timeseries                           |
            | nonprior-itiz-ed51-9600-ce074398604e |
            | nonprior-itiz-edd8-a399-8b1595aeac02 |

