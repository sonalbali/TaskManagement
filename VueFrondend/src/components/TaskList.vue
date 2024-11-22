<template>
  <div>
    <h1>Task List</h1>
    <button @click="createTask" class="create-btn">Create Task</button>

    <!-- Filters -->
    <div class="filters">
      <select v-model="filterStatus">
        <option value="">All Status</option>
        <option>To Do</option>
        <option>In Progress</option>
        <option>Completed</option>
      </select>

      <input
        type="date"
        v-model="filterDueDate"
        placeholder="Filter by due date"
      >
    </div>

    <!-- Table -->
    <table class="task-table">
      <thead>
        <tr>
          <th>Title</th>
          <th>Description</th>
          <th>Status</th>
          <th>Due Date</th>
          <th>Assigned To</th>
          <th>UserId</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="task in filteredTasks" :key="task.id">
          <td>{{ task.title }}</td>
          <td>{{ task.description }}</td>
          <td>{{ task.status }}</td>
          <td>{{ task.dueDate ? task.dueDate.split('T')[0] : '' }}</td>
          <td>{{ task.assign_To }}</td>
          <td>{{ task.userId }}</td>
          <td>
            <button @click="editTask(task.task_Id)">Edit</button>
            <button @click="deleteTask(task.task_Id)">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
// ... existing imports ...impot//
import Api from '../services/Api';
export default {
  data() {
    return {
      tasks: [],
      filterStatus: '',
      filterDueDate: ''
    };
  },
  computed: {
    // ... existing Username computed property ...

    filteredTasks() {
      const routeUserId = localStorage.getItem('userId')
      //const userId = this.$route.params.UserId;
      console.log('Current UserId from route:', routeUserId);

      return this.tasks.filter(task => {

        const matchesStatus = !this.filterStatus || task.status === this.filterStatus;
        const taskDate = task.dueDate ? task.dueDate.split('T')[0] : '';
        const matchesDate = !this.filterDueDate || taskDate === this.filterDueDate;
        const matchesUser = String(task.userId) === String(routeUserId); // Changed to lowercase userId

        //console.log('Matches:', { matchesStatus, matchesDate, matchesUser });

        return matchesStatus && matchesDate && matchesUser;
      });
    }
  },
  methods: {

    fetchTasks() {
      const routeUserId = localStorage.getItem('userId')
        Api.getAllTasks().then((response) => {
          this.tasks= response.data;
        this.filteredTasks = this.tasks.filter((task) => task.userId == routeUserId);
        });
      },
    formatDate(date) {
      if (!date) return '';
      return date.split('T')[0]; ////new Date(date).toLocaleDateString();
    },

    createTask(task) {
      this.$router.push({ name: 'createTask', param :{task}}); // Redirect to TaskList view

     // this.$router.push('/createTask');  // Assuming you have a create task route
    },
    editTask(id) {
      this.$router.push({ name: 'TaskForm', params: {id} });

    },
    deleteTask(taskId) {
     Api.deleteTask(`${taskId}`) .then(() => {
      console.log(`Task with ID ${taskId} deleted successfully.`);
      // Reload the task list
      this.fetchTasks();
    });
      },
  },
  created() {
     //this.Username=this.Username();
    this.fetchTasks();

    },
};
</script>

<style scoped>
.filters {
  margin: 20px 0;
  display: flex;
  gap: 10px;
}
.create-btn{
    background-color: #007bff;
    color: white;
    padding: 0.75rem;
    border: none;
    border-radius: 4px;
    font-size: 1rem;
    cursor: pointer;
    transition: background-color 0.3s
}

.task-table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 20px;
}

.task-table th,
.task-table td {
  border: 1px solid #ddd;
  padding: 8px;
  text-align: left;
}

.task-table th {
  background-color: #007bff;
  color: white;
}

.task-table tr:nth-child(even) {
  background-color: #f9f9f9;
}

.task-table tr:hover {
  background-color: #f5f5f5;
}

button {
  margin: 0 5px;
  padding: 5px 10px;
  cursor: pointer;
}
</style>
