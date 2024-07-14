// axiosConfig.ts
import axios, {AxiosInstance} from 'axios';

function getToken(): string | null {
  return localStorage.getItem('userToken');
}

const axiosInstance: AxiosInstance = axios.create({
  baseURL: 'https://localhost:44496',
});

axiosInstance.interceptors.request.use(
  (config) => {
    if (config.headers) {
      if (config.headers.noAuth) {
        delete config.headers.noAuth;
      }
      const token = getToken();
      if (token) {
        config.headers['Authorization'] = `Bearer ${token}`;
      }
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default axiosInstance;
