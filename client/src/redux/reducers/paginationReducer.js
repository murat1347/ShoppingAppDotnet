
export default (state = {}, action) => {
    switch (action.type) {
      case FETCH_POSTS:
        return action.payload.data.pagination
      default:
        return state
    }
  }