<template>
    <div class="home">
        <bugs-grid/>
    </div>
</template>

<script>
    import BugsGrid from "../components/BugsGrid.vue";

    export default {
        name: 'home',
        components: {
            BugsGrid
        }
    }
</script>
