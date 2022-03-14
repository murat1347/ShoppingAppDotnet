import axios from 'axios';
import { createAsyncThunk, createSlice, isAnyOf } from "@reduxjs/toolkit";

// axios.interceptors.request.use(
//   function (config) {
//     const { origin } = new URL(config.url);

//     const allowedOrigins = [process.env.REACT_APP_BASE_ENDPOINT];
//     const token = localStorage.getItem('access-token');
//     if (allowedOrigins.includes(origin)) {
//       config.headers.authorization = token;
//     }

  //   return config;
  // },
  
  function (error) {
    // Do something with request error
    return Promise.reject(error);
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
  console.log(input);
  const { data } = await axios.post(
    'http://localhost:4988/api/Register',
    input
  );

  return data;
};

export const fetchLogout = async () => {
  const { data } = await axios.post(
    `${process.env.REACT_APP_BASE_ENDPOINT}/api//logout`,
    {
      refresh_token: localStorage.getItem('refresh-token'),
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
// export const fetchCurrentUser = createAsyncThunk<User>(
//   'account/fetchCurrentUser',
//   async (_, thunkAPI) => {
//       thunkAPI.dispatch(setUser(JSON.parse(localStorage.getItem('user'))));
//       try {
//           const userDto = await agent.Account.currentUser();
//           const {basket, ...user} = userDto;
         
//           localStorage.setItem('user', JSON.stringify(user));
//           return user;
//       } catch (error) {
//           return thunkAPI.rejectWithValue({error: error.data});
//       }
//   },
//   {
//       condition: () => {
//           if (!localStorage.getItem('user')) return false;
//       }
//   }
// )
export const postProduct = async (input) => {
  const { data } = await axios.post(
    `${process.env.REACT_APP_BASE_ENDPOINT}/product`,
    input
  );

  return data;
};

export const accountSlice = createSlice({
  name: 'account',
  initialState,
  reducers: {
      signOut: (state) => {
          state.user = null;
          localStorage.removeItem('user');
          history.push('/');
      },
      setUser: (state, action) => {
          let claims = JSON.parse(Buffer.from(action.payload.token.split('.')[1]));
          let roles = claims['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
          state.user = {...action.payload, roles: typeof(roles) === 'string' ? [roles] : roles};
      }
}
});
