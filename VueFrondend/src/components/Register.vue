<!--<template>-->
<!--</template>

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

<template>
  <div>
    <h1>Register</h1>
    <form @submit.prevent="register">
      <label>Username:</label>
      <input v-model="userName" required />

      <label>Password:</label>
      <input type="password" v-model="password" required />

      <label>Confirm Password:</label>
      <input type="password" v-model="confirmPassword" required />

      <button type="submit">Register</button>
    </form>
  </div>
</template>

<script>
  import api from '../services/api'; // Update to your registration endpoint

  export default {
    data() {
      return {
        userName: '',
        password: '',
        confirmPassword: '',
      };
    },
    methods: {
      async register() {
        if (this.password !== this.confirmPassword) {
          alert('Passwords do not match.');
          return;
        }

        try {
          await api.post('/Registration/register', {
            userName: this.userName,
            password: this.password,
          });
          alert('Registration successful. You can now log in.');
          this.$router.push('/login');
        } catch (error) {
          alert('Registration failed. Please try again.');
        }
      },
    },
  };
</script>
