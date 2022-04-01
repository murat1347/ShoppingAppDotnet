import {
  CATEGORY_FETCH_START,
  CATEGORY_FETCH_SUCCESS,
  CATEGORY_FETCH_FAIL,
} from "../actions/actionTypes";

const initialState = {
  category: [],
  start: false,
  success: false,
  err: null,
};

export const categoriesReducer = (state = initialState, action) => {
  switch (action.type) {
    case CATEGORY_FETCH_START:
      return {
        ...state,
        start: true,
      };
    case CATEGORY_FETCH_SUCCESS:
      return {
        ...state,
        start: false,
        success: true,
        category: action.payload,
      };
    case CATEGORY_FETCH_FAIL:
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
