<template>
  <div class="order-form">
    <h1>{{ isEditMode ? 'Edit Order' : 'New Order' }}</h1>
    <form @submit.prevent="saveOrder">
      <div class="form-group">
        <label for="customerName">Customer Name:</label>
        <input
          type="text"
          id="customerName"
          v-model="order.customerName"
          required
        />
      </div>
      <div class="form-group">
        <label for="orderDate">Order Date:</label>
        <input
          type="date"
          id="orderDate"
          v-model="order.orderDate"
          required
        />
      </div>
      <div class="form-group">
        <label for="status">Status:</label>
        <select id="status" v-model="order.status" required>
          <option value="Pending">Pending</option>
          <option value="Completed">Completed</option>
          <option value="Cancelled">Cancelled</option>
        </select>
      </div>
      <div class="form-group">
        <label for="total">Total:</label>
        <input
          type="number"
          id="total"
          v-model="order.total"
          required
          min="0"
          step="0.01"
        />
      </div>
      <div class="form-actions">
        <button type="submit" class="save-button">Save</button>
        <button type="button" class="cancel-button" @click="closeForm">Cancel</button>
      </div>
    </form>
  </div>
</template>

<script setup lang="ts">
import { ref, defineEmits, defineProps, watch } from 'vue';

const emit = defineEmits(['close', 'save']);
const props = defineProps({
  orderData: {
    type: Object,
    default: () => ({
      customerName: '',
      orderDate: '',
      status: 'Pending',
      total: 0,
    }),
  },
});

const order = ref({ ...props.orderData });
const isEditMode = ref(false);

watch(
  () => props.orderData,
  (newOrder) => {
    order.value = { ...newOrder };
    isEditMode.value = !!newOrder; // Check if editing an existing order
  },
  { immediate: true }
);

function saveOrder() {
  emit('save', { ...order.value });
  emit('close');
}

function closeForm() {
  emit('close');
}
</script>

<style scoped>
.order-form {
  max-width: 500px;
  margin: 0 auto;
  padding: 1rem;
  border: 1px solid #ddd;
  border-radius: 8px;
  background-color: #f9f9f9;
}

h1 {
  text-align: center;
  margin-bottom: 1rem;
}

.form-group {
  margin-bottom: 1rem;
}

label {
  display: block;
  margin-bottom: 0.5rem;
}

input,
select {
  width: 100%;
  padding: 0.5rem;
  border: 1px solid #ccc;
  border-radius: 4px;
  box-sizing: border-box;
}

.form-actions {
  display: flex;
  justify-content: space-between;
}

button {
  padding: 0.5rem 1rem;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.save-button {
  background-color: #007bff;
  color: white;
}

.save-button:hover {
  background-color: #0056b3;
}

.cancel-button {
  background-color: #dc3545;
  color: white;
}

.cancel-button:hover {
  background-color: #c82333;
}
</style>
