import axios, {AxiosHeaders, AxiosInstance} from 'axios';
import Store from "../DataStore";

function getToken(): string | null {
  const auth = Store.getState().auth;
  return auth ? auth.authToken : null;
}

const axiosInstance: AxiosInstance = axios.create({
  baseURL: 'https://localhost:44496',
});

axiosInstance.interceptors.request.use(
  (config) => {
    console.log(config)
    if (!config.headers) {
      config.headers = new AxiosHeaders();
    }
    if (config.headers.noAuth) {
      delete config.headers.noAuth;
    }
    else {
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
