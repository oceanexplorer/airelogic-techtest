<template>
  <div id="app">
    <navigation />
    <div class="container-fluid mt-5">
      <grid />
    </div>
  </div>
</template>

<script>
import navigation from './components/Navigation.vue'
import grid from './components/Grid.vue'

export default {
  name: 'app',
  components: {
    navigation,
    grid
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
