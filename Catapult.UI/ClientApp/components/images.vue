<template>
  <div>
    <h1>All images</h1>

    <div v-if="!images" class="text-center">
      <p><em>Loading...</em></p>
      <h1><icon icon="spinner" pulse /></h1>
    </div>

    <template v-if="images">
      <table class="table">
        <thead class="bg-dark text-white">
          <tr>
            <th>Name</th>
            <th>Country</th>
            <th>OS Version</th>
            <th>Platform Version</th>
            <th>Size (MB)</th>
            <th>Created</th>
            <th></th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr :class="index % 2 == 0 ? 'bg-white' : 'bg-light'" v-for="(image, index) in images" :key="index">
            <td>{{ image.repoTags[0] }}</td>
            <td v-if="image.labels == null">Unknown</td>
            <td v-else>{{ image.labels.country }}</td>
            <td v-if="image.labels == null">Unknown</td>
            <td v-else>{{ image.labels.osversion }}</td>
            <td v-if="image.labels == null">Unknown</td>
            <td v-else>{{ image.labels.platform }}</td>
            <td>{{ image.size / 1024 / 1024 }}</td>
            <td>{{ image.createdFormatted }}</td>
            <td><button disabled class="btn-primary">New Container</button></td>
            <td><button disabled class="btn-danger">Remove</button></td>
          </tr>
        </tbody>
      </table>
      <nav aria-label="...">
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
        images: null,
        total: 0,
        pageSize: 5,
        currentPage: 1
      }
    },

    methods: {
      async loadPage(page) {
        this.currentPage = page

        try {
          var from = (page - 1) * (this.pageSize)
          var to = from + this.pageSize
          let response = await this.$http.get(`/api/image/images?from=${from}&to=${to}`)
          console.log(response.data.images)
          this.images = response.data.images
          this.total = response.data.total
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
