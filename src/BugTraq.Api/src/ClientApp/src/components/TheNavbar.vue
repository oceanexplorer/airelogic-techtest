<template>
    <b-navbar class="mb-3" toggleable="lg" type="dark" variant="info">
        <b-navbar-brand href="#">
            <font-awesome-icon icon="bug"/>
            <span class="pl-2">BugTraq</span>
        </b-navbar-brand>
        <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>
        <b-collapse id="nav-collapse" is-nav>
            <b-navbar-nav>
                <b-nav-item href="#" to="/">Bugs</b-nav-item>
                <b-nav-item href="#" to="/users">Users</b-nav-item>
            </b-navbar-nav>
        </b-collapse>
    </b-navbar>
</template>

<script>
    export default {
        name: 'TheNavbar'
    }
</script>