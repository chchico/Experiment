import Vue from 'vue'
import VueRouter from 'vue-router'

import PageA from './../components/pageA.vue'
// import PageB from '@/components/pageB.vue'
import PageC from './../components/pageC.vue'
import Login from './../components/login.vue'
import Top from './../components/Top.vue'

Vue.use(VueRouter)

const router = new VueRouter({
  routes: [
    { path: '/', component: Top },
    { path: '/login', component: Login },
    { path: '/pageA', component: PageA },
    // { path: '/pageB', component: PageB },
    { path: '/pageC', component: PageC }
  ]
})

router.beforeEach((to, from, next) => {
  console.log(to)
  next()
  /*
  if (to.matched.some(page => page.meta.isPublic) || Store.state.auth.token) {
    next()
  } else {
    next('/login')
  } */
})

export default router
