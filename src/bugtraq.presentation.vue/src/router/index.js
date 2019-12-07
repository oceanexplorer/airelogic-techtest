import Vue from 'vue'
import VueRouter from 'vue-router'
import Bugs from '../views/Bugs.vue'

Vue.use(VueRouter);

const routes = [
  {
    path: '/',
    name: 'bugs',
    component: Bugs
  },
  {
    path: '/users',
    name: 'users',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/Users.vue')
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
