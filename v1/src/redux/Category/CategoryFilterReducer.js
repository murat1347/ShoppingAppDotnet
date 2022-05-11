import { createSlice } from "@reduxjs/toolkit";
import { categoryAsync } from "./CategoryService";
const initialState = {
    items: [],
    error: null,
    status: "idle",
  };
  export const CategoryFilterSlice = createSlice({
    name: "CategoryFilterSlice",
    initialState,
    reducers: {
        changeCategoryFilter: (state, action) => {
            state.changeCategoryFilter = action.payload;
          },
    },
    extraReducers: {
    
      [categoryAsync.pending]: (state, action) => {
        state.status = "loading";
      },
      [categoryAsync.fulfilled]: (state, action) => {
        state.items.push(action.payload);
        state.status = "succeeded";
      },
      [categoryAsync.rejected]: (state, action) => {
        state.status = "failed";
        state.error = action.error;     
      },
      
    },
  });

  export default CategoryFilterSlice.reducer;