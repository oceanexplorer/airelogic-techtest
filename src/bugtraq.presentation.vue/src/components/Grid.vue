<template>
  <div>
    <div class="row mb-3 pl-3">
      <b-button-group>
        <b-button @click="displayMethod = 'cards'">Cards</b-button>
        <b-button @click="displayMethod = 'table'">Table</b-button>
      </b-button-group>
    </div>
    <div class="row" v-if="displayMethod === 'cards'">
      <div
        class="col-sm"
        v-for="(value, name, index) in groupedByProperty('status')"
        v-bind:key="index"
      >
        <b-card>
          <template v-slot:header>
            <h6 class="mb-0">{{ name | capitalize }}</h6>
          </template>
          <b-card-text>
            <b-card class="mb-2" v-for="entry in value" v-bind:key="entry.id">
              <h5>{{ entry.title }}</h5>
              <b-card-text>{{ entry.title }}</b-card-text>
            </b-card>
          </b-card-text>
        </b-card>
      </div>
    </div>
    <div class="row" v-if="displayMethod === 'table'">
      <div class="col">
        <b-table :items="gridData"></b-table>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "grid",
  data: function() {
    return {
      displayMethod: "cards",
      gridData: [
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
        },
        {
          id: 3,
          title: "Unable to view users",
          date: "11/01/2019",
          status: "active"
        },
        {
          id: 4,
          title: "Unable to view food",
          date: "11/01/2019",
          status: "resolved"
        },
        {
          id: 5,
          title: "Another Bug",
          date: "11/01/2019",
          status: "closed"
        }
      ]
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