import {
  PRODUCT_FETCH_START,
  PRODUCT_FETCH_SUCCESS,
  PRODUCT_FETCH_FAIL,
} from "./actionTypes";
import { productsURL } from "../urls";
import axios from "axios";

export const getProducts = (dispatch) => {
  dispatch({ type: PRODUCT_FETCH_START });
  axios
    .get(productsURL)
    .then((res) => dispatch({ type: PRODUCT_FETCH_SUCCESS, payload: res.data }))
    .catch((err) => dispatch({ type: PRODUCT_FETCH_FAIL, payload: err }));
};
