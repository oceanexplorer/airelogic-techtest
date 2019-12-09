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
        <b-table striped hover :items="users" :fields="fields" class="mt-4"></b-table>        
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