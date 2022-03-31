import {combineReducers} from "redux"
import {categoriesReducer} from "./reducers/categoriesReducer"
import {productReducer} from "./reducers/productReducer"
import {priceFilterReducer} from "./reducers/priceFilterReducer"
import {categoryFilterReducer} from "./reducers/categoryFilterReducer"
const rootReducer = combineReducers({
    product:productReducer,
    priceFilter: priceFilterReducer,
    category:categoriesReducer,
    filteredCategory: categoryFilterReducer,
})

export default rootReducer;