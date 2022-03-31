const initialState = {
  filteredCategory: [],
};

export const categoryFilterReducer = (state = initialState, action) => {
  if (action.type === "CATEGORY_FILTER_UPDATE") {
    return {
      filteredCategory: action.payload,
    };
  } else {
    return state;
  }
};
