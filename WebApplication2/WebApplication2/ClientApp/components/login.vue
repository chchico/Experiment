<template>
  <div id="app">
    <section class="hero is-light is-fullheight">
      <div class="hero-body">
        <div class="container has-text-centered">
          <div class="column is-4 is-offset-4">
            <div class="box">
              <div class="field">
                <div class="control">
                  <input class="input is-large" type="email" placeholder="Eメール" v-model="email" autofocus="" name="email" />
                </div>
              </div>
              <div class="field">
                <div class="control">
                  <input class="input is-large" type="password" placeholder="パスワード" v-model="password" maxlength="20" name="password" />
                </div>
              </div>
              <div class="field">
                <label class="checkbox"><input type="checkbox" />ログインしたままにする</label>
              </div>
              <button class="button is-block is-info is-large is-fullwidth" @click="login()">ログイン</button>
            </div>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script>
export default {
  name: 'Login',
  data() {
    return {
      email: '',
      password: ''
    }
  },
  methods: {
    async login() {
      await this.$store.dispatch('auth/login', {
        email: this.email,
        password: this.password
      })

      this.$router.push('/')
    }
  }
}
</script>
