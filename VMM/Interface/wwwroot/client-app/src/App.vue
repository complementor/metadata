<template>
  <v-app id="inspire">
    <v-navigation-drawer v-model="drawer" app>
      <v-list dense>
        <v-list-item two-line>
          <v-list-item-avatar>
            <img src="https://cdn.vuetifyjs.com/images/lists/2.jpg" />
          </v-list-item-avatar>

          <v-list-item-content>
            <v-list-item-title>Thomas</v-list-item-title>
            <v-list-item-subtitle>Logged In</v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>

        <v-list-item link>
          <v-list-item-action>
            <v-icon @click="GoToHome">mdi-home</v-icon>
          </v-list-item-action>
          <v-list-item-content @click="GoToHome">
            <v-list-item-title>Home</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <v-list-item>
          <v-list-item-action>
            <v-icon>mdi-account</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>Account</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <v-list-item>
          <v-list-item-action>
            <v-icon>arrow_back</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>Log out</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>

    <!-- https://vuetifyjs.com/en/styles/colors#material-colors -->

    <v-app-bar color="light-blue darken-3" dark app>
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