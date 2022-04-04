const initialState = {
  filteredCategory: "",
};

export const categoryFilterReducer = (state = initialState, action) => {
  if (action.type === "CATEGORY_FILTER_UPDATE") {
    return {success: true,
      filteredCategory: action.payload
    };}
  else{
  return state;
}
};
