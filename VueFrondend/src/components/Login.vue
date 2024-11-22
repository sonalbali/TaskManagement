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
</script>-->
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
</script>-->

<template>
  <div>
    <h1>Login</h1>
    <form @submit.prevent="login">
      <label>Username:</label>
      <input v-model="userName" required />

      <label>Password:</label>
      <input type="password" v-model="password" required />

      <button type="submit">Login</button>
    </form>
  </div>
</template>

<script>
  import api from '../services/api'; // Update to your login endpoint

  export default {
    data() {
      return {
        userName: '',
        password: '',
      };
    },
    methods: {
      async login() {
        try {
          const response = await api.post('/Registration/login', {
            userName: this.userName,
            password: this.password,
          });
          // Handle login success (e.g., store JWT, redirect to tasks)
          localStorage.setItem('token', response.data.token);
          this.$router.push('/');
        } catch (error) {
          alert('Login failed. Please check your credentials.');
        }
      },
    },
  };
</script>

<style>
</style>
