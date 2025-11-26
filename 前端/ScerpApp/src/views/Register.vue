<template>
  <div class="register-container">
    <el-card class="register-card">
      <h2 class="register-title">用户注册</h2>
      <el-form :model="registerForm" :rules="registerRules" ref="registerFormRef" label-width="80px">
        <el-form-item label="用户名" prop="username">
          <el-input v-model="registerForm.username" placeholder="请输入用户名"></el-input>
        </el-form-item>
        <el-form-item label="密码" prop="password">
          <el-input
            v-model="registerForm.password"
            placeholder="请输入密码"
            type="password"
            show-password
          ></el-input>
        </el-form-item>
        <el-form-item label="确认密码" prop="confirmPassword">
          <el-input
            v-model="registerForm.confirmPassword"
            placeholder="请再次输入密码"
            type="password"
            show-password
          ></el-input>
        </el-form-item>
        <el-form-item label="邮箱" prop="email">
          <el-input v-model="registerForm.email" placeholder="请输入邮箱"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="handleRegister">注册</el-button>
          <el-button @click="resetRegisterForm">重置</el-button>
        </el-form-item>
      </el-form>
    </el-card>
  </div>
</template>

<script setup>
import { ref, reactive } from 'vue';
import { ElMessage } from 'element-plus';

const registerForm = reactive({
  username: '',
  password: '',
  confirmPassword: '',
  email: '',
});

const validateConfirmPassword = (rule, value, callback) => {
  if (value === registerForm.password) {
    callback();
  } else {
    callback(new Error('两次输入密码不一致'));
  }
};

const registerRules = reactive({
  username: [
    { required: true, message: '请输入用户名', trigger: 'blur' },
    { min: 3, message: '用户名长度至少为3位', trigger: 'blur' },
  ],
  password: [
    { required: true, message: '请输入密码', trigger: 'blur' },
    { min: 6, message: '密码长度至少为6位', trigger: 'blur' },
  ],
  confirmPassword: [
    { required: true, message: '请再次输入密码', trigger: 'blur' },
    { validator: validateConfirmPassword, message: '两次输入密码不一致', trigger: 'blur' },
  ],
  email: [
    { required: true, message: '请输入邮箱', trigger: 'blur' },
    { type: 'email', message: '请输入正确的邮箱格式', trigger: 'blur' },
  ],
});

const registerFormRef = ref(null);



const handleRegister = async () => {
  if (!registerFormRef.value.validate()) {
    return;
  }
  // 这里可以添加实际的注册逻辑，比如发送axios请求到后端保存用户注册信息
  // 暂时模拟注册成功提示
  ElMessage.success('注册成功');
};

const resetRegisterForm = () => {
  registerFormRef.value.resetFields();
};
</script>

<style scoped>
.register-container {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: calc(100vh - var(--el-main-padding) * 2 - 120px);
  background-color: #f4f4f4;
}

.register-card {
  width: 420px;
}

.register-title {
  text-align: center;
  margin-bottom: 20px;
}
</style>