<template>
    <div>
        <b-button v-b-modal.addUserModal>Add User</b-button>

        <b-modal hide-footer id="addUserModal" size="lg" title="Add A User" v-on:hidden="modalHidden">
            <div>
                <b-alert dismissible v-model="success" variant="success">
                    User has been added successfully
                </b-alert>
                <b-alert dismissible v-model="errored" variant="danger">
                    Sorry an error has occured
                </b-alert>
                <b-form @submit="onSubmit" class="form-horizontal" v-if="show">
                    <b-form-group
                            id="firstName-group"
                            label="First Name"
                            label-cols="3"
                            label-for="firstName"
                    >
                        <b-form-input
                                id="firstName"
                                required
                                type="text"
                                v-model="form.firstName"
                        ></b-form-input>
                    </b-form-group>

                    <b-form-group
                            id="surname-group"
                            label="Surname"
                            label-cols="3"
                            label-for="surname"
                    >
                        <b-form-input
                                id="surname"
                                required
                                type="text"
                                v-model="form.surname"
                        ></b-form-input>
                    </b-form-group>
                    <div class="float-right">
                        <b-button @click.prevent="close" class="mr-2" type="reset" variant="danger">Close</b-button>
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
                    .then(function () {
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
            close() {
                this.$bvModal.hide("addUserModal");
            }
        }
    }
</script>