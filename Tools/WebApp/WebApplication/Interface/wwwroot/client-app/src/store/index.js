import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    api: "https://localhost:44349/api/",
    currentFile: "",
    omrProperties: []
  },
  getters: {
    GetApiValue: state => state.api,
    GetOmrProperties: state => state.omrProperties
  },
  mutations: {

  },
  actions: {
    Search(context, obj) {
      return new Promise((resolve, reject) => {
        axios.get(context.state.api + "files/search?property=" + obj.property + "&" + "query=" + obj.query)
          .then(response => {
            resolve(response)
          }).catch(errors => {
            reject(errors)
          });
      })
    },
    GetFileByGuid(context, guid) {
      return new Promise((resolve, reject) => {
        axios.get(context.state.api + "files/" + guid)
          .then(response => {
            resolve(response)
            context.state.currentFile = response.data;
          }).catch(errors => {
            reject(errors)
          });
      })
    },
    SearchVideoScenes(context, model) {
      return new Promise((resolve, reject) => {
        axios.post(context.state.api + "files/video/search", model)
          .then(response => {
            resolve(response)
          }).catch(errors => {
            reject(errors)
          });
      })
    },
    GetExistentGenericProperties(context) {
      return new Promise((resolve, reject) => {
        axios.get(context.state.api + "files/genericproperties")
          .then(response => {
            resolve(response)
            context.state.omrProperties = response.data;
          }).catch(errors => {
            reject(errors)
          });
      })
    }
  },
  modules: {
  }
})
