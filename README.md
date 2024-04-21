#### Test case

I will explain here my thoughts about the design and the architectural process in general.

## Technical requirements:

1. The application must prioritise strong security measures due to permissions handling, so the data storage has to be reliable, durable and correct.
2. A critical aspect for achieving optimal user experience is minimising latency during the door unlocking process, requiring very low latency.
3. There is a need to maintain a comprehensive registry of events to track all user actions within the system, we need a way to store large volumes of data that will expand rapidly over time.
4. Scalability is a fundamental requirement.

___

## Reasoning and Technology choice:

- To achieve requirements explained in (1), a SQL database is needed because we have to ensure referential integrity, durability, and correctness of the data as much as we can, and SQL databases provide ACID transactions to deal with these aspects.

- In contrast, SQL databases are not easy to scale horizontally, so we can store user information and permission-related data in this database but not events due to their growing nature.
 
- To deal with requirements in (2), we need a view of the data related to the unlocking action to ensure fast queries. This action is supposed to be the most requested action of our application, so the database in which we store this view has to be read-wise scalable, fulfilling requirement (4).

- While there are other storage systems faster than SQL databases, such as caching, we also desire more durability. Therefore, a NoSQL database is well-suited for this case, and we can also use this database as storage for our events, providing a solution for requirement (3).

___

## System Architecture and Implementation Process:

After considering the reasoning provided in the previous paragraph, I determined that the CQRS pattern was well suited for this case and proceeded with its implementation.

### <ins> Initially, I came up with the following design: </ins>


FOTO

Pros:

- Easy to implement

Cons:

- Difficult for development, prone to conflicts during integration
- Very low scalability; queries and commands cannot be scaled separately
- Prone to inconsistent data between the two storages; as everything is in the same process, an exception during writing on one database could result in an inconsistent system.


### <ins> So then I attempted (unsuccessfully) to implement this design: </ins>


FOTO

Pros:

- Very good for development; different teams can work within their contexts because we have all the query management separated from the commands.
- High scalability; due to the decoupled nature, we can have multiple producers and consumers, enabling horizontal scaling.
- The queue provide effective ways of handling retries, ensuring system consistency.

Cons:

- Very challenging to implement for me. I encountered numerous difficulties connecting everything, especially with the rabbit client library (the official), this library didn't have async production. As a workaround, I implemented a background service, but I faced significant challenges when communicating events to the query side, so I realised i would not have time to finish in this way so I gave up.


#### So I decided to start again after 4 days of development and try to implement a distributed monolith (successfully, I hope):
___

## Explanation of the resultant design:

#### I attempted to define strict boundaries from the query side to the command side. The workflow is as follows:

- The command side receives a request and processes it. Subsequently, this side of the monolith publishes a message to a queue (RabbitMQ).
- We have a worker process that consumes the queue and has access to the code of the query side. This worker has no reference to any project on the write side; its sole purpose is to consume the queue and activate the code of the query side of the monolith.
- On the query side, we have event handlers that the worker will trigger, this handlers will process the events and register the changes in the database to maintain consistency between both sides of the data.

###### The query side manages two aspects of the application:
- Intensive reads, such as when a request to unlock a door is made.
- Storing events of every user interaction with the doors, as well as when a user's permission rights are raised.

FOTO

Pros:

- Facilitates development; integration is enhanced as the command side has no reference to any project on the query side, and the query side only references the domain of the command side, just to use the contracts of the events.
- Supports scalability well; we can duplicate the code of each side into two different repositories and prepare separate instances.

Cons:

- Complex (at least for me).
- Difficult to establish clear boundaries.

