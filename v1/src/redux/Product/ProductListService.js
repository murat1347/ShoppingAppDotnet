import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";

export const productListAsync = createAsyncThunk(
  "productListSlice/productListAsync",
  async (CategoryId) => {
    const response = await axios.get(
      `http://localhost:4988/api/Product`,{params:{CategoryId:CategoryId,PAGE_SIZE:10}}
    );
    return response.data;
  }
);
