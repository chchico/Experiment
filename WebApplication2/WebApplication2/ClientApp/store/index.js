import Vue from 'vue'
import Vuex from 'vuex'

import auth from './modules/auth'
import counter from './modules/counter'

// プラグインを登録
Vue.use(Vuex)

// ストアを作成
const store = new Vuex.Store({
  modules: {
    auth,
    counter
  }
})

export default store
