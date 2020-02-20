<template>
  <div class="search__input">
    <!-- <v-card class="search__card"> -->

    <v-text-field v-if="loading" loading disabled outlined label="Search..." append-icon="search"></v-text-field>
    <v-text-field v-else outlined label="Search..." append-icon="search"></v-text-field>
    <!-- </v-card> -->

    <v-card>
      <v-list-item v-for="item in searchResults" :key="item.title" @click="GoToFile(item.videoId)">
        <v-list-item-icon>
          <v-icon v-text="icon"></v-icon>
        </v-list-item-icon>

        <v-list-item-content>
          <v-list-item-title v-text="item.title"></v-list-item-title>
          <v-list-item-subtitle v-text="item.stadnard"></v-list-item-subtitle>
          <v-list-item-subtitle v-text="item.duration"></v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>
    </v-card>

    <v-snackbar color="error" :top="true" v-model="snackBar">
      {{ snackBarText }}
      <v-btn dark text @click="snackBar = false">Close</v-btn>
    </v-snackbar>
  </div>
</template>

<script>
  export default {
    name: 'SearchBar',

    data: () => ({
      icon: "ondemand_video",
      searchResults: [],
      loading: false,
      snackBar: false,
      snackBarText: 'Search error'
    }),

    created () {
      this.fetchInitSearchResults();
    },

    methods: {
      GoToFile (guid) {
        this.$router.push({ name: 'video', params: { guid: guid }});
      },
      fetchInitSearchResults () {
        this.loading = true;
        this.$store.dispatch("Search")
        .then(response => {
          this.searchResults = response.data;
          this.loading = false;
        })
        .catch(errors => { 
          this.snackBar = true;
          this.snackBarText = errors;
          this.loading = false;
        });
      }
    }
  }
</script>

<style lang="scss">
.search__input {
  // margin-top: 2rem;
  // max-width: 1200px;
  //margin: 2rem auto;
  padding: 2rem;
}
.search__card {
  padding: 2rem;
  margin-top: 2rem;
}
.v-list-item {
  border-width: 1px;
  border-bottom-style: solid;
  border-color: rgba(0, 0, 0, 0.12);
}
</style>
