<template>
  <div class="provenance">
    <template v-if="loading">Loading...</template>
    <template v-show="!loading">
      <div class="legends__container">
        <span id="force-legend-1"></span>
      </div>
      <v-row>
        <v-col cols="12" md="6">
          <div class="graph__container">
            <span id="chart"></span>
            <span id="force-graph"></span>
          </div>
        </v-col>
        <v-col cols="12" md="6">
          <div class="prov__model">
            <img v-show="!loading" src="@/assets/key-concepts.png" />
          </div>
        </v-col>
      </v-row>
    </template>
  </div>
</template>

<script>
/* eslint-disable no-unused-vars */
import * as d3 from "d3";

  export default {
    name: 'provenance',

    data: () => ({
      loading: false,
      graph: {
        "nodes": [
          {"id": "red"},
          {"id": "orange"},
          {"id": "yellow"},
          {"id": "green"},
          {"id": "blue"},
          {"id": "violet"}
        ],
        "links": [
          {"source": "red", "target": "yellow"},
          {"source": "red", "target": "blue"},
          {"source": "red", "target": "green"},
          {"source": "red", "target": "violet"},
          {"source": "violet", "target": "orange"}
        ]
      }
    }),

    beforeCreate () {
      this.$store.dispatch("GetProvenanceData")
      .then(response => { console.log(response.data) }).catch(errors => { console.log(errors) });
    },

    mounted () {
      this.loading = true;
      setTimeout(() => {
        this.renderGraph();
      }, 1000); 
    },

    computed: {
      ApiValue () {
        return this.$store.getters.GetApiValue;
      },
      ProvenanceData () {
        return this.$store.getters.GetProvenanceData;
      }
    },

    methods: {
      renderGraph() {
        // d3 version: npm i d3@3   

        // draw legend 1
        var svg1 = d3.select("#force-legend-1").append("svg")
              .attr("width", 320)
              .attr("height", 65);

        var lineData = [
            { "x1": 5, "y1": 15, "x2": 40, "y2": 15, "name": "used" },
            { "x1": 5, "y1": 30, "x2": 40, "y2": 30, "name": "wasGeneratedBy" },
            { "x1": 155, "y1": 15, "x2": 190, "y2": 15, "name": "wasAssociatedWith" },
            { "x1": 155, "y1": 30, "x2": 190, "y2": 30, "name": "wasAttributedTo" },
            { "x1": 155, "y1": 45, "x2": 190, "y2": 45, "name": "wasDerivedFrom" },
          ];

        var lines = svg1.selectAll("line")
              .data(lineData)
              .enter()
              .append("line");

        var lineAttributes = lines
              .attr("x1", function (d) { return d.x1; })
              .attr("y1", function (d) { return d.y1; })
              .attr("x2", function (d) { return d.x2; })
              .attr("y2", function (d) { return d.y2; })
              .attr("stroke-width", 2)
              .attr("stroke", "black")
              .attr("class", function (d) { return "link "+ d.name });

        var text = svg1.selectAll("text")
              .data(lineData)
              .enter()
              .append("text");

        var textLabels = text
              .attr("x", function(d) { return d.x2+10; })
              .attr("y", function(d) { return d.y2+4; })
              .text( function (d) { return d.name; })
              .attr("font-family", "sans-serif")
              .attr("font-size", "20px")
              .attr("class", function (d) { return "marker "+ d.name })
              .attr("stroke-width", 0);

        // draw graph
        var width = 650,
            height = 500;

        var svg3 = d3.select("#force-graph").append("svg")
              .attr("viewBox", "0 0 " + width + " " + height);
              // .attr("width", width)
              // .attr("height", height);

        var defs = svg3.append("svg:defs");
        var nodes = this.ProvenanceData.nodes;
        var links = this.ProvenanceData.links
        console.log(nodes);
        console.log(links)

        var force = d3.layout.force()
            .nodes(nodes)
            .links(links)
            .size([width, height])
            .linkDistance(100)
            .charge(-2500)
            .on("tick", tick)
            .start();

        svg3.append("defs").selectAll("marker")
            .data(["used", "wasGeneratedBy", "wasAssociatedWith", "wasAttributedTo", "wasDerivedFrom"])
            .enter().append("marker")
            .attr("id", function(d) { return d; })
            .attr("viewBox", "0 -5 10 10")
            .attr("refX", 15)
            .attr("refY", -1)
            .attr("markerWidth", 6)
            .attr("markerHeight", 6)
            .attr("orient", "auto")
            .append("path")
            .attr("d", "M0,-5L10,0L0,5")
            .attr("class", function(d) { return "marker "+d; });

          var path = svg3.append("g").selectAll("path")
              .data(force.links())
              .enter().append("path")
              .attr("class", function(d) { return "link " + d.type; })
              .attr("marker-end", function(d) { return "url(#" + d.type + ")"; });
          
          var shapes = svg3.append("g").selectAll(".shapes")
              .data(force.nodes())
              .enter();

          var ellipse = shapes.append("ellipse")
              .filter(function(d){ return d.type == "entity"; })
              .attr("class", function(d) { return d.type; })
              .attr("rx", 15)
              .attr("ry", 10)
              .attr("cx", 0)
              .attr("cy", 0)
              .call(force.drag);

          var polygon = shapes.append("path")
              .filter(function(d){ return d.type == "agent"; })
              .attr("class", function(d) { return d.type; })
              .attr("d", "M-15,7 L-15,-5 L0,-12 L15,-5 L15,7 L-15,7")
              .call(force.drag);

          var rectangle = shapes.append("rect")
              .filter(function(d){ return d.type == "activity"; })
              .attr("class", function(d) { return d.type; })
              .attr("x", -14)
              .attr("y", -9)
              .attr("width", 28)
              .attr("height", 18)
              .call(force.drag);

          var text3 = svg3.append("g").selectAll("text")
              .data(force.nodes())
              .enter().append("text")
              .attr("x", 20)
              .attr("y", 5)
              .text(function(d) { return d.name; });

          function tick() {
            path.attr("d", linkArc);
            ellipse.attr("transform", transform);
            polygon.attr("transform", transform);
            rectangle.attr("transform", transform);
            text3.attr("transform", transform);
          }

          function linkArc(d) {
            var dx = d.target.x - d.source.x,
                dy = d.target.y - d.source.y,
                dr = Math.sqrt(dx * dx + dy * dy);
            return "M" + d.source.x + "," + d.source.y + "A" + dr + "," + dr + " 0 0,1 " + d.target.x + "," + d.target.y;
          }

          function transform(d) {
            return "translate(" + d.x + "," + d.y + ")";
          }

          this.loading = false;
      }
    },

    watch: {

    }

  }
  /* eslint-enable no-unused-vars */
</script>

<style lang="scss">
/* svg graphs */
svg {
  background-color: #fff;
  border: 1px solid silver;
}

.link {
  fill: none;
  stroke: #aaa;
  stroke-width: 2px;
}
.link.used {
  stroke: #5c6bc0;
  fill: none;
}

.marker.used {
  stroke: #5c6bc0;
  fill: #5c6bc0;
}

.link.wasGeneratedBy {
  stroke: #4caf50;
  fill: none;
}

.marker.wasGeneratedBy {
  stroke: #4caf50;
  fill: #4caf50;
}

.link.wasAssociatedWith {
  /*stroke-dasharray: 0,2 1;*/
  stroke: grey;
  fill: none;
}
.marker.wasAssociatedWith {
  stroke: grey;
  fill: grey;
}

.link.wasDerivedFrom {
  stroke-dasharray: 0, 2 1;
  stroke: #aaa;
  fill: none;
}
.marker.wasDerivedFrom {
  stroke: #aaa;
  fill: #aaa;
}

.link.wasAttributedTo {
  /*stroke-dasharray: 0,2 1;*/
  stroke: #ccc;
  fill: none;
}
.marker.wasAttributedTo {
  stroke: #ccc;
  fill: #ccc;
}

circle {
  fill: #ccc;
  stroke: #333;
  stroke-width: 1.5px;
}

.agent {
  fill: #e6eb9e;
  stroke: #000;
}

.entity {
  fill: #fffedf;
  stroke: #000;
}

.activity {
  fill: #cfceff;
  stroke: #000;
}

text {
  font: 12px sans-serif;
  pointer-events: none;
  text-shadow: 0 1px 0 #fff, 1px 0 0 #fff, 0 -1px 0 #fff, -1px 0 0 #fff;
}
.legends__container {
  p {
    float: left;
  }
  #force-legend-2 {
    margin-left: 2rem;
  }
}
.graph__container {
  // max-width: 50%;
  // display: inherit;
  // float: left;
}
.prov__model {
  display: flex;
  align-items: center;
  justify-content: center;
  padding-top: 7%;
  img {
    max-width: 100%;
  }
}
</style>
