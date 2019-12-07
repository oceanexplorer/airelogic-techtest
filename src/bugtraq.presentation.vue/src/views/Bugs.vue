<template>
  <div class="home">
    
    <bugs-grid>
      <template v-slot:actions>
        <add-bug />
      </template>
    </bugs-grid>
  </div>
</template>

<script>
  import BugsGrid from "../components/BugsGrid.vue";
  import AddBug from "../components/AddBug.vue"
  
  export default {
      name: 'home',
      components: {
          BugsGrid,
          AddBug
      }
  }
</script>
