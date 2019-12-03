<template>
  <div>
    <div class="row mb-3 pl-3">
      <b-button-group>
        <b-button @click="displayMethod = 'cards'">Cards</b-button>
        <b-button @click="displayMethod = 'table'">Table</b-button>
      </b-button-group>
    </div>
    <div class="row" v-if="displayMethod === 'cards'">
      <div class="col" v-for="status in statuses" v-bind:key="status">
        <b-card>
          <template v-slot:header>
            <h6 class="mb-0">{{ status | capitalize }}</h6>
          </template>
          <b-card-text>
            <draggable v-bind="dragOptions">
              <transition-group type="transition" :name=status>
                <b-card class="mb-2 draggable" v-for="ticket in tickets[status]" v-bind:key="ticket.id">
                  <h5>{{ ticket.title }}</h5>
                  <b-card-text>{{ ticket.title }}</b-card-text>
                </b-card>
              </transition-group>
            </draggable>
          </b-card-text>
        </b-card>
      </div>
    </div>
  </div>  
</template>

<script>

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
      statuses: ['new', 'active', 'resolved', 'closed'],
      isDragging: false,
      tickets: {
        new: [
          {
            id: 1,
            title: "Change the title on the homepage",
            date: "01/01/2019",
            status: "new"
          },
          {
            id: 2,
            title: "Uploading data causes an error",
            date: "15/01/2019",
            status: "new"
          }
        ],
        active: [
          {
            id: 3,
            title: "Unable to view users",
            date: "11/01/2019",
            status: "active"
          }
        ],
        resolved: [
          {
            id: 4,
            title: "Unable to view food",
            date: "11/01/2019",
            status: "resolved"
          }
        ],
        closed: [
          {
            id: 5,
            title: "Another Bug",
            date: "11/01/2019",
            status: "closed"
          }
        ]
      }
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
    groupedByProperty: function(propertyName) {
      return this._.groupBy(this.gridData, propertyName);
    }
  }
};
</script>

<style>
  .draggable { cursor: pointer}
</style>