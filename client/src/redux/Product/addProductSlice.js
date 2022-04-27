import { createSlice } from "@reduxjs/toolkit";
import { addProductAsync } from "./addProductService";
const initialState = {
    items: [],
    error: null,
    status: "idle",
  };
  export const addProductSlice = createSlice({
    name: "addProductSlice",
    initialState,
    reducers: {},
    extraReducers: {
    
      [addProductAsync.pending]: (state, action) => {
        state.status = "loading";
      },
      [addProductAsync.fulfilled]: (state, action) => {
        state.items.push(action.payload);
        state.status = "succeeded";
      },
      [addProductAsync.rejected]: (state, action) => {
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
  export default addProductSlice.reducer;