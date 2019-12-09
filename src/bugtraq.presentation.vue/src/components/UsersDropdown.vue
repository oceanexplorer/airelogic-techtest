<template>
  <b-form-select
          id="user"
          :options="users"
          required
          v-model="selectedId"
  ></b-form-select>
</template>

<script>
    /* eslint-disable no-console */
    /* eslint-disable no-unused-vars */
    /* eslint-disable no-debugger */
    export default {
        data() {
            return {
                users: [{text: 'Select One...', value: null}],
                selectedId: this.value
            }
        },
        mounted() {
            this.loadUsers();
        },
        watch: {
            selectedId(updatedValue) {
                this.$emit('input', updatedValue);
            },
            value(updatedValue) {                
                this.selectedId = updatedValue;
            },
        },
        props: ['value'],
        methods: {
            loadUsers() {
                let self = this;
                this.axios
                    .get('http://localhost:5000/api/users')
                    .then(function(result){
                        result.data.forEach(user => {
                            let userOption = self.convertUserToOption(user);
                            self.users.push(userOption);
                        });
                    })
            },
            convertUserToOption(user) {
                return{ text: `${ user.firstName } ${ user.surname }`, value: user.userId};
            }            
        }        
    }
</script>