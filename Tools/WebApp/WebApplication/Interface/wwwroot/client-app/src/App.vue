<template>
  <v-app id="inspire">
    <v-navigation-drawer v-model="drawer" app>
      <v-list dense>
        <v-list-item link>
          <v-list-item-action>
            <v-icon @click="GoToHome">mdi-home</v-icon>
          </v-list-item-action>
          <v-list-item-content @click="GoToHome">
            <v-list-item-title>Home</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>

    <!-- https://vuetifyjs.com/en/styles/colors#material-colors -->

    <v-app-bar color="indigo lighten-1" dark app>
      <v-app-bar-nav-icon @click.stop="drawer = !drawer" />
      <v-toolbar-title>Data lake</v-toolbar-title>
    </v-app-bar>

    <v-content>
      <v-container>
        <router-view></router-view>
      </v-container>
    </v-content>
    <!-- <v-footer color="indigo" app>
      <span class="white--text">&copy; 2019</span>
    </v-footer>-->
  </v-app>
</template>

<script>


export default {
  name: 'App',

  methods: {
    GoToHome () {
      this.$router.push({ name: 'home' });
    }
  },
  
  components: {

  },

  data: () => ({
     drawer: true
  }),
};
</script>

<style lang="scss">
.theme--light.v-app-bar.v-toolbar.v-sheet {
  //background-color: white !important;
}
.container {
  // max-width: 1200px !important;
}
.v-content {
  // background: #f5f5f5;
}
.v-list-item__content {
  a {
    color: black !important;
    text-decoration: none !important;
  }
}
</style>