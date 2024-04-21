Test case

I will explain here my thoughts about the design and the architectural process in general.

Technical requirements:

1. The application must prioritise strong security measures due to permissions handling, so the data storage has to be reliable, durable and correct.
2. A critical aspect for achieving optimal user experience is minimising latency during the door unlocking process, requiring very low latency.
3. There is a need to maintain a comprehensive registry of events to track all user actions within the system, we need a way to store large volumes of data that will expand rapidly over time.
4. Scalability is a fundamental requirement.

