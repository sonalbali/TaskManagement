<template>
  <div>
    <h1>Task List</h1>
    <select v-model="filterStatus">
      <option value="">All</option>
      <option>To Do</option>
      <option>In Progress</option>
      <option>Completed</option>
    </select>
    <ul>
      <li v-for="task in filteredTasks" :key="task.id">
        <h3>{{ task.title }}</h3>
        <p>{{ task.description }}</p>
        <p>Status: {{ task.status }}</p>
        <button @click="editTask(task)">Edit</button>
        <button @click="deleteTask(task.id)">Delete</button>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
    import api from '../services/api';
    import { defineComponent } from 'vue';

    //export default defineComponent({
    //    components: {
    //    },
    //    directives: {
    //    },
    //    filters: {
    //    },
    //    props: {
    //    },
    //    data() {
    //        return {
    //        }
    //    },
    //    computed: {
    //    },
    //    watch: {
    //    },
    //    beforeCreate() {
    //    },
    //    created() {
    //    },
    //    beforeMount() {
    //    },
    //    mounted() {
    //    },
    //    updated() {
    //    },
    //    activated() {
    //    },
    //    deactivated() {
    //    },
    //    beforeDestroy() {
    //    },
    //    destroyed() {
    //    },
    //    methods: {
    //    },
  //});

  //import api from '@/services/api';

  export default {
    data() {
      return {
        tasks: [],
        filterStatus: '',
      };
    },
    computed: {
      filteredTasks() {
        return this.filterStatus
          ? this.tasks.filter((task) => task.status === this.filterStatus)
          : this.tasks;
      },
    },
    methods: {
      fetchTasks() {
        api.getTasks().then((response) => {
          this.tasks = response.data;
        });
      },
      editTask(task) {
        this.$router.push({ name: 'EditTask', params: { id: task.id } });
      },
      deleteTask(id) {
        api.deleteTask(id).then(() => this.fetchTasks());
      },
    },
    created() {
      this.fetchTasks();
    },
  };

</script>

<style>
</style>
