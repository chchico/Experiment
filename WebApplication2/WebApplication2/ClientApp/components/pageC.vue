<style>
.yellow {
  color: #f0f;
}
</style>

<template>
  <div class="container">
    <h1 class="yellow">page C</h1>
    <div>
      <button v-on:click="clickPlus">プラス</button>
      <button v-on:click="clickMinus">マイナス</button>
      <p>{{ countNum }}</p>
    </div>
    <div>
      <router-link to="/">戻る</router-link>
    </div>
  </div>
</template>

<script>
export default {
  computed: {
    countNum() {
      return this.$store.getters.count
    }
  },
  methods: {
    clickPlus() {
      // ミューテーションにある increment メソッドを呼び出す（同期）
      this.$store.commit('counter/increment')
    },
    clickMinus() {
      this.$store.commit('counter/decrement')
    }
  }
}
</script>
