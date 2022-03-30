import {combineReducers} from "redux"
import categoryReducer from "../redux/reducers/categoryReducer"
import changeCategoryReducer from "./reducers/changeCategoryReducer";
import { brandsReducer } from "./reducers/brandReducer";
import { phonesReducer } from "./reducers/phonesReducer";
const rootReducer = combineReducers({
    categoryReducer,
    changeCategoryReducer,
    brandsReducer,
    phonesReducer
})

export default rootReducer;