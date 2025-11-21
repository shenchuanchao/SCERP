<script setup lang="ts">
import { ref } from 'vue';
import { RouterLink, RouterView } from 'vue-router';

const isSidebarVisible = ref(true);

function toggleSidebar() {
  isSidebarVisible.value = !isSidebarVisible.value;
}
</script>

<template>
  <div class="layout">
    <aside class="sidebar" :class="{ hidden: !isSidebarVisible }">
    <div class="logo">
      <img alt="Vue logo" src="@/assets/logo.svg" width="125" height="125" />
      </div>
      <ul>
        <li><RouterLink to="/">Dashboard</RouterLink></li>
        <li><RouterLink to="/order/list">OrderList</RouterLink></li>
        <li><RouterLink to="/profile">Profile</RouterLink></li>
        <li><RouterLink to="/settings">Settings</RouterLink></li>
      </ul>
    </aside>

    <div class="right-panel">
      <header class="header">
        <button class="toggle-button" @click="toggleSidebar">
          {{ isSidebarVisible ? 'Hide Menu' : 'Show Menu' }}
        </button>
        <nav>
          <RouterLink to="/">Home</RouterLink>
          <RouterLink to="/about">About</RouterLink>
          <RouterLink to="/login">Login</RouterLink>
        </nav>
      </header>

      <main class="main-content">
        <RouterView />
      </main>

      <footer>
        <p>&copy; 2025 SCERP. All rights reserved.</p>
      </footer>
    </div>
  </div>
</template>

<style scoped>
.layout {
  display: flex;
  height: 100vh;
}

.sidebar {
  width: 200px;
  background-color: #f0f0f0;
  border-right: 1px solid #ddd;
  display: flex;
  flex-direction: column;
  transition: width 0.3s ease;
}

.sidebar.hidden {
  width: 0;
  padding: 0;
  overflow: hidden;
}

.logo {
  height: 60px; /* Match the height of the header */
      padding: 0.5rem;
      text-align:center;
          border-bottom: 1px solid #ddd;
}
.logo img{
  max-height:100%;
  max-width:100%;
}

.sidebar ul {
  list-style: none;
  padding: 1rem;
  width: 100%;
}

.sidebar li {
  margin-bottom: 1rem;
}

.sidebar a {
  text-decoration: none;
  color: #333;
  display:block;
}

.sidebar a.router-link-exact-active {
  font-weight: bold;
  color: #007bff;
}

.right-panel {
  flex: 1;
  display: flex;
  flex-direction: column;
}

.header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0.5rem;
  background-color: #f5f5f5;
  border-bottom: 1px solid #ddd;
  height: 60px; /* Set a fixed height for the header */
}

.toggle-button {
  padding: 0.5rem 1rem;
  border: none;
  background-color: #007bff;
  color: white;
  border-radius: 4px;
  cursor: pointer;
}

.toggle-button:hover {
  background-color: #0056b3;
}

nav a {
  margin: 0 1rem;
  text-decoration: none;
  color: #333;
}

nav a.router-link-exact-active {
  font-weight: bold;
  color: #007bff;
}

.main-content {
  flex: 1;
  padding: 1rem;
  overflow-y:auto;
}

footer {
  text-align: center;
  padding: 0.5rem;
  background-color: #f5f5f5;
  border-top: 1px solid #ddd;
}
</style>
