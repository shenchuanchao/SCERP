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
    <el-table :data="paginatedOrders" border height="350" style="width: 100%"
    highlight-current-row>
      <el-table-column prop="id" label="Id" width="180" />
      <el-table-column prop="customerName" label="customerName" width="180" />
      <el-table-column prop="orderDate" label="orderDate" sortable  />
      <el-table-column prop="status" label="status" />
      <el-table-column prop="total" label="total" sortable  />
      <el-table-column fixed="right" label="Operations" min-width="120">
        <template #default="scope">
          <el-button size="small" @click="handleEdit(scope.$index, scope.row)">
            Edit
          </el-button>
          <el-button
            size="small"
            type="danger"
            @click="confirmDelete(scope.row)"
          >
            Delete
          </el-button>
        </template>
      </el-table-column>
    </el-table>
    <div class="pagination">
      <button :disabled="currentPage === 1" @click="currentPage--">Previous</button>
      <span>Page {{ currentPage }} of {{ totalPages }}</span>
      <button :disabled="currentPage === totalPages" @click="currentPage++">Next</button>
    </div>

    <!-- 订单表单组件 -->
      <order-form
        :visible.sync="false"
        :edit-data="currentEditData"
        :is-edit="isEditMode"
        @success="handleFormSuccess"
        @close="handleFormClose">
      </order-form>

    <!-- Delete Confirmation Dialog -->
    <el-dialog
      v-model="showDeleteConfirm"
      title="删除提示"
      width="500"
      transition="dialog-bounce"
      draggable
    >
      <span>您确认要删除此订单？</span>
      <template #footer>
        <div class="dialog-footer">
          <el-button @click="closeDeleteConfirm">取消</el-button>
          <el-button type="primary" @click="deleteOrder">确认删除</el-button>
        </div>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue';
import OrderForm from './OrderForm.vue';

const orders = ref([
  { id: 1, customerName: 'John Doe', orderDate: '2023-10-01', status: 'Pending', total: 100 },
  { id: 2, customerName: 'Jane Smith', orderDate: '2023-10-02', status: 'Completed', total: 200 },
  { id: 3, customerName: 'Alice Johnson', orderDate: '2023-10-03', status: 'Cancelled', total: 150 },
  { id: 4, customerName: 'Alice Johnson', orderDate: '2023-10-03', status: 'Cancelled', total: 150 },
  { id: 5, customerName: 'Alice Johnson', orderDate: '2023-10-03', status: 'Cancelled', total: 150 },
  { id: 6, customerName: 'Alice Johnson', orderDate: '2023-10-03', status: 'Cancelled', total: 150 },
  { id: 7, customerName: 'Alice Johnson', orderDate: '2023-10-03', status: 'Cancelled', total: 150 },
  { id: 8, customerName: 'Alice Johnson', orderDate: '2023-10-03', status: 'Cancelled', total: 150 },
  { id: 9, customerName: 'Alice Johnson', orderDate: '2023-10-03', status: 'Cancelled', total: 150 },
  { id: 10, customerName: 'Alice Johnson', orderDate: '2023-10-03', status: 'Cancelled', total: 150 },
  { id: 11, customerName: 'Alice Johnson', orderDate: '2023-10-03', status: 'Cancelled', total: 150 },
]);

const searchQuery = ref('');
const currentPage = ref(1);
const itemsPerPage = 10;
const showOrderForm = ref(false);
const selectedOrder = ref(null);
const isEditMode= ref(false);
const showDeleteConfirm = ref(false);
const confirmedOrder = ref(null);

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
   isEditMode = false;
      selectedOrder = null;
      showOrderForm = true;

}

function handleEdit(index: number, row: any) {
  const order = orders.value.find((o) => o.id === row.id);
  if (order) {
    selectedOrder.value = { ...order }; // Pass a copy of the order to edit
    showOrderForm.value = true;
  }
}

function confirmDelete(order: any) {
  confirmedOrder.value = order;
  showDeleteConfirm.value = true;
}

function closeDeleteConfirm() {
  showDeleteConfirm.value = false;
  confirmedOrder.value = null;
}

function deleteOrder() {
  if (confirmedOrder.value) {
    orders.value = orders.value.filter((o) => o.id !== confirmedOrder.value.id);
    closeDeleteConfirm();
  }
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

.dialog-bounce-enter-active,
.dialog-bounce-leave-active,
.dialog-bounce-enter-active .el-dialog,
.dialog-bounce-leave-active .el-dialog {
  transition: all 0.5s cubic-bezier(0.175, 0.885, 0.32, 1.275);
}

.dialog-bounce-enter-from,
.dialog-bounce-leave-to {
  opacity: 0;
}

.dialog-bounce-enter-from .el-dialog,
.dialog-bounce-leave-to .el-dialog {
  transform: scale(0.3) translateY(-50px);
  opacity: 0;
}
</style>
