<template>
    <div id="app">
        <TheNavbar/>
        <div class="container-fluid">
            <router-view/>
        </div>
    </div>
</template>

<script>
    import TheNavbar from './components/TheNavbar'

    export default {
        name: 'app',
        components: {
            TheNavbar
        }
    }
</script>
