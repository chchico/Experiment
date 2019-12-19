<template>
  <div id="app">
    <p>{{ msg }}</p>
    <router-link to="/login">Login</router-link>
    <router-link to="/pageA">PageA</router-link>
  </div>
</template>

<script>
export default {
  name: 'Top',
  data() {
    return {
      msg: 'Welcome to Your Vue.js App'
    }
  }
}
</script>
