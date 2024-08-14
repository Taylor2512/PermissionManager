import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:7280/api', // Cambia esto si es necesario
});

export default api;
