# -*- coding: utf-8 -*-
"""
Spyder Editor

This is a temporary script file.
"""
 
import rdflib

dbo = rdflib.Namespace("http://dbpedia.org/ontology/")
dbr = rdflib.Namespace("http://dbpedia.org/resource/")

uri = "something"
res = "something"

mapping_rules = {
    'family': 'relatedTo',
    'singer': 'MusicalArtist',
    'writer': 'Author'
}

p = mapping_rules[input()]
g.add((rdflib.URIRef(uri), dbo[p], rdflib.URIRef(res)))

print(g)