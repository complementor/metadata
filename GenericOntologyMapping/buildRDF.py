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

CALL semantics.importOntology("P:/src/GenericOntologyMapping/TripleStorage/m3oSamples/xmp-example.owl","Turtle")
CALL semantics.LiteOntoImport("P:/src/GenericOntologyMapping/TripleStorage/m3oSamples/xmp-example.owl","Turtle")

semantics.LiteOntoImport

http://ansgarscherp.net/ontology/m3o.semantic-multimedia.org/examples/xmp-example.owl