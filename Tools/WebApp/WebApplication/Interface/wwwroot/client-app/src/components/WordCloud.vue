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
      wordColors: ['#0277BD', '#039BE5', '#4FC3F7', '#B3E5FC'],
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
