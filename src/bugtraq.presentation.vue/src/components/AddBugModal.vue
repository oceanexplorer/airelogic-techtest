<template>
  <div>
    <b-button v-b-modal.addBugModal>Add Bug</b-button>

    <b-modal id="addBugModal" title="Add A Bug" size="lg" hide-footer v-on:hidden="modalHidden">
      <div>
        <b-alert v-model="success" variant="success" dismissible>
          Bug has been added successfully
        </b-alert>
        <b-alert v-model="errored" variant="danger" dismissible>
          Sorry an error has occured
        </b-alert>
        <b-form @submit="onSubmit" v-if="show" class="form-horizontal">
          <b-form-group
            id="title-group"
            label="Title"
            label-for="title"
            label-cols="3"
          >
            <b-form-input
              id="title"
              v-model="form.title"
              type="text"
              required
            ></b-form-input>
          </b-form-group>

          <b-form-group
            id="description-group"
            label="Description"
            label-for="description"
            label-cols="3"
          >
            <b-form-input
              id="description"
              v-model="form.description"
              type="text"
              required
            ></b-form-input>
          </b-form-group>

          <b-form-group 
            id="status-group" 
            label="Status" 
            label-for="status"
            label-cols="3"
          >
            <b-form-select
              id="status"
              v-model="form.status"
              :options="statuses"
              required
            ></b-form-select>
          </b-form-group>

          <b-form-group
            id="user-group"
            label="Users"
            label-for="user"
            label-cols="3"
          >
            <users-dropdown v-model="form.userId"></users-dropdown>
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
    /* eslint-disable no-console */
    /* eslint-disable no-unused-vars */
    /* eslint-disable no-debugger */
    import usersDropdown from "./UsersDropdown";
    
    export default {
      components: {
          usersDropdown
      },
      data() {
        return {
          form: {
            title: '',
            description: '',
            status: null,
            userId: null
          },
          statuses: [{ text: 'Select One...', value: null }, 'New', 'Active', 'Resolved', 'Closed'],
          show: true,
          users: [{ text: 'Select One...', value: null }],
          errored: false,
          success: false
        }
    },
    methods: {
      onSubmit(evt) {
        evt.preventDefault();
        let self = this;
        this.axios
          .post('http://localhost:5000/api/bugs', this.form)
          .then(function() {
              self.success = true;
              self.reset();
              self.$root.$emit("bug-added");
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
        this.form.title = '';
        this.form.description = '';
        this.form.status = null;
        this.form.userId = null;
        
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
        this.$bvModal.hide("addBugModal");        
      }      
    }
  }
</script>