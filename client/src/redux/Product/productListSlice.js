import { createSlice } from "@reduxjs/toolkit";
import { productListAsync } from "./ProductListService";
const initialState = {
  items: [],
  error: null,
  status: "idle",
  veri:[]
};
export const productListSlice = createSlice({
  name: "productListSlice",
  initialState,
  reducers: {
    setItems: (state, items) => {
      state.items = items;
    },
    veri: (state, items) => {
      state.veri=items.payload
    },
  },
  extraReducers: {

    [productListAsync.pending]: (state, action) => {
      state.status = "loading";
    },
    [productListAsync.fulfilled]: (state, action) => {
      state.items = action.payload;
      state.status = "succeeded";
    },
    [productListAsync.rejected]: (state, action) => {
      state.status = "failed";
      state.error = action.error;
    },

  },
});

export const { setItems,veri } = productListSlice.actions;
export default productListSlice.reducer;