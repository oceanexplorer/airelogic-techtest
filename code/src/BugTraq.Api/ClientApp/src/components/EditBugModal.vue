<template>
    <div>
        <b-button v-b-modal.editBugModal v-if="showButton">Edit Bug</b-button>

        <b-modal @change="onChange" hide-footer id="editBugModal" size="lg" title="Edit Bug" v-on:hidden="modalHidden">
            <div class="content">
                <div class="d-flex justify-content-center mb-3" v-if="isLoading">
                    <b-spinner label="Loading..."></b-spinner>
                </div>
                <div v-if="!isLoading">
                    <b-alert dismissible v-model="success" variant="success">
                        Bug has been updated successfully
                    </b-alert>
                    <b-alert dismissible v-model="errored" variant="danger">
                        Sorry an error has occured
                    </b-alert>
                    <b-form @submit="onSubmit" class="form-horizontal" v-if="show">
                        <b-form-group
                                id="title-group"
                                label="Title"
                                label-cols="3"
                                label-for="title"
                        >
                            <b-form-input
                                    id="title"
                                    required
                                    type="text"
                                    v-model="form.title"
                            ></b-form-input>
                        </b-form-group>

                        <b-form-group
                                id="description-group"
                                label="Description"
                                label-cols="3"
                                label-for="description"
                        >
                            <b-form-input
                                    id="description"
                                    required
                                    type="text"
                                    v-model="form.description"
                            ></b-form-input>
                        </b-form-group>

                        <b-form-group
                                id="status-group"
                                label="Status"
                                label-cols="3"
                                label-for="status"
                        >
                            <b-form-select
                                    :options="statuses"
                                    id="status"
                                    required
                                    v-model="form.status"
                            ></b-form-select>
                        </b-form-group>

                        <b-form-group
                                id="user-group"
                                label="Users"
                                label-cols="3"
                                label-for="user"
                        >
                            <users-dropdown v-model="form.userId"></users-dropdown>
                        </b-form-group>
                        <div class="float-right">
                            <b-button @click.prevent="close" class="mr-2" type="reset" variant="danger">Close</b-button>
                            <b-button type="submit" variant="primary">
                                <span>Submit</span>
                                <b-spinner class="ml-2" small v-if="isSubmitting"></b-spinner>
                            </b-button>
                        </div>
                    </b-form>
                </div>
            </div>
        </b-modal>
    </div>
</template>

<script>
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
                statuses: [
                    {text: 'Select One...', value: null},
                    {text: 'New', value: 'new'},
                    {text: 'Active', value: 'active'},
                    {text: 'Resolved', value: 'resolved'},
                    {text: 'Closed', value: 'closed'}
                ],
                show: true,
                users: [{text: 'Select One...', value: null}],
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
                    .then(function () {
                        self.$bvModal.hide("editBugModal");
                        self.reset();
                        self.$root.$emit("bug-updated");
                    })
                    .catch(function () {
                        self.errored = true;
                    })
                    .finally(function () {
                        self.isSubmitting = false;
                    })
            },
            onChange() {
                let self = this;
                self.isLoading = true;
                this.axios
                    .get(`http://localhost:5000/api/bugs/${this.bugId}`)
                    .then(function (result) {
                        let ticket = result.data;
                        self.form.bugId = self.bugId;
                        self.form.title = ticket.title;
                        self.form.description = ticket.description;
                        self.form.status = ticket.status;
                        self.form.userId = ticket.userId;

                        console.log(ticket);
                    })
                    .finally(function () {
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
            close() {
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