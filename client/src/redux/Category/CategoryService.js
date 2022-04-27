import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";

export const categoryAsync = createAsyncThunk(
  "CategorySlice/categoryAsync",
  async () => {
    const response = await axios.get(
      `http://localhost:4988/api/Category`
    );
    return response.data;
  }
);
