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
    <v-card>
      <div class="tabs">
        <v-tabs color="indigo lighten-1">
          <v-tab @click="HandleTab1">Info</v-tab>
          <v-tab @click="HandleTab2">OCR</v-tab>
          <v-tab @click="HandleTab3">Speech recognition</v-tab>
          <v-tab @click="HandleTab4">Word cloud</v-tab>
          <v-tab @click="HandleTab5">Provenance</v-tab>
          <v-tab @click="HandleTab6">Collaboration</v-tab>
        </v-tabs>

        <div class="tabs__content">
          <template v-if="tab1">
            <p>
              <b>Duration:</b>
              {{currentFile.duration}}
            </p>
            <template v-for="(item, key) in currentFile.generics">
              <b :key="key">{{item.name}}:</b>
              {{item.value}}&nbsp;&nbsp;&nbsp;
            </template>
          </template>
          <template v-if="tab2">
            <p>{{currentFile.ocrAggregated}}</p>
          </template>
          <template v-if="tab3">
            <p>{{currentFile.speechAggregated}}</p>
          </template>
          <template v-if="tab4">
            <div>
              <wordcloud
                :data="words"
                nameKey="name"
                valueKey="value"
                :color="myColors"
                :showTooltip="false"
                :wordClick="wordClickHandler"
              ></wordcloud>
            </div>
          </template>
          <template v-if="tab5">
            <p>provenance</p>
          </template>
          <template v-if="tab6">
            <p>collaboration</p>
          </template>
        </div>
      </div>
    </v-card>

    <v-card :loading="loading">
      <v-card-title>
        <v-text-field
          class="mx-4"
          flat
          hide-details
          label="Search the scenes..."
          prepend-inner-icon="search"
          v-model="searchString"
        ></v-text-field>
      </v-card-title>
      <v-simple-table>
        <template v-slot:default>
          <thead>
            <tr>
              <th class="text-left">Scene</th>
              <th class="text-left">Objects</th>
              <th class="text-left">Optical character recognition (OCR)</th>
              <th class="text-left">Speech recognition</th>
              <th class="text-left">Sentiment analysis</th>
              <th class="text-left"></th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="item in scenes" :key="item.name">
              <td>{{item.sceneNumber}}</td>

              <td>
                <template
                  v-for="object in item.objects"
                >{{object.name}} ({{object.confidence.substring(0, 3)}}) &nbsp;&nbsp;&nbsp;</template>
              </td>
              <td>{{ item.ocr }}</td>
              <td>{{ item.speech }}</td>
              <td>
                <template v-if="item.sentiment !== null">
                <template v-if="item.sentiment.negative >= 0.8">
                  <v-chip class="ma-2" color="red" text-color="white">Neg</v-chip>
                </template>
                </template>
                <template v-if="item.sentiment !== null">
                <template v--if="item.sentiment.neutral >= 0.8">
                  <v-chip class="ma-2">Neu</v-chip>
                </template>
                 </template>
                  <template v-if="item.sentiment !== null">
                <template v-if="item.sentiment.positive >= 0.8">
                  <v-chip class="ma-2" color="green" text-color="white">Pos</v-chip>
                </template>
                   </template>
              </td>
              <td>
                <v-btn
                  small
                  @click="OpenScene(item.startTimeSeconds, item.endTimeSeconds)"
                >Watch scene</v-btn>
              </td>
            </tr>
          </tbody>
        </template>
      </v-simple-table>
    </v-card>

    <!-- video dialog -->
    <v-dialog v-model="dialog" width="50%">
      <v-card>
        <iframe
          :key="iframeKey"
          width="100%"
          height="500"
          :src="iframeSource"
          frameborder="0"
          allow="autoplay"
        ></iframe>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" text @click="dialog = false">Done</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import wordcloud from 'vue-wordcloud'

export default {

  data: () => ({
    dialog: false,
    loading: false,
    currentFile: "",
    tab1: true,
    tab2: false,
    tab3: false,
    tab4: false,
    tab5: false,
    tab6: false,
    iframeKey: 0,
    iframeSource: "",
    searchString: "",
    scenes: [],
    myColors: ['#5C6BC0', '#7986CB', '#9FA8DA', '#C5CAE9'],
    words: []
  }),

  created () {
    this.GetFileByGuid();
  },

  methods: {
    wordClickHandler(name, value, vm) {
      console.log('wordClickHandler', name, value, vm);
    },
    GetFileByGuid () {
      //this.loading = true;
      this.$store.dispatch("GetFileByGuid", this.$route.params.guid)
      .then(response => {
        this.currentFile = response.data.videoMetadataDto;
        this.scenes = this.currentFile.scenes;
        this.words = response.data.words;
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
    OpenScene (sceneStart, sceneEnd) {
      this.dialog = true;
      this.iframeKey++;
      this.GetIframeSource(sceneStart, sceneEnd);
    },
    HandleTab1 () {
      this.tab1 = true;
      this.tab2 = false;
      this.tab3 = false;
      this.tab4 = false;
      this.tab5 = false;
      this.tab6 = false;
    },
    HandleTab2 () {
      this.tab1 = false;
      this.tab2 = true;
      this.tab3 = false;
      this.tab4 = false;
      this.tab5 = false;
      this.tab6 = false;
    },
    HandleTab3 () {
      this.tab1 = false;
      this.tab2 = false;
      this.tab3 = true;
      this.tab4 = false;
      this.tab5 = false;
      this.tab6 = false;
    },
    HandleTab4 () {
      this.tab1 = false;
      this.tab2 = false;
      this.tab3 = false;
      this.tab4 = true;
      this.tab5 = false;
      this.tab6 = false;
    },
    HandleTab5 () {
      this.tab1 = false;
      this.tab2 = false;
      this.tab3 = false;
      this.tab4 = false;
      this.tab5 = true;
      this.tab6 = false;
    },
    HandleTab6 () {
      this.tab1 = false;
      this.tab2 = false;
      this.tab3 = false;
      this.tab4 = false;
      this.tab5 = false;
      this.tab6 = true;
    },
    GetIframeSource (sceneStart, sceneEnd) {
      let youtubeId = this.CurrentFile.youTubeId;
      let source = "https://www.youtube.com/embed/" + youtubeId + "?start=" + sceneStart + "&end=" + sceneEnd + "&autoplay=1";
      this.iframeSource = source;
    },
    SearchVideoScenes () {
      //this.loading = true;
      let model = {
        scenes: this.CurrentFile.scenes,
        searchQuery: this.searchString
      };
      this.$store.dispatch("SearchVideoScenes", model)
      .then(response => {
        this.scenes = response.data;
        this.loading = false;
      })
      .catch(errors => { 
        this.snackBar = true;
        this.snackBarText = errors;
        this.loading = false;
      });
    }

  },

  computed: {
    CurrentFile () {
      return this.currentFile;
    }
  },

  watch: {
    dialog (val) {
      if(val === false) {
        this.iframeKey++;
        this.iframeSource = "";
      }
    },
    searchString (val) {
      if(val.length > 2) {
        this.SearchVideoScenes();
      }
      if(val.length === 0) {
        this.scenes = this.CurrentFile.scenes;
      }
    }
  },

   components: {
    wordcloud
  },

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
.tabs {
  margin-top: 1rem;
  margin-bottom: 1.5rem;
  padding: 1rem;
  &__content {
    margin-top: 1.5rem;
  }
}

// .theme--light.v-text-field--solo-inverted.v-input--is-focused
//   > .v-input__control
//   > .v-input__slot {
//   background: rgba(0, 0, 0, 0.06) !important;

// }
</style>
