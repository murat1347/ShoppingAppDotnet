import { chartReducer } from "./reducers/chartReducer";
import { priceFilterReducer } from "./reducers/priceFilterReducer";
import { createStore, combineReducers, applyMiddleware } from "redux";
import { productReducer } from "./reducers/productReducer";
import { categoriesReducer } from "./reducers/categoriesReducer";
import logger from "redux-logger";
import thunk from "redux-thunk";
import { categoryFilterReducer } from "./reducers/categoryFilterReducer";

const rootReducer = combineReducers({
  product: productReducer,
  category: categoriesReducer,
  chart: chartReducer,
  filteredCategory: categoryFilterReducer,
  priceFilter: priceFilterReducer,
});

const store = createStore(rootReducer, applyMiddleware(logger, thunk));
export default store;
