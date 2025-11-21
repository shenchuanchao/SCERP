<template>
  <div class="order-list">
    <div class="top-bar">
    <div class="button-list">
      <button class="new-order-button" @click="addOrder">New Order</button>
      <button class="refresh-button" @click="refreshOrders">Refresh</button>
      </div>
      <div class="search-bar">
        <input
          type="text"
          v-model="searchQuery"
          placeholder="Search orders..."
          @input="filterOrders"
        />
        <button class="search-button" @click="onSearchClick">Search</button>
      </div>
    </div>
    <table class="order-table">
      <thead>
        <tr>
          <th>Order ID</th>
          <th>Customer Name</th>
          <th>Order Date</th>
          <th>Status</th>
          <th>Total</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="order in paginatedOrders" :key="order.id">
          <td>{{ order.id }}</td>
          <td>{{ order.customerName }}</td>
          <td>{{ order.orderDate }}</td>
          <td>{{ order.status }}</td>
          <td>{{ order.total }}</td>
          <td>
            <button class="edit-button" @click="editOrder(order.id)">Edit</button>
            <button class="delete-button" @click="confirmDelete(order.id)">Delete</button>
          </td>
        </tr>
        <tr v-if="!paginatedOrders.length">
          <td colspan="6">No orders found.</td>
        </tr>
      </tbody>
    </table>
    <div class="pagination">
      <button :disabled="currentPage === 1" @click="currentPage--">Previous</button>
      <span>Page {{ currentPage }} of {{ totalPages }}</span>
      <button :disabled="currentPage === totalPages" @click="currentPage++">Next</button>
    </div>

    <!-- Order Form Modal -->
    <div v-if="showOrderForm" class="modal" @click.self="closeOrderForm">
      <div class="modal-content">
        <OrderForm
          :orderData="selectedOrder"
          @save="handleSaveOrder"
          @close="closeOrderForm"
        />
      </div>
    </div>

    <!-- Delete Confirmation Modal -->
    <div v-if="showDeleteConfirm" class="modal" @click.self="closeDeleteConfirm">
      <div class="modal-content">
        <p>Are you sure you want to delete this order?</p>
        <div class="modal-actions">
          <button class="confirm-button" @click="deleteOrder(confirmedOrderId)">Yes</button>
          <button class="cancel-button" @click="closeDeleteConfirm">No</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue';
import OrderForm from './OrderForm.vue';

const orders = ref([
  { id: 1, customerName: 'John Doe', orderDate: '2023-10-01', status: 'Pending', total: 100 },
  { id: 2, customerName: 'Jane Smith', orderDate: '2023-10-02', status: 'Completed', total: 200 },
  { id: 3, customerName: 'Alice Johnson', orderDate: '2023-10-03', status: 'Cancelled', total: 150 },
]);

const searchQuery = ref('');
const currentPage = ref(1);
const itemsPerPage = 5;
const showOrderForm = ref(false);
const selectedOrder = ref(null);
const showDeleteConfirm = ref(false);
const confirmedOrderId = ref<number | null>(null);

const filteredOrders = computed(() => {
  if (!searchQuery.value) return orders.value;
  return orders.value.filter(order =>
    Object.values(order).some(value =>
      String(value).toLowerCase().includes(searchQuery.value.toLowerCase())
    )
  );
});

const totalPages = computed(() => Math.ceil(filteredOrders.value.length / itemsPerPage));

const paginatedOrders = computed(() => {
  const start = (currentPage.value - 1) * itemsPerPage;
  return filteredOrders.value.slice(start, start + itemsPerPage);
});

function filterOrders() {
  currentPage.value = 1; // Reset to the first page on search
}

function onSearchClick() {
  console.log('Search button clicked with query:', searchQuery.value);
}

function addOrder() {
    console.log("click add");
  selectedOrder.value = null; // Clear selected order for new entry
  showOrderForm.value = true;
}

function editOrder(orderId: number) {
  const order = orders.value.find((o) => o.id === orderId);
  if (order) {
    selectedOrder.value = { ...order }; // Pass a copy of the order to edit
    showOrderForm.value = true;
  }
}

function confirmDelete(orderId: number) {
  confirmedOrderId.value = orderId;
  showDeleteConfirm.value = true;
}

function closeDeleteConfirm() {
  showDeleteConfirm.value = false;
  confirmedOrderId.value = null;
}

function deleteOrder(orderId: number) {
  orders.value = orders.value.filter((o) => o.id !== orderId);
  closeDeleteConfirm();
}

function handleSaveOrder(order: any) {
  if (order.id) {
    // Edit existing order
    const index = orders.value.findIndex((o) => o.id === order.id);
    if (index !== -1) {
      orders.value[index] = order;
    }
  } else {
    // Add new order
    order.id = Date.now(); // Generate a unique ID
    orders.value.push(order);
  }
}

function closeOrderForm() {
  showOrderForm.value = false;
}

function refreshOrders() {
  console.log('Refreshing orders...');
  // Simulate refreshing the order list (e.g., fetch from API)
  // For now, just log the action
}
</script>

<style scoped>
.order-list {
  padding: 1rem;
}

.top-bar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1rem;
}

.new-order-button {
  padding: 0.5rem 1rem;
  border: none;
  background-color: #28a745;
  color: white;
  border-radius: 4px;
  cursor: pointer;
  margin-right: 0.5rem;
}

.new-order-button:hover {
  background-color: #218838;
}

.refresh-button {
  padding: 0.5rem 1rem;
  border: none;
  background-color: #17a2b8;
  color: white;
  border-radius: 4px;
  cursor: pointer;
}

.refresh-button:hover {
  background-color: #138496;
}

.search-bar {
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

.search-bar input {
  flex: 1;
  padding: 0.5rem;
  border: 1px solid #ccc;
  border-radius: 4px;
}

.search-button {
  padding: 0.5rem 1rem;
  border: none;
  background-color: #007bff;
  color: white;
  border-radius: 4px;
  cursor: pointer;
}

.search-button:hover {
  background-color: #0056b3;
}

.order-table {
  width: 100%;
  border-collapse: collapse;
  margin-bottom: 1rem;
}

.order-table th,
.order-table td {
  border: 1px solid #ddd;
  padding: 0.75rem;
  text-align: left;
}

.order-table th {
  background-color: #f5f5f5;
}

.edit-button,
.delete-button {
  padding: 0.25rem 0.5rem;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.edit-button {
  background-color: #007bff;
  color: white;
  margin-right: 0.5rem;
}

.edit-button:hover {
  background-color: #0056b3;
}

.delete-button {
  background-color: #dc3545;
  color: white;
}

.delete-button:hover {
  background-color: #c82333;
}

.pagination {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.pagination button {
  padding: 0.5rem 1rem;
  border: none;
  background-color: #007bff;
  color: white;
  border-radius: 4px;
  cursor: pointer;
}

.pagination button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

.modal {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
}

.modal-content {
  background: white;
  padding: 1rem;
  border-radius: 8px;
  width: 90%;
  max-width: 500px;
  text-align: center;
}

.modal-actions {
  display: flex;
  justify-content: center;
  gap: 1rem;
  margin-top: 1rem;
}

.confirm-button {
  padding: 0.5rem 1rem;
  border: none;
  background-color: #dc3545;
  color: white;
  border-radius: 4px;
  cursor: pointer;
}

.confirm-button:hover {
  background-color: #c82333;
}

.cancel-button {
  padding: 0.5rem 1rem;
  border: none;
  background-color: #6c757d;
  color: white;
  border-radius: 4px;
  cursor: pointer;
}

.cancel-button:hover {
  background-color: #5a6268;
}
</style>
