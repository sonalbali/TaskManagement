<template>
  <NavBar />
  <div class="main-content">
    <router-view />
  </div>
</template>

<script>
import NavBar from './components/NavBar.vue'

export default {
  components: {
    NavBar
  }
}
</script>

<style>
.main-content {
  margin-top: 60px;
  padding: 1rem;
}
</style>
