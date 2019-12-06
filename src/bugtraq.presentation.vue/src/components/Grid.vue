<template>
  <div>
    <div class="row mb-3 pl-3">
      <b-button-group>
        <b-button @click="displayMethod = 'cards'">Cards</b-button>
        <b-button @click="displayMethod = 'table'">Table</b-button>
      </b-button-group>
    </div>
    <div class="row" v-if="displayMethod === 'cards'">
      <div class="col" v-for="(tickets, status) in statuses" v-bind:key="status">
        <b-card class="h-100">
          <template v-slot:header>
            <h6 class="mb-0">{{ status | capitalize }}</h6>
          </template>
          <b-card-text>
            <div v-if="loading === true" class="d-flex justify-content-center mb-3">
              <b-spinner label="Loading..."></b-spinner>
            </div>
              <draggable 
                class=dragArea
                :list="statuses[status]" 
                v-bind="dragOptions" 
                @change="onChange($event, status)"
              >
                  <b-card class="mb-2 draggable draggable-card" v-for="ticket in statuses[status]" v-bind:key="ticket.bugId">
                    <h6 class="text-left">{{ ticket.title }}</h6>
                    <b-card-text>
                      <div class="row">
                        <div class="col text-left">
                          <font-awesome-icon icon="user" class="text-primary" />
                          <span class="pl-3">{{ ticket.userFirstName }} {{ ticket.userSurname }}</span>
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
        <b-table :items="tickets"></b-table>
      </div>
    </div>
  </div>  
</template>

<script>
/* eslint-disable no-console */
/* eslint-disable no-unused-vars */
/* eslint-disable no-debugger */
import draggable from "vuedraggable";

export default {
  name: "grid",
  components: {
    draggable
  },
  computed: {
    dragOptions() {
      return {
        animation: 150,
        group: "description"
      };
    }
  },
  data: function() {
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
      ticketsByStatus: {}
    };
  },
  filters: {
    capitalize: function(value) {
      if (!value) return "";
      value = value.toString();
      return value.charAt(0).toUpperCase() + value.slice(1);
    }
  },
  methods: {
    onChange (event, newStatus) {
      if(event.added) {
        this.updateTicket(event.added.element.bugId, newStatus);
      }
    },
    filterByProperty(collection, propertyName, propertyValue) {
      return this._.filter(collection, (item) =>  { return item[propertyName].toLowerCase() === propertyValue.toLowerCase() });
    },
    loadTickets (tickets) {
      Object.entries(this.statuses).forEach(entry => {
        let status = entry[0];
        this.statuses[status] = this.filterByProperty(tickets, 'status', status);
      });
    },
    updateTicket (id, status) {
      this.axios
        .put(`http://localhost:5000/api/bugs/updatestatus/${id}/${status}`)
    }
  },
  mounted () {
    this.axios
      .get('http://localhost:5000/api/bugs')
      .then(response => this.loadTickets(response.data))
      .finally(() => this.loading = false);
  }
};
</script>

<style>
  .draggable { 
    cursor: pointer;
  }

  .dragArea {
    min-height: 15rem;
  }
  
  .draggable-card {
    border-left: 5px solid blue;
  }
</style>