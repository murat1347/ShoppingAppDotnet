import * as actionTypes from "./actionTypes";
import axios from "axios"; 


export function GET_REGISTER_SUCCESS(register) {
  return { type: actionTypes.GET_REGISTER_SUCCESS, payload: register }
}
export const getRegister = async (input) => {
  console.log( JSON.stringify(input))
  axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*'

  return axios.post("http://localhost:4988/api/Register",{
    method: "POST",
    headers: {
      "content-type": "application/json", "Access-Control-Allow-Headers": "*", // this will allow all CORS requests
      "Access-Control-Allow-Methods": 'OPTIONS,POST,GET' // this states the allowed methods
    },
    body: JSON.stringify(input)
  })
  
    
}