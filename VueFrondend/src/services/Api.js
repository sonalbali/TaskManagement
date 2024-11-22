import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'http://localhost:5083/api', // Change to your API URL
  headers: {
    'Content-Type': 'application/json',
    'Access-Control-Allow-Origin': "*",
  },
});
//export default apiClient;
export default {
  login(credentials) {
    return apiClient.post('/Registration/login', credentials);
  },
  register(user) {
    return apiClient.post('/Registration/register', user);
  },
  getAllTasks() {
    return apiClient.get('/Task/');
  },
  getTask(UserId) {
   return apiClient.get(`/Task/${UserId}`);
  },
  createTask(task) {
    return apiClient.post('/Task/', task);
  },
  updateTask(id, task) {
   //return apiClient.put('/Task/', {id});
   return apiClient.put(`/Task/${id}`,task);
  },
  getTaskbyId(id) {
    //return apiClient.put('/Task/', {id});
    return apiClient.get(`Task/search/${id}`);
   },
  // getAllTasks(){
  //   return apiClient.get('/GetTaskDetails');
  // }
  deleteTask(taskId) {
   return apiClient.delete(`/Task/${taskId}`);
  },
  getAllUsers() {
    console.log('Fetching users...'); // Debug log
    return apiClient.get(`/Registration/users`)
    //return axios.get('http://localhost:5083/api/Registration/users')
     // Adjust port to match your .NET backend
        .catch(error => {
            console.error('API Error:', error.response || error);
            throw error;
        });
  }
};
