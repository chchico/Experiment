export default {
  namespaced: true,
  state: {
    count: 0
  },
  getters: {
    count: (state) => state.count
  },
  mutations: {
    // カウントアップするミューテーションを登録（ミューテーションは同期でなくてはだめ）
    increment: (state) => state.count++,
    decrement: (state) => state.count--
  }
}
