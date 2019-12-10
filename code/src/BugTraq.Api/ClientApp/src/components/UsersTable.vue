<template>
    <div>
        <div class="row">
            <div class="col">
                <h2>Users</h2>
            </div>
        </div>

        <div class="row">
            <div class="col">
                <add-user-modal class="float-right"/>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <b-table :fields="fields" :items="users" class="mt-4" hover striped></b-table>
            </div>
        </div>
    </div>
</template>

<script>
    import AddUserModal from "./AddUserModal";

    export default {
        name: "UsersTable",
        components: {
            AddUserModal
        },
        data() {
            return {
                fields: ['firstName', 'surname'],
                users: []
            }
        },
        mounted() {
            this.$root.$on('user-added', () => {
                this.loadUsers();
            });
            this.loadUsers();
        },
        methods: {
            loadUsers() {
                let self = this;
                this.axios
                    .get('http://localhost:5000/api/users')
                    .then(function (result) {
                        self.users = result.data;
                    })
            }
        }

    }
</script>