import * as actionTypes from "./actionTypes";
import { Axios } from "axios";

export function getCategoriesSuccess(categories){
    return {type: actionTypes.GET_CATEGORIES_SUCCESS, payload: categories}
}

export function getCategories() {
  return function(dispatch) {
    let url = "http://localhost:4988/api/Category";
    return fetch(url)
      .then(response => response.json())
      .then(result => dispatch(getCategoriesSuccess(result)));
  };
}
export function changeCategory(category) {
  return { type: actionTypes.CHANGE_CATEGORY, payload: category };
}
