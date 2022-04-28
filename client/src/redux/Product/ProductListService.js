import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";


export const productListAsync = createAsyncThunk(
  "productListSlice/productListAsync",
  async (data) => {
    const response = await axios.get(
      `http://localhost:4988/api/Product`,{params:{CategoryId:data[0],sortBy:data[1],page:data[2],PAGE_SIZE:12}}
    );
    return response.data;
  }
);