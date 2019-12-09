<template>
  <div>
    <b-button v-b-modal.editBugModal v-if="showButton">Edit Bug</b-button>

    <b-modal id="editBugModal" title="Edit Bug" size="lg" hide-footer v-on:hidden="modalHidden" @change="onChange">
      <div class="content">
        <div class="d-flex justify-content-center mb-3" v-if="isLoading">
          <b-spinner label="Loading..."></b-spinner>
        </div>
        <div v-if="!isLoading">
          <b-alert v-model="success" variant="success" dismissible>
            Bug has been updated successfully
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
              <b-button type="submit" variant="primary">              
                <span>Submit</span>
                <b-spinner small class="ml-2" v-if="isSubmitting"></b-spinner>
              </b-button>
            </div>
          </b-form>
        </div>
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
        props: {
          showButton: Boolean,
          bugId: Number
        },       
        data() {
            return {
                form: {
                    bugId: null,
                    title: null,
                    description: null,
                    status: null,
                    userId: null
                },
                statuses: [{ text: 'Select One...', value: null }, 'New', 'Active', 'Resolved', 'Closed'],
                show: true,
                users: [{ text: 'Select One...', value: null }],
                errored: false,
                success: false,
                isLoading: false,
                isSubmitting: false
            }
        },
        methods: {
            onSubmit(evt) {
                evt.preventDefault();
                let self = this; 
                self.isSubmitting = true;
                this.axios
                    .put(`http://localhost:5000/api/bugs/${this.bugId}`, this.form)
                    .then(function() {
                        self.$bvModal.hide("editBugModal");
                        self.reset();
                        self.$root.$emit("bug-updated");
                    })
                    .catch(function () {
                        self.errored = true;
                    })                    
                    .finally(function(){
                        self.isSubmitting = false;
                    })
            },
            onChange() {
                let self = this;
                self.isLoading = true;
                this.axios
                    .get(`http://localhost:5000/api/bugs/${this.bugId}`)
                    .then(function(result){
                        let ticket = result.data;
                        self.form.bugId = self.bugId;
                        self.form.title = ticket.title;
                        self.form.description = ticket.description;
                        self.form.status = ticket.status;
                        self.form.userId = ticket.userId;
                    })
                    .finally(function(){
                        self.isLoading = false;
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
                this.$bvModal.hide("editBugModal");
            }
        }
    }
</script>

<style>
  #editBugModal .content {
    min-height: 260px;
  }
</style>