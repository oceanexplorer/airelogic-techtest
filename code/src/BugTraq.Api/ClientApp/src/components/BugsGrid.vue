<template>
    <div>
        <div class="row mb-3">
            <div class="col-6">
                <b-button-group>
                    <b-button @click="displayMethod = 'cards'">
                        <font-awesome-icon icon="address-card"/>
                    </b-button>
                    <b-button @click="displayMethod = 'table'">
                        <font-awesome-icon icon="table"/>
                    </b-button>
                </b-button-group>
            </div>
            <div class="col-6">
                <add-bug-modal class="float-right pl-2"></add-bug-modal>
            </div>
        </div>
        <div class="row" v-if="displayMethod === 'cards'">
            <div class="col" v-bind:key="status" v-for="(tickets, status) in statuses">
                <b-card class="h-100">
                    <template v-slot:header>
                        <h6 class="mb-0">{{ status | capitalize }}</h6>
                    </template>
                    <b-card-text>
                        <div class="d-flex justify-content-center mb-3" v-if="loading === true">
                            <b-spinner label="Loading..."></b-spinner>
                        </div>
                        <draggable
                                :list="statuses[status]"
                                @change="onChange($event, status)"
                                class=dragArea
                                v-bind="dragOptions"
                        >
                            <b-card class="mb-2 draggable draggable-card" v-bind:key="ticket.bugId"
                                    v-for="ticket in statuses[status]">
                                <h6 class="text-left">{{ ticket.title }}</h6>
                                <b-card-text>
                                    <div class="row">
                                        <div class="col text-left">
                                            <font-awesome-icon class="text-primary" icon="user"/>
                                            <span class="pl-3">{{ ticket.userFirstName }} {{ ticket.userSurname }}</span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col text-right">
                                            <a @click="showEditBugModal(ticket.bugId)" class="cursor-pointer pr-2"
                                               href="#">
                                                <font-awesome-icon class="text-primary cursor-pointer" icon="edit"/>
                                            </a>
                                            <a @click="showDeleteConfirmationModal(ticket.bugId)" class="cursor-pointer"
                                               href="#">
                                                <font-awesome-icon class="text-danger cursor-pointer" icon="trash"/>
                                            </a>
                                        </div>
                                    </div>
                                </b-card-text>
                            </b-card>
                        </draggable>
                    </b-card-text>
                </b-card>
            </div>
        </div>
        <div class="row" v-if="displayMethod === 'table'">
            <div class="col">
                <b-table :items="combineTickets" :fields="fields"></b-table>
            </div>
        </div>
        <edit-bug-modal :bugId="idForEdit" :show-button="false" class="float-right"></edit-bug-modal>
    </div>
</template>

<script>
    import draggable from "vuedraggable";
    import editBugModal from "./EditBugModal";
    import AddBugModal from "./AddBugModal";

    export default {
        name: "grid",
        components: {
            AddBugModal,
            draggable,
            editBugModal
        },
        computed: {
            dragOptions() {
                return {
                    animation: 150,
                    group: "description"
                };
            },
            combineTickets(){
                let tickets = [];
                let self = this;
                Object.entries(this.statuses).forEach(entry => {
                    let status = entry[0];
                    tickets = tickets.concat(self.statuses[status]);
                });
                return tickets;
            }
        },
        data: function () {
            return {
                displayMethod: "cards",
                statuses: {
                    new: [],
                    active: [],
                    resolved: [],
                    closed: []
                },
                isDragging: false,
                loading: true,
                info: null,
                tickets: {},
                ticketsByStatus: {},
                deleteConfirmation: {
                    result: null
                },
                idForEdit: null,
                fields: [
                    {
                        key: 'title',
                        sortable: true
                    },
                    {
                        key: 'status',
                        sortable: true
                    },
                    {
                        key: 'userFirstName',
                        label: 'First Name',
                        sortable: true
                    },
                    {
                        key: 'userSurname',
                        label: 'Surname',
                        sortable: true
                    }
                ],
            };
        },
        filters: {
            capitalize: function (value) {
                if (!value) return "";
                value = value.toString();
                return value.charAt(0).toUpperCase() + value.slice(1);
            }
        },
        methods: {
            onChange(event, newStatus) {
                if (event.added) {
                    this.updateTicket(event.added.element.bugId, newStatus);
                }
            },
            filterByProperty(collection, propertyName, propertyValue) {
                return this._.filter(collection, (item) => {
                    return item[propertyName].toLowerCase() === propertyValue.toLowerCase()
                });
            },
            getTickets() {
                this.axios
                    .get('http://localhost:5000/api/bugs')
                    .then(response => this.loadTickets(response.data))
                    .finally(() => this.loading = false);
            },
            loadTickets(tickets) {
                Object.entries(this.statuses).forEach(entry => {
                    let status = entry[0];
                    this.statuses[status] = this.filterByProperty(tickets, 'status', status);
                });
            },
            showDeleteConfirmationModal(id) {
                this.$bvModal.msgBoxConfirm("Are you sure you want to delete this bug?")
                    .then(okToDelete => {
                        if (okToDelete) {
                            this.deleteTicket(id);
                        }
                    });

            },
            showEditBugModal(id) {
                this.idForEdit = id;
                this.$bvModal.show("editBugModal");
            },
            updateTicket(id, status) {
                this.axios
                    .put(`http://localhost:5000/api/bugs/updatestatus/${id}/${status}`)
            },
            deleteTicket(id) {
                this.axios
                    .delete(`http://localhost:5000/api/bugs/${id}`)
                    .then(this.getTickets);
            }
        },
        mounted() {

            this.$root.$on('bug-added', () => {
                this.getTickets();
            });
            this.$root.$on('bug-updated', () => {
                this.getTickets();
            });

            this.getTickets();

        }
    };
</script>

<style>
    .draggable {
        cursor: move;
    }

    .dragArea {
        min-height: 15rem;
    }

    .draggable-card {
        border-left: 5px solid blue;
    }
</style>