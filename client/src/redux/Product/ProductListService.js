import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";


export const productListAsync = createAsyncThunk(
  "productListSlice/productListAsync",
  async (data) => {
    console.log(data)
    const response = await axios.get(
      `http://localhost:4988/api/Product`,{params:{CategoryId:data[0],sortBy:data[1],PAGE_SIZE:10}}
    );
    return response.data;
  }
);
export const productListAsync2 = createAsyncThunk(
  "productListSlice/productListAsync",
  async (CategoryId) => {
    const response = await axios.get(
      `http://localhost:4988/api/Product`,{params:{sortBy:CategoryId,PAGE_SIZE:10}}
    );
    return response.data;
  }
);