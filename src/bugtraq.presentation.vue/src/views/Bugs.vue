<template>
  <div class="home">
    
    <bugs-grid>
      <template v-slot:actions>
        <add-bug-modal/>
      </template>
    </bugs-grid>
  </div>
</template>

<script>
  import BugsGrid from "../components/BugsGrid.vue";
  import AddBugModal from "../components/AddBugModal.vue"
  
  export default {
      name: 'home',
      components: {
          BugsGrid,
          AddBugModal
      }
  }
</script>
