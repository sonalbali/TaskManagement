<!-- eslint-disable vue/multi-word-component-names -->

<template>
  <div>
    <h1>Login</h1>
    <form @submit.prevent="login">
    <div class="form-group">
      <label>User Name </label>
      <input v-model="Username" required />

       </div>

      <div class="form-group">
      <label>Password </label>
      <input type="password" v-model="PasswordHash" required />
       </div>
      <button type="submit">Login</button>
    </form>
     <p>
      Don't have an account?
      <router-link to="/register">Register here</router-link>
    </p>
  </div>

</template>

<script>
  import api from '../services/Api'; // Update to your login endpoint

  export default {
    data() {
      return {
        Username: '',
        PasswordHash: '',

      };

    },
    methods: {
      async login() {
        try {

          const response = await api.login({
            Username: this.Username,
            PasswordHash: this.PasswordHash,
          });


          localStorage.setItem('token', response.data.token);
          localStorage.setItem('userId', response.data.userId);
          const userId = response.data.userId;

            this.$router.push({ name: 'UserTaskList', params: { UserId: userId} }); // Redirect to TaskList view

          }
        catch (error) {
          console.log(error);

          alert('Login failed. Please check your credentials.');
        }
      },
    },
  };
</script>
<style scoped>
.login-container {
  max-width: 400px;
  margin: auto;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 8px;
  background: #f9f9f9;
}

.form-group {
  margin-bottom: 15px;
}

label {
  display: block;
  font-weight: bold;
}

input {
  width: 100%;
  padding: 8px;
  margin-top: 5px;
  border: 1px solid #ccc;
  border-radius: 4px;
}

button {
  width: 100%;
  padding: 10px;
  background: #007bff;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

button:hover {
  background: #0056b3;
}
</style>


