<template>
  <div class="provenance">
    <template v-if="loading">Loading...</template>
    <template v-show="!loading">
      <p id="chart"></p>
      <p id="force-legend"></p>

      <p id="force-graph"></p>
    </template>
  </div>
</template>

<script>
/* eslint-disable no-unused-vars */
import * as d3 from "d3";

  export default {
    name: 'provenance',

    data: () => ({
      loading: false
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
        console.log("create chart")
        /* Force-directed graph layout */

        // First draw some lines for a simple legend
        var svg3 = d3.select("#force-legend").append("svg")
                    .attr("width", 300)
                    .attr("height", 65);

        var lineData = [
          { "x1": 5, "y1": 15, "x2": 40, "y2": 15, "name": "used" },
          { "x1": 5, "y1": 30, "x2": 40, "y2": 30, "name": "wasGeneratedBy" },
          { "x1": 5, "y1": 45, "x2": 40, "y2": 45, "name": "hadMember" },
          { "x1": 155, "y1": 15, "x2": 190, "y2": 15, "name": "wasAssociatedWith" },
          { "x1": 155, "y1": 30, "x2": 190, "y2": 30, "name": "wasAttributedTo" },
          { "x1": 155, "y1": 45, "x2": 190, "y2": 45, "name": "wasDerivedFrom" },
          ];

        var lines = svg3.selectAll("line")
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

        var text = svg3.selectAll("text")
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

        var width2 = 960,
            height2 = 500;

        var svg2 = d3.select("#force-graph").append("svg")
              .attr("width", width2)
              .attr("height", height2);

        var defs = svg2.append("svg:defs");
        var nodes = this.ProvenanceData.nodes;
        var links = this.ProvenanceData.links

          var force = d3.layout.force()
              .nodes(nodes)
              .links(links)
              .size([width2, height2])
              .linkDistance(150)
              .charge(-200)
              .on("tick", tick)
              .start();

          // Per-type markers, as they don't inherit styles.
          svg2.append("defs").selectAll("marker")
              .data(["used", "wasGeneratedBy", "wasAssociatedWith", "wasAssociatedWith", "hadMember", "wasDerivedFrom"])
            .enter().append("marker")
              .attr("id", function(d) { return d; })
              .attr("viewBox", "0 -5 10 10")
              .attr("refX", 15)
              .attr("refY", -1.5)
              .attr("markerWidth", 6)
              .attr("markerHeight", 6)
              .attr("orient", "auto")
            .append("path")
              .attr("d", "M0,-5L10,0L0,5")
              .attr("class", function(d) { return "marker "+d; });

          var path = svg2.append("g").selectAll("path")
              .data(force.links())
            .enter().append("path")
              .attr("class", function(d) { return "link " + d.type; })
              //.attr("marker-end", marker('#0000ff'));
              .attr("marker-end", function(d) { return "url(#" + d.type + ")"; });
          
          var shapes = svg2.append("g").selectAll(".shapes")
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

        /*  var circle = shapes.append("circle")
              .filter(function(d){ return d.type == "agent"; })
              .attr("class", function(d) { return d.type; })
              .attr("r", 8)
              .call(force.drag);
        */
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

          var text2 = svg2.append("g").selectAll("text")
              .data(force.nodes())
            .enter().append("text")
              .attr("x", 16)
              .attr("y", ".75em")
              .text(function(d) { return d.name; });

          // Use elliptical arc path segments to doubly-encode directionality.
          function tick() {
            path.attr("d", linkArc);
            ellipse.attr("transform", transform);
            polygon.attr("transform", transform);
            rectangle.attr("transform", transform);
            text2.attr("transform", transform);
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

.link.sankey {
  /*stroke: #ccc;*/
  stroke-opacity: 0.5;
}
.link.sankey:hover {
  stroke-opacity: 0.8;
}
.link.used {
  stroke: #8b0000;
  fill: none;
}

.marker.used {
  stroke: #8b0000;
  fill: #8b0000;
}

.link.wasGeneratedBy {
  stroke: darkgreen;
  fill: none;
}

.marker.wasGeneratedBy {
  stroke: darkgreen;
  fill: darkgreen;
}
.marker.wasGeneratedBy {
  stroke: darkgreen;
  fill: darkgreen;
}

.link.hadMember {
  stroke: #fed37f;
  fill: none;
}
.marker.hadMember {
  stroke: #fed37f;
  fill: #fed37f;
}

.link.wasAssociatedWith {
  /*stroke-dasharray: 0,2 1;*/
  stroke: #ccc;
  fill: none;
}
.marker.wasAssociatedWith {
  stroke: #ccc;
  fill: #ccc;
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
  fill: #fed37f;
  stroke: #000;
}

.entity {
  fill: #fffc87;
  stroke: #808080;
}

.activity {
  fill: #9fb1fc;
  stroke: #0000ff;
}

text {
  font: 10px sans-serif;
  pointer-events: none;
  text-shadow: 0 1px 0 #fff, 1px 0 0 #fff, 0 -1px 0 #fff, -1px 0 0 #fff;
}
</style>
