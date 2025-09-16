# Experimental Query Chaining with Unit of Work

This part of the project was created as an **experiment** to explore how far I could go with chaining queries on top of a Unit of Work pattern.

The idea was to build a flexible way of composing queries using a chain-like style, similar to how `Include`, `Where`, and other operations can be combined together in a natural flow. The goal was to avoid writing conditions once in a static way and instead allow them to be chained dynamically.

While the implementation worked and proved that the concept is possible, it was **not intended to be a professional or production-ready solution**. It was mainly a playground to test how such chaining could be built on top of the abstractions I already had, and to see if the developer experience could be improved.

In short, this was a **learning experiment** that gave me insight into how chaining and query composition can be integrated with Unit of Work. The results were interesting and partially successful, but this should be treated as an experimental approach rather than a finalized design.
