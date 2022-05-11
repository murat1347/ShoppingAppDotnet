import CategorySlice from "./Category/CategorySlice";
import { configureStore } from "@reduxjs/toolkit";
import addProductSlice from "./Product/addProductSlice";
import productListSlice from "./Product/productListSlice";
import CategoryFilterSlice  from "./Category/CategoryFilterReducer";

export const store = configureStore({
  reducer: {
    addProductSlice: addProductSlice,
    CategorySlice : CategorySlice,
    productListSlice : productListSlice,
    CategoryFilterSlice : CategoryFilterSlice,
  },
});