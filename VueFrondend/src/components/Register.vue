<!-- eslint-disable vue/multi-word-component-names -->


<template>
  <div class="form-container">
    <h1 class="form-title">Register</h1>
    <form @submit.prevent="register" class="register-form" novalidate>
      <div class="form-group">
        <label>Username</label>
        <input
          v-model="Username"
          @blur="validateUsername"
          :class="{ 'error-input': usernameError }"
          class="form-input"
          required
        />
        <span class="error-message" v-if="usernameError">{{ usernameError }}</span>
      </div>

      <div class="form-group">
        <label>Password</label>
        <input
          type="password"
          v-model="PasswordHash"
          @blur="validatePassword"
          :class="{ 'error-input': passwordError }"
          class="form-input"
          required
        />
        <span class="error-message" v-if="passwordError">{{ passwordError }}</span>
      </div>

      <button type="submit" class="submit-btn" :disabled="!isFormValid">Register</button>

      <div class="login-link">
        Already have an account? <router-link to="/login">Login here</router-link>
      </div>
    </form>
  </div>
</template>

<script>
import api from '../services/Api';

export default {
  data() {
    return {
      Username: '',
      PasswordHash: '',
      usernameError: '',
      passwordError: ''
    };
  },
  computed: {
    isFormValid() {
      return this.Username &&
             this.PasswordHash &&
             !this.usernameError &&
             !this.passwordError;
    }
  },
  methods: {
    validateUsername() {
      if (!this.Username) {
        this.usernameError = 'Username is required';
      } else if (this.Username.length < 3) {
        this.usernameError = 'Username must be at least 3 characters';
      } else {
        this.usernameError = '';
      }
    },
    validatePassword() {
      if (!this.PasswordHash) {
        this.passwordError = 'Password is required';
      } else if (this.PasswordHash.length < 6) {
        this.passwordError = 'Password must be at least 6 characters';
      } else {
        this.passwordError = '';
      }
    },
    async register() {
      this.validateUsername();
      this.validatePassword();

      if (!this.isFormValid) {
        return;
      }

      try {
        await api.register({
          Username: this.Username,
          PasswordHash: this.PasswordHash,
        });
        alert('Registration successful. You can now log in.');
        this.$router.push('/login');
      // eslint-disable-next-line no-unused-vars
      } catch (error) {

        alert('Registration failed. Please try again.');
      }
    }
  }
};
</script>

<style scoped>
.form-container {
  max-width: 400px;
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

.register-form {
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

.error-input {
  border-color: #dc3545;
}

.error-message {
  color: #dc3545;
  font-size: 0.8rem;
  margin-top: 0.25rem;
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
  margin-top: 1rem;
}

.submit-btn:hover {
  background-color: #007bff;
}

.submit-btn:disabled {
  background-color: #cccccc;
  cursor: not-allowed;
}

.login-link {
  text-align: center;
  margin-top: 1rem;
  color: #2c3e50;
}

.login-link a {
  color: #007bff;
  text-decoration: none;
}

.login-link a:hover {
  text-decoration: underline;
}
</style>
