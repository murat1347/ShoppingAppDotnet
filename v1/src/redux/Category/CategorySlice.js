import { createSlice } from "@reduxjs/toolkit";
import { categoryAsync } from "./CategoryService";
const initialState = {
    items: [],
    error: null,
    status: "idle",
  };
  export const CategorySlice = createSlice({
    name: "CategorySlice",
    initialState,
    reducers: {},
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
  
//   export const selectTodos = (state) => state.todos.items;
//   export const activeFilter = (state) => state.todos.activeFilter;
//   export const selectFilteredTodos = (state) => {
//     if (state.todos.activeFilter === "all") {
//       return state.todos.items;
//     }
//     return state.todos.items.filter((todo) =>
//       state.todos.activeFilter === "active"
//         ? todo.completed === false
//         : todo.completed === true
//     );
//   };
//   export const { changeActiveFilter, clearCompleted } = todosSlice.actions;
  export default CategorySlice.reducer;