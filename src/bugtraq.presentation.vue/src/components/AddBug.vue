<template>
  <div>
    <b-button v-b-modal.modal-1>Add Bug</b-button>

    <b-modal id="modal-1" title="Add A Bug" size="lg" busy="true">
      <div>
        <b-alert v-model="errored" variant="danger" dismissible>
          Sorry an error has occured
        </b-alert>
        <b-form @submit="onSubmit" @reset="onReset" v-if="show" class="form-horizontal">
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
            <b-form-select
              id="user"
              v-model="form.userId"
              :options="users"
              required
            ></b-form-select>
          </b-form-group>

          <b-button type="submit" variant="primary">Submit</b-button>
          <b-button type="reset" variant="danger">Reset</b-button>
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
            title: '',
            description: '',
            status: null,
            userId: null
          },
          statuses: [{ text: 'Select One', value: null }, 'New', 'Active', 'Resolved', 'Closed'],
          show: true,
          users: [{ text: 'Select One', value: null }, { text: 'Sarah Smith', value: 1 }],
          errored: false
        }
    },
    methods: {
      onSubmit(evt) {
        evt.preventDefault();
        let self = this;
        this.axios
          .post('http://localhost:5000/api/bugs', this.form)
          .catch(function () {
            self.errored = true;
          });
      },
      onReset(evt) {
        evt.preventDefault();
        
        // Reset our form values
        this.form.title = '';
        this.form.description = '';
        this.form.status = null;
        this.form.userId = [];
        
        // Trick to reset/clear native browser form validation state
        this.show = false;
        this.$nextTick(() => {
            this.show = true
        })
      }
    }
  }
</script>