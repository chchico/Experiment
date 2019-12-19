<style>
.yellow {
  color: #f0f;
}
</style>

<template>
  <div class="row">
    <div class="col-md-12">
      <button v-on:click="getData">Get milk!!</button>
      <table class="table table-striped">
        <thead>
          <tr>
            <th>製品名</th>
            <th>種別</th>
            <th>価格</th>
            <th>操作</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in items" v-bind:key="item.name">
            <td>{{ item.name }}</td>
            <td>{{ item.typeCd | typeFormatter }}</td>
            <td>{{ item.price }}</td>
            <td><a v-bind:href="item.url">Edit</a></td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  filters: {
    typeFormatter: function(val) {
      var typeCds = [
        { id: '1', name: '新品' },
        { id: '2', name: '中古' }
      ]
      var newVal = ''

      typeCds.forEach(function(el) {
        if (val.toString() === el.id) {
          newVal = el.name
        }
      })

      return newVal
    }
  },
  data() {
    return {
      items: []
    }
  },
  methods: {
    getData: function() {
      // ログイン直後は持ってないので取りに行く必要がある
      var token = this.$store.state.auth.token

      var options = {
        headers: {
          Authorization: `Bearer ${token}`
        }
      }

      axios.get('/api/Items/', options).then((response) => {
        this.items = response.data
      })
    }
  }
}
</script>
