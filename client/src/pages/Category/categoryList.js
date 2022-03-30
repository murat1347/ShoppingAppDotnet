import React, { Component } from "react";
import { connect } from "react-redux";
import jwt_decode from 'jwt-decode'
import { bindActionCreators } from "redux";
import * as categoryActions from "../../redux/actions/categoryActions";
import { ListGroup, ListGroupItem } from "reactstrap";
// import { Badge } from "reactstrap";
//import * as productActions from "../../redux/actions/productActions";
import { Badge,Text,Link } from '@chakra-ui/react'
import Brands from "../Products/Brands";
class CategoryList extends Component {
  componentDidMount() {
    this.props.actions.getCategories();
  }

  selectCategory = category => {
    this.props.actions.changeCategory(category);
    this.props.actions.getProducts(category.id)
  };
  // componentDidMount() {
  //   const token = localStorage.token
  //   if(token){
  //     const decoded = jwt_decode(token)
  //     console.log(decoded)
  //   this.setState(decoded)
  //   }else{
  //     this.props.history.push('/Login')
  //   }
  // }
  render() {
    return (
      <div>
        <h3>
          {/* <Badge color="warning">Categories</Badge> */}
        </h3>
        {this.props.categories?.map(category => (
         
          <Badge ml='1' fontSize='2em' colorScheme='green'>
         <Link href="/category">{category.name}</Link><br/>
          </Badge>
       ))}
<Brands></Brands>
        {/* <ListGroup>
          {this.props.categories.map(category => (
            <ListGroupItem
            //   active={category.id === this.props.currentCategory.id}
            //   onClick={() => this.selectCategory(category)}
            //   key={category.id}
            // >
              {category.name}
            </ListGroupItem>
          ))}
        </ListGroup> */}
      </div>
    );
  }
}

function mapStateToProps(state) {
  return {
   // currentCategory: state.changeCategoryReducer,
    categories: state.categoryReducer
  };
}

function mapDispatchToProps(dispatch) {
  return {
    actions: {
      getCategories: bindActionCreators(
        categoryActions.getCategories,
        dispatch
      ),
    //   changeCategory: bindActionCreators(
    //     categoryActions.changeCategory,
    //     dispatch
    //   ),
    //   getProducts: bindActionCreators(productActions.getProducts, dispatch)
}   // 
  };
}

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(CategoryList);
