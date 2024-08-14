import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:5000/api', // Cambia esto si es necesario
});

export default api;
