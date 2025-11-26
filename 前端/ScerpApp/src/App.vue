<script setup lang="ts">
import { ref } from 'vue';
import { RouterLink, RouterView } from 'vue-router';

const isCollapse = ref(false);

function toggleSidebar() {
  isCollapse.value = !isCollapse.value;
}
</script>

<template>
  <div class="layout">
    <el-container>    
      <el-aside class="sidebar" >
        
        <el-menu
          default-active="2"
          class="el-menu-vertical-demo"
          :collapse="isCollapse"
        >
        <el-menu-item index="0">
          <img alt="Vue logo" src="@/assets/logo.svg" style="max-height:100%;max-width:100%;" />
        </el-menu-item>
        <el-menu-item index="1">
            <RouterLink to="/">
              <el-icon><el-icon><HomeFilled /></el-icon></el-icon>
              Dashboard
            </RouterLink>            
        </el-menu-item>
        <el-sub-menu index="2">
            <template #title>
              <el-icon><el-icon><Goods /></el-icon></el-icon>
              <span>销售管理</span>
            </template>
              <el-menu-item index="1-1">
              <RouterLink to="/order/list">订单列表</RouterLink>
              </el-menu-item>        
          </el-sub-menu>
          <el-sub-menu index="3">
            <template #title>
              <el-icon><Setting /></el-icon>
              <span>系统管理</span>
            </template>
              <el-menu-item index="2-1">
              <RouterLink to="/login">Login</RouterLink>
              </el-menu-item>
              <el-menu-item index="2-2">
              <RouterLink to="/register">Register</RouterLink>
              </el-menu-item>       
          </el-sub-menu>
         
          
        </el-menu>
      </el-aside>
      <el-container>
        <el-header class="header">
        <div>
         <el-radio-group v-model="isCollapse">
          <el-radio-button :value="true" v-show="!isCollapse"><el-icon color="#007bff"  ><Fold /></el-icon></el-radio-button>
          <el-radio-button :value="false" v-show="isCollapse"><el-icon color="#007bff"  ><Expand /></el-icon></el-radio-button>
        </el-radio-group>
        
        
        </div>
       
        <nav>
          <RouterLink to="/">Home</RouterLink>
          <RouterLink to="/about">About</RouterLink>
          <RouterLink to="/login">Login</RouterLink>
          <RouterLink to="/register">Register</RouterLink>
        </nav>
        </el-header>
        <el-main>
        <RouterView />
        </el-main>
        <el-footer>
        <p>&copy; 2025 SCERP. All rights reserved.</p>
        </el-footer>
      </el-container>
    </el-container>
  </div>
  
</template>



<style scoped>
.layout {
  display: flex;
  height: 100vh;
}

.el-menu-vertical-demo{
 height: 100vh;
}
.el-menu-vertical-demo:not(.el-menu--collapse) {
  width: 200px;
 
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
    background-color: #f5f5f5;
    border-bottom: 1px solid #ddd;
}


nav a {
  margin: 0 1rem;
  text-decoration: none;
  color: #333;
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
