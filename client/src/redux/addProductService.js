import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";


const config = {

  headers: { Authorization: `Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJtYWlsQGRvZ3VrYW5oYW4uY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6Im1haWxAZG9ndWthbmhhbi5jb20iLCJleHAiOjE2NTEwNDUxOTB9.bt24lIn9qM73XcCUJfm2gREt9NbqGBWEWC5NuqhqP0wLbFs5lAlktYmxVkX2BwK28Z8T4W4mOQizLLq-4atiPg` }
};
export const addProductAsync = createAsyncThunk(
  "products/addProductAsync",
  async (data) => {
    const response = await axios.post(
      `http://localhost:4988/api/Product`,
      data,
      config
    );
    return response.data;
  }
);
