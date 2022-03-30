import {
  PHONES_FETCH_START,
  PHONES_FETCH_SUCCESS,
  PHONES_FETCH_FAIL,
} from "../actions/actionTypes";

import axios from "axios";

export const getPhones = (dispatch) => {
  dispatch({ type: PHONES_FETCH_START });
  axios
    .get("http://localhost:4988/api/product")
    .then((res) => dispatch({ type: PHONES_FETCH_SUCCESS, payload: res.data }))
    .catch((err) => dispatch({ type: PHONES_FETCH_FAIL, payload: err }));
};
