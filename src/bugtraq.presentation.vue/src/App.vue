<template>
  <div id="app">
    <the-navbar />
    <div class="container-fluid mt-5">
      <bugs-grid />
      <add-bug />
    </div>
  </div>
</template>

<script>
import TheNavbar from './components/TheNavbar.vue'
import BugsGrid from './components/BugsGrid.vue'
import AddBug from './components/AddBug.vue'

export default {
  name: 'app',
  components: {
    TheNavbar,
    BugsGrid,
    AddBug
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
