import { createRouter, createWebHistory } from 'vue-router';
import Login from '../components/Login.vue';
import Register from '../components/Register.vue';
//import TaskList from '../View/TaskListView.vue';
import TaskList from '../components/TaskList.vue';
import TaskForm from '../components/TaskForm.vue';

const routes = [
  { path: '/login', name: 'Login', component: Login },
  { path: '/Register', name: 'Register', component: Register },
  { path: '/Task/:UserId', name: 'UserTaskList', component: TaskList },
  { path: '/task', name: 'AllTaskList', component: TaskList },
  //{ path: '/task/new', name: 'NewTask', component: TaskForm },
 //working api  { path: '/updateTask/:id', name: 'TaskForm', component: TaskForm,  props: (route) => ({ task: route.params.task }) },
  { path: '/updateTask/:id', name: 'TaskForm', component: TaskForm,  props: true  },

  //{ path: '/task-form/:id?',      name: 'TaskForm',      component: TaskForm    },
  { path: '/task/', name: 'createTask',component: TaskForm},
  { path: '/Task/:id', name: 'deleteTask',component: TaskList},
 // { path: '/Registration/users', name: 'users',component: TaskForm},
 // { path: '/Task/search/:id', name: 'EditTask', component: TaskForm, props: true },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
