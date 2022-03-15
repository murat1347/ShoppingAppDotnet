import axios from 'axios';

const interceptor = axios.interceptors.response.use(
  response => response,
  error => {
      // Reject promise if usual error
      if (error.response.status !== 401) {
          return Promise.reject(error);
      }
      
      /* 
       * When response code is 401, try to refresh the token.
       * Eject the interceptor so it doesn't loop in case
       * token refresh causes the 401 response
       */
      axios.interceptors.response.reject(interceptor);

      return axios.post('/api/Account', {
          'refleshToken': this._getToken('refleshToken')
      }).then(response => {
          localStorage.setItem("token")
          error.response.config.headers['Authorization'] = 'Bearer ' + response.data.token;
          return axios(error.response.config);
      }).catch(error => {
        localStorage.removeItem("token");
          this.router.push('/login');
          return Promise.reject(error);
      }).finally(interceptor);
  }
);

export const fetchProductList = async ({ pageParam = 0 }) => {
  const { data } = await axios.get(
    `${process.env.REACT_APP_BASE_ENDPOINT}/product?page=${pageParam}`
  );

  return data;
};

export const fetchProduct = async (id) => {
  const { data } = await axios.get(
    `${process.env.REACT_APP_BASE_ENDPOINT}/product/${id}`
  );

  return data;
};

export const fetchRegister = async (input) => {
  const { data } = await axios.post(
    `${process.env.REACT_APP_BASE_ENDPOINT}/auth/register`,
    input
  );

  return data;
};

export const fetchMe = async () => {
  const { data } = await axios.get(
   "http://localhost:4988/api/Account"
  );

  return data;
};

export const fetchLogout = async () => {
  const { data } = await axios.post(
    "http://localhost:4988/api/Logout",
    {
      refresh_token: localStorage.getItem('refleshToken'),
    }
  );

  return data;
};

export const fetchLogin = async (input) => {
  console.log(input);
  const { data } = await axios.post(
    "http://localhost:4988/api/Account",
    input
  );
  return data;
};



export const postOrder = async (input) => {
  const { data } = await axios.post(
    `${process.env.REACT_APP_BASE_ENDPOINT}/order`,
    input
  );

  return data;
};

export const fetchOrders = async () => {
  const { data } = await axios.get(
    `${process.env.REACT_APP_BASE_ENDPOINT}/order`
  );

  return data;
};

export const deleteProduct = async (id) => {
  const { data } = await axios.delete(
    `${process.env.REACT_APP_BASE_ENDPOINT}/product/${id}`
  );

  return data;
};

export const updateProduct = async (input, id) => {
  const { data } = await axios.put(
    `${process.env.REACT_APP_BASE_ENDPOINT}/product/${id}`,
    input
  );

  return data;
};

export const postProduct = async (input) => {
  const { data } = await axios.post(
    `${process.env.REACT_APP_BASE_ENDPOINT}/product`,
    input
  );

  return data;
};

const logout = () => {
  localStorage.removeItem("user");
};

const getCurrentUser = () => {
  return JSON.parse(localStorage.getItem("user"));
};