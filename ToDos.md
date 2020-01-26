Findings
========

We can see a couple of directions from here but each has it's own ups and downs.
First off we want to use this ontology for multimedia metadata:
<http://ansgarscherp.net/ontology/m3o.semantic-multimedia.org/>. We could find
the M3O in the format of RDF/XML which explains the concepts and the properties.
In addition it also has a couple of examples of each standard that show how
individuals can be appended.

We expect to write a query that retrieves two individuals of the same class
stored in two different datasets. Ie:

Here are more queries we try to get inspiration from - <https://bit.ly/2GuFinr>

M3O(RDF/XML) -\> sqlite -\> sparql
----------------------------------

### what we need to do

-   manually create m3o dataset for standard1

-   manually create m3o dataset for standard2

-   convert m3o datasets to turtle

-   import it to sql lite

-   install jena

-   learn sparql to query the datasets

### downs

-   hard to work with it because of sofisticated model

-   too scientific, not widely adopted -
    https://twobithistory.org/2018/05/27/semantic-web.html;
    https://medium.com/terminusdb/graph-fundamentals-part-3-graph-schema-languages-1fc25ca294dd

-   jena and sparql can bring extra complexity

### ups

-   sparql is well documented and flexible

M3O(turtle) -\> neo4j -\> Neo4j cypher
--------------------------------------

### what we need to do

-   convert the m3o rdf/xlm to turtle

-   manually create m3o dataset for dublincore

-   manually create m3o dataset for mpeg7

-   convert m3o datasets to turtle

-   import the turtle to neo4j

-   learn cypher to query the graph

### downs

-   some documentation is missing;

-   converting to turtle might bring extra complexity

### ups

-   one of the steps is already implemented - use neo4j semantics library to
    import ontology as a turtle format
    <https://neo4j.com/docs/labs/nsmntx/current/importing-ontologies/>

JSON-LD -\> MongoDb -\> Google Cayley
-------------------------------------

### what we need to do

-   create the ontology or some kind of model(otherwise use the schema.org as
    context)

-   manually create json-ld dataset for standard1

-   manually create json-ld dataset for standard2

-   we could index and query in mongo loosing the connections or use the cayley

### downs

-   OWL is obsolete and not supported with Json-ld

### ups

-   the Liked Data State Of Art
