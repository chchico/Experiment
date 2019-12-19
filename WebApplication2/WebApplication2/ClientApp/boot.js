// Bootstrapのスタイルシート側の機能を読み込む
import './../node_modules/bootstrap/dist/css/bootstrap.min.css'
// BootstrapのJavaScript側の機能を読み込む
import 'bootstrap'

import Vue from 'vue'

import Router from './router/index'
import Store from './store/index'

import App from './app.vue'

export default new Vue({
  el: '#app',
  router: Router,
  store: Store,
  components: { App },
  template: '<App/>'
})
