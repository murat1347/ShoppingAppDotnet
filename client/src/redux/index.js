

import { configureStore } from "@reduxjs/toolkit";
import addProductSlice from "./addProductSlice";

export const store = configureStore({
  reducer: {
    addProductSlice: addProductSlice,
  },
});