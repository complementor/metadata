<template>
  <div class="collaboration">
    <v-row>
      <v-col cols="12" md="6">
        <!-- <v-row>
                  <v-col cols="12" md="10">
                    <v-text-field v-model="label" label="Label"></v-text-field>
                  </v-col>
                  <v-col cols="12" md="2">
                    <v-btn small>Add</v-btn>
                  </v-col>
        </v-row>-->

        <template v-for="(comment, key) in comments">
          <v-list-item two-line :key="key">
            <v-list-item-avatar>
              <v-img :src="comment.userIcon"></v-img>
            </v-list-item-avatar>
            <v-list-item-content>
              <v-list-item-title>{{comment.user}}</v-list-item-title>
              <v-list-item-subtitle>{{comment.description}}</v-list-item-subtitle>

              <v-list-item-subtitle class="comment__date">{{comment.dateTime}}</v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item>
        </template>
      </v-col>
      <v-col cols="12" md="6">
        <template v-for="(tag, key) in tags">
          <v-chip
            class="ma-2"
            color="light-blue darken-3"
            outlined
            close
            :key="key"
          >{{tag.tagName}} ({{tag.userName}})</v-chip>
        </template>
      </v-col>
    </v-row>
  </div>
</template>

<script>
  export default {
    name: 'collaboration',
    props: {
      comments: Array,
      tags: Array
    },
    data: () => ({

    }),

    created () {

    },

    methods: {

    },

    watch: {

    }

  }
</script>

<style lang="scss">
.comment__date {
  padding-top: 0.5rem;
}
</style>
