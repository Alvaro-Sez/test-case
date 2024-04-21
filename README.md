Test case

I will explain here my thoughts about the design and the architectural process in general.

Technical requirements:

1. The application must prioritise strong security measures due to permissions handling, so the data storage has to be reliable, durable and correct.
2. A critical aspect for achieving optimal user experience is minimising latency during the door unlocking process, requiring very low latency.
3. There is a need to maintain a comprehensive registry of events to track all user actions within the system, we need a way to store large volumes of data that will expand rapidly over time.
4. Scalability is a fundamental requirement.

Reasoning and Technology choice:

- To achieve requirements explained in (1), a SQL database is needed because we have to ensure referential integrity, durability, and correctness of the data as much as we can, and SQL databases provide ACID transactions to deal with these aspects.

- In contrast, SQL databases are not easy to scale horizontally, so we can store user information and permission-related data in this database but not events due to their growing nature.
 
- To deal with requirements in (2), we need a view of the data related to the unlocking action to ensure fast queries. This action is supposed to be the most requested action of our application, so the database in which we store this view has to be read-wise scalable, fulfilling requirement (4).

- While there are other storage systems faster than SQL databases, such as caching, we also desire more durability. Therefore, a NoSQL database is well-suited for this case, and we can also use this database as storage for our events, providing a solution for requirement (3).
