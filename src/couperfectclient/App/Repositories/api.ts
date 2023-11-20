import axios from 'axios';

const yourIp = '';

const api = axios.create({
  baseURL: `http://${yourIp}:5072/`
});

export default api;