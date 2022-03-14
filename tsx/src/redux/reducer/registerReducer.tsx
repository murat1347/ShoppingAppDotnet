import * as actionTypes from "../actions/actionTypes"
import initialState from "./initialState";

export default function registerReducer(state=initialState.register,action){
    switch (action.type) {
        case actionTypes.GET_REGISTER_SUCCESS:
            return action.payload
        default:
            return state;
    }
}