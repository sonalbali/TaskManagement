<template>
  <div class="form-container">
    <h1 class="form-title">{{ isEdit ? 'Edit Task' : 'Create Task' }}</h1>



    <form @submit.prevent="submitTask" class="task-form">
      <div class="form-group">
        <label>ID:</label>
        <input v-model="task.task_Id" readonly class="form-input" />
      </div>
      <div class="form-group">
        <label>Title:</label>
        <input v-model="task.title" required class="form-input" />
      </div>

      <div class="form-group">
        <label>Description:</label>
        <textarea v-model="task.description" required class="form-input textarea"></textarea>
      </div>

      <div class="form-group">
        <label>Assign to:</label>
        <select v-model="task.assign_To" required class="form-input">
          <option value="">Select User</option>
          <option v-for="user in users" :key="user.userId" :value="user.name">
            {{ user.name }}
          </option>
        </select>
      </div>

      <div class="form-group">
        <label>Status:</label>
        <select v-model="task.status" required class="form-input">
          <option>To Do</option>
          <option>In Progress</option>
          <option>Completed</option>
        </select>
      </div>

      <div class="form-group">
        <label>Due Date:</label>
        <input type="date" v-model="task.dueDate" class="form-input" />
      </div>

      <button type="submit" class="submit-btn">
        {{ isEdit ? 'Update' : 'Create' }}
      </button>
    </form>
  </div>
</template>

<script>
  import api from '../services/Api';

  export default {
    data() {
      return {
        task: {
          id:'',
          title: '',
          description: '',
          status: ' ',
          dueDate: null,
          assign_To:'',
        },
        users: [],
        isEdit: false,

      };
    },
    beforeCreate() {

        },
    methods: {

      fetchTask(id) {
        api.getTaskbyId(id).then((response) => {
          const fetchedTask = response.data;
          if (fetchedTask.dueDate) {
            fetchedTask.dueDate = fetchedTask.dueDate.split('T')[0];
          }
          this.task = {
            ...fetchedTask,
            assign_To: fetchedTask.assign_To?.toString()
          };
          console.log('Fetched task:', this.task);
        }).catch((error) => {
          console.error('Error fetching task:', error);
        });
      },

      submitTask() {

        const routeUserId = localStorage.getItem('UserId')
        if (this.isEdit==true)
        {
           api.updateTask(`${this.$route.params.id}`,this.task)//.then(() => this.$router.push({ name: 'UserTaskList', params: { Id:routeUserId}}))
           this.$router.push({ name: 'UserTaskList', params: { UserId: routeUserId} }); // Redirect to TaskList view

          }
        else
        {
          api.createTask(this.task).then(() => {
          // Navigate back to the task list with the username parameter
          this.$router.push(`/task/${routeUserId}`)//.then(() => this.$router.push({ name: 'UserTaskList', params: { Id:routeUserId}}));
        })
        .catch(error => {
          console.error('Error creating task:', error);
        });
        }
      },

      fetchUsers() {
        api.getAllUsers()
          .then((response) => {
            this.users = response.data;
          })
          .catch((error) => {
            console.error('Error fetching users:', error);
          });
      },

    },
  created() {
    this.fetchUsers();
    if (this.$route.params.id) {
      this.isEdit = true;
      this.fetchTask(this.$route.params.id);
    } else {
      this.task.task_Id = '0';  // Set ID to 0 in create mode
    }
  },
  };
</script>

<style scoped>
.form-container {
  max-width: 1000px;
  width: 800px;
  margin: 2rem auto;
  padding: 2rem;
  background-color: #ffffff;
  border-radius: 8px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}

.form-title {
  text-align: center;
  color: #2c3e50;
  margin-bottom: 2rem;
}

.task-form {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.form-group {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

.form-group label {
  color: #2c3e50;
  font-weight: 600;
}

.form-input {
  padding: 0.75rem;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 1rem;
  transition: border-color 0.3s ease;
}

.form-input:focus {
  outline: none;
  border-color: #007bff;
}

.textarea {
  min-height: 100px;
  resize: vertical;
}

.submit-btn {
  background-color: #007bff;
  color: white;
  padding: 0.75rem;
  border: none;
  border-radius: 4px;
  font-size: 1rem;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.submit-btn:hover {
  background-color: #007bff;
}

select.form-input {
  background-color: white;
}
</style>
