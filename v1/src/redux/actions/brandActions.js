import {
    BRANDS_FETCH_START,
    BRANDS_FETCH_SUCCESS,
    BRANDS_FETCH_FAIL,
    CATEGORY_FILTER_UPDATE
  } from "../actions/actionTypes";

  import axios from "axios";
  
  export const getBrands = () => {
    return (dispatch) => {
    dispatch({ type: BRANDS_FETCH_START });
    axios
      .get("https://localhost:5001/api/v1/Product",{params:{version:1,CategoryId:2
      }})
      .then((res) => dispatch({ type: CATEGORY_FILTER_UPDATE, payload: res.payload.products }))
      .then((res) =>{
        console.log(res.payload.products)})
      .catch((err) => dispatch({ type: BRANDS_FETCH_FAIL, payload: err }));
  }};
  