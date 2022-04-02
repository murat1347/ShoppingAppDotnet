import React, { useEffect, useState } from "react";
import { useSelector, useDispatch } from "react-redux";
import { getCategory } from "../redux/actions/categoriesActions";
import { getProducts } from "../redux/actions/productActions";
import {getBrands} from "../redux/actions/brandActions"
import { connect } from "react-redux";

function Brands () {
  const state = useSelector((state) => state);
  const dispatch = useDispatch();
  const categoryState = state.category;
  const productsState = state.product;
  const [checkedBrands, setCheckedBrands] = useState([]);
  //console.log(checkedBrands);
  const filteredCategory= state.filteredCategory
  const handleCheck = (e) => {
    if (e.target.checked) {
      if (!checkedBrands.includes(e.target.id)) {
        setCheckedBrands([...checkedBrands, e.target.id]);
         dispatch(getBrands)
        {console.log(filteredCategory)}
        
      }
    }
    if (!e.target.checked) {
      const filteredArr = checkedBrands.filter(function (item) {
        return item !== e.target.id;
      });
      
      setCheckedBrands(filteredArr);
      

    }
  };
  useEffect(() => {
    dispatch(getCategory);
    dispatch(getProducts);
    dispatch(getBrands)
    // dispatch({ type: "BRANDS_FETCH_SUCCESS", payload: checkedBrands });
  }, []);
  return (
    <div className="mb-5">
      <div className="card">
        <div className="card-header">PRODUCTS</div>
        <ul className="list-group list-group-flush">
          {categoryState.success && productsState.success ? (
            <>
           
              {categoryState.category.map((brand) => {
                const brandHasPhones = productsState.products.products.filter((phone) => {
                  if (phone.brandId === brand.id) {
                    return true;
                  }
                });
                return (
                  <li className="list-group-item" key={brand.id}>
                    <input
                      className="form-check-input"
                      type="checkbox"
                      id={brand.id}
                      onChange={(e) => {
                        handleCheck(e);
                      }}
                    />

                    <label
                      className="form-check-label"
                      htmlFor="flexCheckDefault"
                      style={{
                        display: "inline-block",
                        marginLeft: "1rem",
                      }}
                    > 
                      {brand.name[0].toUpperCase() + brand.name.substring(1)} (
                      {brandHasPhones.length})
                    </label>
                  </li>
                );
              })}
            </>
          ) : null}
        </ul>
      </div>
    </div>
  );
  
};
const mapDispatchToProps = (dispatch) => {
  return{
    getBrands: (arg) => {
       dispatch(getBrands(arg))
    }
  }}


export default connect( mapDispatchToProps, getBrands )(Brands);