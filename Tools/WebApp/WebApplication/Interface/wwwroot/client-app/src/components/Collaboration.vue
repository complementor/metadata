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

        <v-list-item two-line>
          <v-list-item-avatar>
            <v-img
              src="https://images.pexels.com/photos/220453/pexels-photo-220453.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=200"
            ></v-img>
          </v-list-item-avatar>
          <v-list-item-content>
            <v-list-item-title>Peter</v-list-item-title>
            <v-list-item-subtitle>I did a classification task on this video.</v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
        <v-list-item two-line>
          <v-list-item-avatar>
            <v-img src="https://www.joancanto.com/wp-content/uploads/2017/04/H10B0527.jpg"></v-img>
          </v-list-item-avatar>
          <v-list-item-content>
            <v-list-item-title>Anne</v-list-item-title>
            <v-list-item-subtitle>Great job, Peter. Very useful for my work.</v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>

        <v-list-item two-line>
          <v-list-item-avatar>
            <v-img src="https://cdn.vuetifyjs.com/images/lists/2.jpg"></v-img>
          </v-list-item-avatar>
          <v-list-item-content>
            <v-list-item-title>Thomas</v-list-item-title>
            <v-list-item-subtitle>Nice findings! I added a few more tags.</v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
      </v-col>
      <v-col cols="12" md="6">
        <v-chip class="ma-2" color="indigo lighten-1" outlined close>Entertainment</v-chip>
        <v-chip class="ma-2" color="indigo lighten-1" outlined close>Nature</v-chip>
        <v-chip class="ma-2" color="indigo lighten-1" outlined close>New Zealand</v-chip>
        <v-chip class="ma-2" color="indigo lighten-1" outlined close>Travel</v-chip>
        <v-chip class="ma-2" color="indigo lighten-1" outlined close>Youtube</v-chip>
        <v-chip class="ma-2" color="indigo lighten-1" outlined close>Auckland</v-chip>
      </v-col>
    </v-row>
  </div>
</template>

<script>
  export default {
    name: 'collaboration',

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
</style>
