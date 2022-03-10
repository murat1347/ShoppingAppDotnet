import * as actionTypes from "./actionTypes";
import axios, { Axios } from "axios";
export function getProductsSuccess(products) {
  return { type: actionTypes.GET_PRODUCTS_SUCCESS, payload: products };
}

export function createProductSuccess(product) {
  return { type: actionTypes.CREATE_PRODUCT_SUCCESS, payload: product };
}

export function updateProductSuccess(product) {
  return { type: actionTypes.UPDATE_PRODUCT_SUCCESS, payload: product };
}

export function saveProductApi(product) {

  product.addedDate = "2022-03-09T07:57:25.543Z"
  product.modifiedDate = "2022-03-09T07:57:25.543Z";
  product.addedBy = 'string';
  console.log(product);
  axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*'
  return fetch("http://localhost:4988/api/Product/  " + (product.id || ""), {
    method: product.id ? "PUT" : "POST",
    headers: {
      "content-type": "application/json", "Access-Control-Allow-Headers": "*", // this will allow all CORS requests
      "Access-Control-Allow-Methods": 'OPTIONS,POST,GET' // this states the allowed methods
    },
    body: JSON.stringify(product)
  })
    .then(handleResponse)
    .catch(handleError);
}

export function saveProduct(product) {
  return function (dispatch) {
    return saveProductApi(product)
      .then(savedProduct => {
        product.id
          ? dispatch(updateProductSuccess(savedProduct))
          : dispatch(createProductSuccess(savedProduct));
      })
      .catch(error => {
        throw error;
      });
  };
}

export async function handleResponse(response) {
  if (response.ok) {
    return response.ok
  }

  const error = await response.text()
  throw new Error(error)
}

export function handleError(error) {
  console.error("Bir hata oluştu")
  throw error;
}

export function getProducts(categoryId) {
  return function (dispatch) {
    let url = "http://localhost:4988/api/Product/";
    if (categoryId) {
      url = url + "?categoryId=" + categoryId;
    }
    return fetch(url)
      .then(response => response.json())
      .then(result => dispatch(getProductsSuccess(result)));
  };
}
