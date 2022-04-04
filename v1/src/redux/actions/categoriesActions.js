import {
  CATEGORY_FETCH_START,
  CATEGORY_FETCH_SUCCESS,
  CATEGORY_FETCH_FAIL,
} from "./actionTypes";
import { categoriesURL } from "../urls";
import axios from "axios";

export const getCategory = (dispatch) => {
  dispatch({ type: CATEGORY_FETCH_START });
  axios
    .get(categoriesURL)
    .then((res) => dispatch({ type: CATEGORY_FETCH_SUCCESS, payload: res.data }))
    .catch((err) => dispatch({ type: CATEGORY_FETCH_FAIL, payload: err }));
};