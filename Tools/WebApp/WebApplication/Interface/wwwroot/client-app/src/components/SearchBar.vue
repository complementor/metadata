<template>
  <div class="search__input">
    <!-- <v-card class="search__card"> -->

    <v-text-field outlined label="Search..." append-icon="search"></v-text-field>
    <!-- </v-card> -->

    <v-card>
      <v-list-item v-for="item in items" :key="item.title" @click="GoToFile(item.guid)">
        <v-list-item-icon>
          <v-icon v-text="item.icon"></v-icon>
        </v-list-item-icon>

        <v-list-item-content>
          <v-list-item-title v-text="item.title"></v-list-item-title>
          <v-list-item-subtitle v-text="item.subtitle"></v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>
    </v-card>
  </div>
</template>

<script>
  export default {
    name: 'SearchBar',

    data: () => ({
      items: [
        { guid: "1", icon: 'ondemand_video', title: 'Photos', subtitle: 'Jan 9, 2014' },
        { guid: "2", icon: 'play_arrow', title: 'Recipes', subtitle: 'Jan 17, 2014' },
        { guid: "3", icon: 'play_arrow', title: 'Work', subtitle: 'Jan 28, 2014' },
      ],
    }),

    methods: {
      GoToFile (guid) {
        this.$router.push({ name: 'video', params: { guid: guid }});
      }
    }
  }
</script>

<style lang="scss">
.search__input {
  // margin-top: 2rem;
  max-width: 1200px;
  margin: 2rem auto;
}
.search__card {
  padding: 2rem;
  margin-top: 2rem;
}
</style>
