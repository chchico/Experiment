import axios from 'axios'

export default {
  namespaced: true,
  state: {
    token: '',
    userName: '',
    rolls: []
  },
  getters: {
    userName: (state) => state.userName,
    rolls: (state) => state.rolls
  },
  actions: {
    // commit オブジェクトしか使わない場合は下記の方法でもOK
    // async login({ commit }, data) {
    async login(context, data) {
      const params = new URLSearchParams()
      params.append('grant_type', 'password')
      params.append('username', data.email)
      params.append('password', data.password)

      const response = await axios.post('/Token', params)

      context.commit('save', {
        // accessToken: response.data['access_token']
        accessToken: response.data.access_token
      })
    }
  },
  mutations: {
    // ミューテーションを登録（ミューテーションは同期でなくてはだめ）
    save(state, data) {
      state.token = data.accessToken
    }
  }
}
