import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";

export const categoryFilterAsync = createAsyncThunk(
  "CategorySlice/categoryFilterAsync",
  async (checkedBrands) => {
    const response = await axios.get
        ("http://localhost:4988/api/Product",{params:{CategoryId:2,PAGE_SIZE:10}}        
    );
    return response.data;
  }
);
