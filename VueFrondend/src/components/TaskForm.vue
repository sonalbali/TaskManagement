<template>
  <form @submit.prevent="submitTask">
    <label>Title:</label>
    <input v-model="task.title" required />

    <label>Description:</label>
    <textarea v-model="task.description" required></textarea>

    <label>Status:</label>
    <select v-model="task.status" required>
      <option>To Do</option>
      <option>In Progress</option>
      <option>Completed</option>
    </select>

    <label>Due Date (optional):</label>
    <input type="date" v-model="task.dueDate" />

    <button type="submit">Save Task</button>
  </form>
</template>

<script>
  import api from '../services/api';

  export default {
    data() {
      return {
        task: {
          title: '',
          description: '',
          status: 'To Do',
          dueDate: null,
        },
      };
    },
    methods: {
      fetchTask() {
        if (this.$route.params.id) {
          api.getTask(this.$route.params.id).then((response) => {
            this.task = response.data;
          });
        }
      },
      submitTask() {
        if (this.task.id) {
          api.updateTask(this.task.id, this.task).then(() => this.$router.push('/'));
        } else {
          api.createTask(this.task).then(() => this.$router.push('/'));
        }
      },
    },
    created() {
      this.fetchTask();
    },
  };
</script>




<!--<template>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';

    export default defineComponent({
        components: {
        },
        directives: {
        },
        filters: {
        },
        props: {
        },
        data() {
            return {
            }
        },
        computed: {
        },
        watch: {
        },
        beforeCreate() {
        },
        created() {
        },
        beforeMount() {
        },
        mounted() {
        },
        updated() {
        },
        activated() {
        },
        deactivated() {
        },
        beforeDestroy() {
        },
        destroyed() {
        },
        methods: {
        },
    });
</script>

<style>
</style>-->
