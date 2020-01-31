from owlready2 import *

#onto = get_ontology("http://ansgarscherp.net/ontology/m3o.semantic-multimedia.org/examples/xmp-example.owl").load()
onto = get_ontology("")
onto.load()
print(onto.imported_ontologies)
print("\n")
classes = list(onto.classes())
print(classes)
print("\n")
properties = list(onto.properties())
print(properties)
print("\n")

#my_world = World()
#onto = my_world.get_ontology("http://ansgarscherp.net/ontology/m3o.semantic-multimedia.org/ontology/2009/09/16/annotation.owl").load()
graph = default_world.as_rdflib_graph()
r = list(graph.query("""
                     select ?region ?predicate ?object 
                     where {?subject ?predicate ?object} 
                     limit 100
                     """))
print(r)

print("IRI REULST: ")
print(onto.search(hasRegionDataValue = "dk-DK"))

print("\n")


MATCH (n) RETURN *


MATCH (n)
DETACH DELETE n

CALL semantics.importOntology("file:///P:/src/Triplets/turtle/vw.owl","Turtle",{ classLabel : 'Category', objectPropertyLabel: 'Rel', dataTypePropertyLabel: 'Prop'})
CALL semantics.importRDF("file:///P:/src/Triplets/turtle/vw.owl","Turtle", {typesToLabels: true, keepCustomDataTypes: true, handleMultival: 'ARRAY'})
MATCH (n) RETURN *

# ----- source 
# https://jbarrasa.com/2016/04/06/building-a-semantic-graph-in-neo4j/

# ---- old 
CREATE INDEX ON :Resource(uri)
CALL semantics.importRDF("P:/src/Triplestore/methods/vw.owl","RDF/XML")
CALL semantics.importRDF("P:/src/Triplestore/methods/n-triples.net","N-Triples")
CALL semantics.importRDF("http://ansgarscherp.net/ontology/m3o.semantic-multimedia.org/examples/xmp-example.owl","Turtle")
CALL semantics.importRDF("https://github.com/RDFLib/rdflib/blob/master/examples/foaf.rdf","Turtle")
CALL semantics.importOntology("file:///P:/src/Triplets/turtle/ComboM3O.owl","Turtle")
CALL semantics.importOntology("file:///P:/src/Triplets/turtle/vw.owl","Turtle")
CALL semantics.importOntology("file:///P:/src/Triplets/turtle/ComboM3O.owl","Turtle", { classLabel : 'Category', objectPropertyLabel: 'Rel', dataTypePropertyLabel: 'Prop'})

https://github.com/RDFLib/rdflib/blob/master/examples/foaf.rdf
http://ansgarscherp.net/ontology/m3o.semantic-multimedia.org/examples/xmp-example.owl
CALL semantics.importOntology("P:/src/GenericOntologyMapping/TripleStorage/m3oSamples/xmp-example.owl","Turtle")

semantics.LiteOntoImport is depricated

http://ansgarscherp.net/ontology/m3o.semantic-multimedia.org/examples/xmp-example.owl