#from owlready2 import *
#y_world = World()
#onto = my_world.get_ontology("http://ansgarscherp.net/ontology/m3o.semantic-multimedia.org/examples/xmp-example.owl").load()
#graph = default_world.as_rdflib_graph()
#r = list(graph.query("""
                    # select ?region ?predicate ?object
                     #where {?subject ?predicate ?object}
                     #limit 100
                    # """))
#print(r)
#print("IRI REULST: ")
#print(onto.search(hasRegionDataValue = "dk-DK"))
#print("\n")

from owlready2 import *
import rdflib

#onto = get_ontology("http://ansgarscherp.net/ontology/m3o.semantic-multimedia.org/examples/xmp-example.owl").load()


my_data = '''
    <rdf:RDF xmlns="http://m3o.semantic-multimedia.org/examples/xmp-example.owl#" xmlns:dublincore="http://m3o.semantic-multimedia.org/mappings/2011/04/20/dublincore.owl#" xmlns:annotation="http://m3o.semantic-multimedia.org/ontology/2009/09/16/annotation.owl#" xmlns:rdfs="http://www.w3.org/2000/01/rdf-schema#" xmlns:owl2xml="http://www.w3.org/2006/12/owl2-xml#" xmlns:DUL="http://www.loa-cnr.it/ontologies/DUL.owl#" xmlns:xmp-example="http://m3o.semantic-multimedia.org/examples/xmp-example.owl#" xmlns:xmp="http://m3o.semantic-multimedia.org/mappings/2011/04/20/xmp.owl#" xmlns:owl="http://www.w3.org/2002/07/owl#" xmlns:xsd="http://www.w3.org/2001/XMLSchema#" xmlns:rdf="http://www.w3.org/1999/02/22-rdf-syntax-ns#" xml:base="http://m3o.semantic-multimedia.org/examples/xmp-example.owl">
    <owl:Ontology rdf:about="">
    <owl:imports rdf:resource="http://m3o.semantic-multimedia.org/mappings/2011/04/20/dublincore.owl"/>
    <owl:imports rdf:resource="http://m3o.semantic-multimedia.org/mappings/2011/04/20/xmp.owl"/>
    </owl:Ontology>
    <!--
     
        ///////////////////////////////////////////////////////////////////////////////////////
        //
        // Object Properties
        //
        ///////////////////////////////////////////////////////////////////////////////////////
         
    -->
    <!--
     http://www.loa-cnr.it/ontologies/DUL.owl#classifies 
    -->
    <owl:ObjectProperty rdf:about="http://www.loa-cnr.it/ontologies/DUL.owl#classifies"/>
    <!--  http://www.loa-cnr.it/ontologies/DUL.owl#defines  -->
    <owl:ObjectProperty rdf:about="http://www.loa-cnr.it/ontologies/DUL.owl#defines"/>
    <!--  http://www.loa-cnr.it/ontologies/DUL.owl#hasPart  -->
    <owl:ObjectProperty rdf:about="http://www.loa-cnr.it/ontologies/DUL.owl#hasPart"/>
    <!--
     http://www.loa-cnr.it/ontologies/DUL.owl#hasSetting 
    -->
    <owl:ObjectProperty rdf:about="http://www.loa-cnr.it/ontologies/DUL.owl#hasSetting"/>
    <!--  http://www.loa-cnr.it/ontologies/DUL.owl#satisfies  -->
    <owl:ObjectProperty rdf:about="http://www.loa-cnr.it/ontologies/DUL.owl#satisfies"/>
    <!--
     
        ///////////////////////////////////////////////////////////////////////////////////////
        //
        // Data properties
        //
        ///////////////////////////////////////////////////////////////////////////////////////
         
    -->
    <!--
     http://www.loa-cnr.it/ontologies/DUL.owl#hasRegionDataValue 
    -->
    <owl:DatatypeProperty rdf:about="http://www.loa-cnr.it/ontologies/DUL.owl#hasRegionDataValue"/>
    <!--
     
        ///////////////////////////////////////////////////////////////////////////////////////
        //
        // Classes
        //
        ///////////////////////////////////////////////////////////////////////////////////////
         
    -->
    <!--
     http://m3o.semantic-multimedia.org/mappings/2011/04/20/dublincore.owl#LanguageConcept 
    -->
    <owl:Class rdf:about="http://m3o.semantic-multimedia.org/mappings/2011/04/20/dublincore.owl#LanguageConcept"/>
    <!--
     http://m3o.semantic-multimedia.org/mappings/2011/04/20/dublincore.owl#LanguageRegion 
    -->
    <owl:Class rdf:about="http://m3o.semantic-multimedia.org/mappings/2011/04/20/dublincore.owl#LanguageRegion"/>
    <!--
     http://m3o.semantic-multimedia.org/mappings/2011/04/20/dublincore.owl#SubjectConcept 
    -->
    <owl:Class rdf:about="http://m3o.semantic-multimedia.org/mappings/2011/04/20/dublincore.owl#SubjectConcept"/>
    <!--
     http://m3o.semantic-multimedia.org/mappings/2011/04/20/dublincore.owl#SubjectRegion 
    -->
    <owl:Class rdf:about="http://m3o.semantic-multimedia.org/mappings/2011/04/20/dublincore.owl#SubjectRegion"/>
    <!--
     http://m3o.semantic-multimedia.org/mappings/2011/04/20/xmp.owl#OwnerConcept 
    -->
    <owl:Class rdf:about="http://m3o.semantic-multimedia.org/mappings/2011/04/20/xmp.owl#OwnerConcept"/>
    <!--
     http://m3o.semantic-multimedia.org/mappings/2011/04/20/xmp.owl#OwnerRegion 
    -->
    <owl:Class rdf:about="http://m3o.semantic-multimedia.org/mappings/2011/04/20/xmp.owl#OwnerRegion"/>
    <!--
     http://m3o.semantic-multimedia.org/mappings/2011/04/20/xmp.owl#RightsManagementConcept 
    -->
    <owl:Class rdf:about="http://m3o.semantic-multimedia.org/mappings/2011/04/20/xmp.owl#RightsManagementConcept"/>
    <!--
     http://m3o.semantic-multimedia.org/mappings/2011/04/20/xmp.owl#WebStatementConcept 
    -->
    <owl:Class rdf:about="http://m3o.semantic-multimedia.org/mappings/2011/04/20/xmp.owl#WebStatementConcept"/>
    <!--
     http://m3o.semantic-multimedia.org/mappings/2011/04/20/xmp.owl#WebStatementRegion 
    -->
    <owl:Class rdf:about="http://m3o.semantic-multimedia.org/mappings/2011/04/20/xmp.owl#WebStatementRegion"/>
    <!--
     http://m3o.semantic-multimedia.org/ontology/2009/09/16/annotation.owl#AnnotatedConcept 
    -->
    <owl:Class rdf:about="http://m3o.semantic-multimedia.org/ontology/2009/09/16/annotation.owl#AnnotatedConcept"/>
    <!--
     http://m3o.semantic-multimedia.org/ontology/2009/09/16/annotation.owl#AnnotationPattern 
    -->
    <owl:Class rdf:about="http://m3o.semantic-multimedia.org/ontology/2009/09/16/annotation.owl#AnnotationPattern"/>
    <!--
     http://m3o.semantic-multimedia.org/ontology/2009/09/16/annotation.owl#AnnotationSituation 
    -->
    <owl:Class rdf:about="http://m3o.semantic-multimedia.org/ontology/2009/09/16/annotation.owl#AnnotationSituation"/>
    <!--
     http://www.loa-cnr.it/ontologies/DUL.owl#InformationObject 
    -->
    <owl:Class rdf:about="http://www.loa-cnr.it/ontologies/DUL.owl#InformationObject"/>
    <!--
     
        ///////////////////////////////////////////////////////////////////////////////////////
        //
        // Individuals
        //
        ///////////////////////////////////////////////////////////////////////////////////////
         
    -->
    <!--
     http://m3o.semantic-multimedia.org/examples/xmp-example.owl#ac-1 
    -->
    <annotation:AnnotatedConcept rdf:about="#ac-1">
    <rdf:type rdf:resource="http://www.w3.org/2002/07/owl#Thing"/>
    <DUL:classifies rdf:resource="#doc-1"/>
    </annotation:AnnotatedConcept>
    <!--
     http://m3o.semantic-multimedia.org/examples/xmp-example.owl#doc-1 
    -->
    <DUL:InformationObject rdf:about="#doc-1">
    <rdf:type rdf:resource="http://www.w3.org/2002/07/owl#Thing"/>
    <DUL:hasSetting rdf:resource="#id3as-2"/>
    </DUL:InformationObject>
    <!--
     http://m3o.semantic-multimedia.org/examples/xmp-example.owl#id3ap-2 
    -->
    <annotation:AnnotationPattern rdf:about="#id3ap-2">
    <rdf:type rdf:resource="http://www.w3.org/2002/07/owl#Thing"/>
    <DUL:defines rdf:resource="#ac-1"/>
    <DUL:defines rdf:resource="#lc-1"/>
    <DUL:defines rdf:resource="#rmc-1"/>
    <DUL:defines rdf:resource="#sc-1"/>
    </annotation:AnnotationPattern>
    <!--
     http://m3o.semantic-multimedia.org/examples/xmp-example.owl#id3as-2 
    -->
    <owl:Thing rdf:about="#id3as-2">
    <rdf:type rdf:resource="http://m3o.semantic-multimedia.org/ontology/2009/09/16/annotation.owl#AnnotationSituation"/>
    <DUL:satisfies rdf:resource="#id3ap-2"/>
    </owl:Thing>
    <!--
     http://m3o.semantic-multimedia.org/examples/xmp-example.owl#lc-1 
    -->
    <dublincore:LanguageConcept rdf:about="#lc-1">
    <rdf:type rdf:resource="http://www.w3.org/2002/07/owl#Thing"/>
    <DUL:classifies rdf:resource="#lr-1"/>
    </dublincore:LanguageConcept>
    <!--
     http://m3o.semantic-multimedia.org/examples/xmp-example.owl#lr-1 
    -->
    <owl:Thing rdf:about="#lr-1">
    <rdf:type rdf:resource="http://m3o.semantic-multimedia.org/mappings/2011/04/20/dublincore.owl#LanguageRegion"/>
    <DUL:hasRegionDataValue rdf:datatype="http://www.w3.org/2001/XMLSchema#string">en-US</DUL:hasRegionDataValue>
    <DUL:hasSetting rdf:resource="#id3as-2"/>
    </owl:Thing>
    <!--
     http://m3o.semantic-multimedia.org/examples/xmp-example.owl#oc-1 
    -->
    <xmp:OwnerConcept rdf:about="#oc-1">
    <rdf:type rdf:resource="http://www.w3.org/2002/07/owl#Thing"/>
    <DUL:classifies rdf:resource="#or-1"/>
    </xmp:OwnerConcept>
    <!--
     http://m3o.semantic-multimedia.org/examples/xmp-example.owl#or-1 
    -->
    <xmp:OwnerRegion rdf:about="#or-1">
    <rdf:type rdf:resource="http://www.w3.org/2002/07/owl#Thing"/>
    <DUL:hasRegionDataValue rdf:datatype="http://www.w3.org/2001/XMLSchema#string">Example, Inc.</DUL:hasRegionDataValue>
    <DUL:hasSetting rdf:resource="#id3as-2"/>
    </xmp:OwnerRegion>
    <!--
     http://m3o.semantic-multimedia.org/examples/xmp-example.owl#rmc-1 
    -->
    <owl:Thing rdf:about="#rmc-1">
    <rdf:type rdf:resource="http://m3o.semantic-multimedia.org/mappings/2011/04/20/xmp.owl#RightsManagementConcept"/>
    <DUL:hasPart rdf:resource="#oc-1"/>
    <DUL:hasPart rdf:resource="#wtc-1"/>
    </owl:Thing>
    <!--
     http://m3o.semantic-multimedia.org/examples/xmp-example.owl#sc-1 
    -->
    <owl:Thing rdf:about="#sc-1">
    <rdf:type rdf:resource="http://m3o.semantic-multimedia.org/mappings/2011/04/20/dublincore.owl#SubjectConcept"/>
    <DUL:classifies rdf:resource="#sr-1"/>
    </owl:Thing>
    <!--
     http://m3o.semantic-multimedia.org/examples/xmp-example.owl#sr-1 
    -->
    <owl:Thing rdf:about="#sr-1">
    <rdf:type rdf:resource="http://m3o.semantic-multimedia.org/mappings/2011/04/20/dublincore.owl#SubjectRegion"/>
    <DUL:hasRegionDataValue rdf:datatype="http://www.w3.org/2001/XMLSchema#string">Technical Specification</DUL:hasRegionDataValue>
    <DUL:hasSetting rdf:resource="#id3as-2"/>
    </owl:Thing>
    <!--
     http://m3o.semantic-multimedia.org/examples/xmp-example.owl#wtc-1 
    -->
    <owl:Thing rdf:about="#wtc-1">
    <rdf:type rdf:resource="http://m3o.semantic-multimedia.org/mappings/2011/04/20/xmp.owl#WebStatementConcept"/>
    <DUL:classifies rdf:resource="#wtr-1"/>
    </owl:Thing>
    <!--
     http://m3o.semantic-multimedia.org/examples/xmp-example.owl#wtr-1 
    -->
    <owl:Thing rdf:about="#wtr-1">
    <rdf:type rdf:resource="http://m3o.semantic-multimedia.org/mappings/2011/04/20/xmp.owl#WebStatementRegion"/>
    <DUL:hasRegionDataValue rdf:datatype="http://www.w3.org/2001/XMLSchema#anyURI">http://www.example.com/...</DUL:hasRegionDataValue>
    <DUL:hasSetting rdf:resource="#id3as-2"/>
    </owl:Thing>
    </rdf:RDF>
'''
#print(my_data)

g = rdflib.Graph()

g.parse(data=my_data, format="application/rdf+xml")
print(len(g))

#q = g.query("""select ?thing where { 
#        ?thing rdf:type annotation:AnnotatedConcept .}""")

q = g.query("""select ?thing where { 
        ?thing rdf:type annotation:AnnotatedConcept .}""")


for row in q:
    print(row)