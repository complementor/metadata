import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import File from '../views/File.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'home',
    component: Home
  },
  {
    path: '/file/:guid',
    name: 'video',
    component: File
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
