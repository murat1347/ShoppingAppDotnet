import {
    BRANDS_FETCH_START,
    BRANDS_FETCH_SUCCESS,
    BRANDS_FETCH_FAIL,
    CATEGORY_FILTER_UPDATE
  } from "./actionTypes";

  import axios from "axios";
  
  export const GetBrands = (checkedBrands) => dispatch=> {
    
    axios
      .get("https://localhost:5001/api/v1/Product",{params:{version:1,CategoryId:checkedBrands
      }})
      .then((res) => dispatch({ type: CATEGORY_FILTER_UPDATE, payload: res.data }))
     
  };
  