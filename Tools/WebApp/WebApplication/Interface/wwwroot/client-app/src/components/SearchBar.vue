<template>
  <div class="search__input">
    <v-row>
      <v-col cols="12" md="4">
        <v-select
          :items="omrProperties"
          label="Select OMR property"
          v-model="selectedOmrProperty"
          outlined
          :clearable="true"
        ></v-select>
      </v-col>
      <v-col cols="12" md="8">
        <v-text-field
          v-if="loading"
          loading
          disabled
          outlined
          label="Search..."
          append-icon="search"
        ></v-text-field>
        <v-tooltip top v-else-if="!omrPropertySelected">
          <template v-slot:activator="{ on }">
            <div v-on="on">
              <v-text-field disabled outlined label="Search..." append-icon="search"></v-text-field>
            </div>
          </template>
          <span>Select a OMR property to use for searching the data lake.</span>
        </v-tooltip>
        <v-text-field
          v-else
          :autofocus="true"
          v-model="queryString"
          outlined
          label="Search..."
          append-icon="search"
        ></v-text-field>
      </v-col>
    </v-row>

    <div class="search__checkboxes">
      <v-checkbox
        v-model="checkbox1"
        color="indigo lighten-1"
        class="search__checkbox"
        label="Videos"
      ></v-checkbox>

      <v-tooltip top>
        <template v-slot:activator="{ on }">
          <div v-on="on" style="display:flex">
            <v-checkbox disabled class="search__checkbox" label="Text"></v-checkbox>

            <v-checkbox v-on="on" disabled class="search__checkbox" label="Images"></v-checkbox>
          </div>
        </template>
        <span>Sorry, only videos are available in the lake at the moment.</span>
      </v-tooltip>

      <!-- </v-row> -->
    </div>
    <!-- </v-card> -->

    <v-card v-if="!loading">
      <v-list-item v-for="item in searchResults" :key="item.title" @click="GoToFile(item.videoId)">
        <v-list-item-icon>
          <v-icon v-text="icon"></v-icon>
        </v-list-item-icon>

        <v-list-item-content>
          <v-list-item-title v-text="item.title"></v-list-item-title>
          <v-list-item-subtitle v-text="item.standard"></v-list-item-subtitle>
          <v-list-item-subtitle v-text="item.duration"></v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>
    </v-card>

    <template v-else>
      <p>Loading...</p>
    </template>

    <v-snackbar color="error" :top="true" v-model="snackBar">
      {{ snackBarText }}
      <v-btn dark text @click="snackBar = false">Close</v-btn>
    </v-snackbar>
  </div>
</template>

<script>

  export default {
    name: 'searchbar',

    data: () => ({
      icon: "ondemand_video",
      searchResults: [],
      loading: false,
      snackBar: false,
      snackBarText: 'Search error',
      checkbox1: true,
      queryString: "",
      omrProperties: [],
      selectedOmrProperty: "",
      omrPropertySelected: false,
      timer: null
    }),

    created () {
      this.fetchInitSearchResults();
      this.fetchOmrProperties();
    },

    methods: {
      GoToFile (guid) {
        this.$router.push({ name: 'video', params: { guid: guid }});
      },
       fetchOmrProperties () {
        this.loading = true;
        this.$store.dispatch("GetExistentGenericProperties")
        .then(response => {
          this.omrProperties = response.data.listOfProperties;
          this.loading = false;
        })
        .catch(errors => { 
          this.snackBar = true;
          this.snackBarText = errors;
          this.loading = false;
        });
      },
      fetchInitSearchResults () {
        this.loading = true;
        let obj = { property: null, query: null };
        this.$store.dispatch("Search", obj)
        .then(response => {
          this.searchResults = response.data;
          this.loading = false;
        })
        .catch(errors => { 
          this.snackBar = true;
          this.snackBarText = errors;
          this.loading = false;
        });
      },
      search () {
        this.loading = true;
        let obj = { property: this.selectedOmrProperty, query: this.queryString };
        this.$store.dispatch("Search", obj)
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
    },

    watch: {
      queryString (val) {
        if(val !== undefined) {
          if(val.length >= 2) {
            if (this.timer) {
                  clearTimeout(this.timer);
                  this.timer = null;
              }
              this.timer = setTimeout(() => {
                this.search();
              }, 500);
          }
          if(val.length === 0 || !val.length >= 2) {
            this.fetchInitSearchResults();
          }
        } else {
          this.fetchInitSearchResults();
        }
      },
      selectedOmrProperty (val) {
        if(val !== undefined) {
          if(val.length === 0) {
            this.omrPropertySelected = false;
            this.queryString = "";
          } else if(val.length > 0) {
            this.omrPropertySelected = true;
          }
        } else {
          this.omrPropertySelected = false;
          this.queryString = "";
        }
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
  padding-bottom: 0rem;
  // margin-top: 2rem;
  margin-bottom: 2rem;
}
.v-list-item {
  border-width: 1px;
  border-bottom-style: solid;
  border-color: rgba(0, 0, 0, 0.12);
}
.search__checkboxes {
  margin-bottom: 1rem;
  display: flex;
  .v-input {
    padding-right: 1rem;
  }
}
.search__checkbox {
  margin-top: 0px !important;
}
</style>
