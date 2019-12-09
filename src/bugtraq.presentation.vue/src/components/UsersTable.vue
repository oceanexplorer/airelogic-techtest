<template>
  <div>
    <h2>Users</h2>
    <div>
      <b-table striped hover :items="users" :fields="fields" class="mt-4"></b-table>
    </div>
  </div>
</template>

<script>
    export default {
        name: "UsersTable",
        data () {
            return {
                fields: ['firstName', 'surname'],
                users: []
            }
        },
        mounted(){
            let self = this;
            this.axios
                .get('http://localhost:5000/api/users')
                .then(function (result){
                    self.users = result.data;
                })
        }
    }
</script>

<style scoped>

</style>