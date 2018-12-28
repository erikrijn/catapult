<template>
  <div>
    <h1>Containers</h1>

    <div v-if="!containers" class="text-center">
      <p><em>Loading...</em></p>
      <h1><icon icon="spinner" pulse /></h1>
    </div>

    <template v-if="containers">
      <table class="table">
        <thead class="bg-dark text-white">
          <tr>
            <th>Name</th>
            <th>Image</th>
            <th>IP Address</th>
            <th>State</th>
            <th>Status</th>
            <th>Created</th>
            <th></th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr :class="index % 2 == 0 ? 'bg-white' : 'bg-light'" v-for="(container, index) in containers" :key="index">
            <td v-if="container.state == 'running'"><a v-bind:href="container.url" target="_blank">{{ container.names[0].replace('/','') }}</a></td>
            <td v-else>{{ container.names[0].replace('/','') }}</td>
            <td>{{ container.image }}</td>
            <td v-if="container.networkSettings.networks.transparentNet">{{ container.networkSettings.networks.transparentNet.ipAddress }}</td>
            <td v-else></td>
            <td>{{ container.state }}</td>
            <td>{{ container.status }}</td>
            <td>{{ container.createdFormatted }}</td>
            <td v-if="container.state == 'exited'"><button v-on:click="startContainer(container.id)" class="btn-success">Start</button></td>
            <td v-else><button v-on:click="stopContainer(container.id)" class="btn-warning">Stop</button></td>
            <td v-if="container.state == 'exited'"><button v-on:click="removeContainer(container.id)" class="btn-danger">Remove</button></td>
            <td v-else><button disabled class="btn-light">Remove</button></td>
          </tr>
        </tbody>
      </table>
      <nav aria-label="..." v-if="totalPages > 1">
        <ul class="pagination justify-content-center">
          <li :class="'page-item' + (currentPage == 1 ? ' disabled' : '')">
            <a class="page-link" href="#" tabindex="-1" @click="loadPage(currentPage - 1)">Previous</a>
          </li>
          <li :class="'page-item' + (n == currentPage ? ' active' : '')" v-for="(n, index) in totalPages" :key="index">
            <a class="page-link" href="#" @click="loadPage(n)">{{n}}</a>
          </li>
          <li :class="'page-item' + (currentPage < totalPages ? '' : ' disabled')">
            <a class="page-link" href="#" @click="loadPage(currentPage + 1)">Next</a>
          </li>
        </ul>
      </nav>
      <div>
        <button class="btn-primary" v-on:click="createContainer('new')">New Container</button>
      </div>
    </template>
  </div>
</template>

<script>
  export default {
    computed: {
      totalPages: function () {
        return Math.ceil(this.total / this.pageSize)
      }
    },

    data() {
      return {
        containers: null,
        total: 0,
        pageSize: 10,
        currentPage: 1
      }
    },

    methods: {
      async loadPage(page) {
        this.currentPage = page

        try {
          var from = (page - 1) * (this.pageSize)
          var to = from + this.pageSize
          let response = await this.$http.get(`/api/container/containers?from=${from}&to=${to}`)
          console.log(response.data.containers)
          this.containers = response.data.containers
          this.total = response.data.total
        } catch (err) {
          window.alert(err)
          console.log(err)
        }
      },

      async createContainer(name) {
        try {
          this.containers = null;
          await this.$http.post(`/api/container/create?image=mcr.microsoft.com/businesscentral/onprem:ltsc2019&name=${name}`);
          await this.loadPage(1);
        } catch (err) {
          window.alert(err)
          console.log(err)
        }
      },

      async startContainer(id) {
        try {
          this.containers = null;
          await this.$http.post(`/api/container/start?id=${id}`);
          await this.loadPage(1);
        } catch (err) {
          window.alert(err)
          console.log(err)
        }
      },

      async stopContainer(id) {
        try {
          this.containers = null;
          await this.$http.post(`/api/container/stop?id=${id}`);
          await this.loadPage(1);
        } catch (err) {
          window.alert(err)
          console.log(err)
        }
      },

      async removeContainer(id) {
        try {
          this.containers = null;
          await this.$http.delete(`/api/container/remove?id=${id}`);
          await this.loadPage(1);
        } catch (err) {
          window.alert(err)
          console.log(err)
        }
      }
    },

    async created() {
      this.loadPage(1)
    }
  }
</script>

<style>
</style>
