<template>
  <div class="video">
    <v-row>
      <v-col cols="12" md="8">
        <v-btn
          @click="GoToHome"
          style="margin-left:0px!important"
          class="ma-2 video__goback"
          outlined
          color="indigo lighten-1"
        >
          <v-icon dark>chevron_left</v-icon>Go back
        </v-btn>
        <br />
        <br />
      </v-col>
      <v-col cols="12" md="4"></v-col>
    </v-row>

    <!-- {{currentFile}} -->
    <h4>
      <v-icon style="padding-right:0.5rem">ondemand_video</v-icon>
      {{currentFile.title}}
    </h4>

    <br />
    <v-card :loading="loading">
      <v-card-title>
        <v-text-field append-icon="search" label="Search" single-line hide-details></v-text-field>
      </v-card-title>
      <v-simple-table>
        <template v-slot:default>
          <thead>
            <tr>
              <th class="text-left">Scene number</th>
              <th class="text-left">Objects</th>
              <th class="text-left">Optical character recognition</th>
              <th class="text-left">Speech recognition</th>
              <th class="text-left"></th>
              <!-- <th class="text-left">Sentiment analysis</th> -->
            </tr>
          </thead>
          <tbody>
            <tr v-for="item in desserts" :key="item.name">
              <td>1</td>
              <td>{{ item.name }}</td>
              <td>{{ item.calories }}</td>
              <td>{{ item.sr }}</td>
              <td>
                <v-btn small @click="OpenScene">Open scene</v-btn>
              </td>
            </tr>
          </tbody>
        </template>
      </v-simple-table>
    </v-card>

    <!-- video dialog -->
    <v-dialog v-model="dialog" width="50%">
      <v-card>
        <!-- <v-card-text>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</v-card-text>

        <v-divider></v-divider>-->
        <iframe width="100%" height="500" src="https://www.youtube.com/embed/tgbNymZ7vqY"></iframe>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" text @click="dialog = false">Done</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>


export default {


  data: () => ({
    dialog: false,
    loading: false,
    currentFile: "",
    desserts: [
        {
          name: 'Frozen Yogurt',
          calories: 159,
          sr: "test1"
        },
        {
          name: 'Ice cream sandwich',
          calories: 237,
          sr: "test2"
        },
      ],
  }),

  created () {
    this.GetFileByGuid();
  },

  methods: {
    GetFileByGuid () {
      //this.loading = true;
      this.$store.dispatch("GetFileByGuid", this.$route.params.guid)
      .then(response => {
        this.currentFile = response.data;
        this.loading = false;
      })
      .catch(errors => { 
        this.snackBar = true;
        this.snackBarText = errors;
        this.loading = false;
      });
    },
    GoToHome () {
      this.$router.push({ name: 'home' });
    },
    OpenScene () {
      this.dialog = true;
    }
  },

  components: {
 
  }
}
</script>

<style lang="scss">
.video {
  padding: 2rem;
  padding-top: 1rem;

  &__goback {
    margin-bottom: 2rem;
  }
}
.v-dialog {
  .v-card {
    padding-top: 2rem;
  }
}
</style>
