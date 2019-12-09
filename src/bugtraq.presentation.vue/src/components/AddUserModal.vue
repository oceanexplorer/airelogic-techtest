<template>
  <div>
    <b-button v-b-modal.addUserModal>Add User</b-button>

    <b-modal id="addUserModal" title="Add A User" size="lg" hide-footer v-on:hidden="modalHidden">
      <div>
        <b-alert v-model="success" variant="success" dismissible>
          User has been added successfully
        </b-alert>
        <b-alert v-model="errored" variant="danger" dismissible>
          Sorry an error has occured
        </b-alert>
        <b-form @submit="onSubmit" v-if="show" class="form-horizontal">
          <b-form-group
                  id="firstName-group"
                  label="First Name"
                  label-for="firstName"
                  label-cols="3"
          >
            <b-form-input
                    id="firstName"
                    v-model="form.firstName"
                    type="text"
                    required
            ></b-form-input>
          </b-form-group>

          <b-form-group
                  id="surname-group"
                  label="Surname"
                  label-for="surname"
                  label-cols="3"
          >
            <b-form-input
                    id="surname"
                    v-model="form.surname"
                    type="text"
                    required
            ></b-form-input>
          </b-form-group>
          <div class="float-right">
            <b-button type="reset" variant="danger" class="mr-2" @click.prevent="close">Close</b-button>
            <b-button type="submit" variant="primary">Submit</b-button>
          </div>
        </b-form>
      </div>
    </b-modal>
  </div>
</template>

<script>
    export default {
        data() {
            return {
                form: {
                    firstName: '',
                    surname: ''
                },               
                show: true,
                errored: false,
                success: false
            }
        },
        methods: {
            onSubmit(evt) {
                evt.preventDefault();
                let self = this;
                this.axios
                    .post('http://localhost:5000/api/users', this.form)
                    .then(function() {
                        self.success = true;
                        self.reset();
                        self.$root.$emit("user-added");
                    })
                    .catch(function () {
                        self.errored = true;
                    })
            },
            modalHidden() {
                this.reset();
                this.resetAlerts();
            },
            reset() {
                // Reset our form values
                this.form.firstName = '';
                this.form.surname = '';

                // Trick to reset/clear native browser form validation state
                this.show = false;
                this.$nextTick(() => {
                    this.show = true
                });
            },
            resetAlerts() {
                this.errored = false;
                this.success = false;
            },
            close () {
                this.$bvModal.hide("addUserModal");
            }
        }
    }
</script>