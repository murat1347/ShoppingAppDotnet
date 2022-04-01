import {
  PRODUCT_FETCH_START,
  PRODUCT_FETCH_SUCCESS,
  PRODUCT_FETCH_FAIL,
} from "../actions/actionTypes";

const initialState = {
  products: [],
  start: false,
  success: false,
  err: null,
};

export const productReducer = (state = initialState, action) => {
  switch (action.type) {
    case PRODUCT_FETCH_START:
      return {
        ...state,
        start: true,
      };
    case PRODUCT_FETCH_SUCCESS:
      return {
        ...state,
        start: false,
        success: true,
        products: action.payload,
      };
    case PRODUCT_FETCH_FAIL:
      return {
        ...state,
        start: false,
        success: false,
        err: action.payload,
      };
    default:
      return state;
  }
};
