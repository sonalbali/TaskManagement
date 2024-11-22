<template>
  <nav v-if="!isAuthPage" class="nav-bar">
    <div class="nav-content">
      <span class="username">Welcome, {{ username }}</span>
      <button @click="logout" class="logout-btn">Logout</button>
    </div>
  </nav>
</template>

<script>
export default {
  name: 'NavBar',
  computed: {
    username() {
      return this.$route.params.Username || localStorage.getItem('username');
    },
    isAuthPage() {
      return ['/login', '/register'].includes(this.$route.path);
    }
  },
  methods: {
    logout() {
      localStorage.removeItem('username');
      localStorage.removeItem('token');
      localStorage.removeItem('userId');
      this.$router.push('/login');
    }
  }
};
</script>

<style scoped>
.nav-bar {
  background-color: #007bff;
  padding: 1rem;
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  width: 100%;
  z-index: 1000;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.nav-content {
  max-width: 1200px;
  margin: 0 auto;
  display: flex;
  justify-content: flex-end;
  align-items: center;
  gap: 1rem;
  padding: 0 1rem;
}

.username {
  color: white;
  font-weight: 500;
}

.logout-btn {
  background-color: white;
  color: #007bff;
  border: none;
  padding: 0.5rem 1rem;
  border-radius: 4px;
  cursor: pointer;
  font-weight: 500;
  transition: all 0.3s ease;
}

.logout-btn:hover {
  background-color: #f8f9fa;
  transform: translateY(-1px);
}
</style>
