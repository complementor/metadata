<template>
  <div class="wordcloud">
    <vuewordcloud
      :data="words"
      nameKey="name"
      valueKey="value"
      :color="wordColors"
      :showTooltip="false"
      :wordClick="wordClickHandler"
    ></vuewordcloud>
  </div>
</template>

<script>
import vuewordcloud from 'vue-wordcloud'

  export default {
    name: 'wordcloud',
    props: {
      words: Array
    },

    data: () => ({
      wordColors: ['#5C6BC0', '#7986CB', '#9FA8DA', '#C5CAE9'],
    }),

    created () {

    },

    methods: {
      wordClickHandler(name, value, vm) {
        console.log('wordClickHandler', name, value, vm);
      }
    },

    watch: {

    },

    components: {
      vuewordcloud
    }

  }
</script>

<style lang="scss">
</style>
