import React, { useEffect, useState } from "react";
import { useSelector, useDispatch } from "react-redux";
import { getCategory } from "../redux/actions/categoriesActions";
import { getProducts } from "../redux/actions/productActions";
import {GetBrands} from "../redux/actions/categoryFilterActions"
import CategorySlice from "../redux/Category/CategorySlice";
import productListSlice from "../redux/Product/productListSlice";
import { categoryAsync } from "../redux/Category/CategoryService";
import { categoryFilterAsync } from "../redux/Category/CategoryFilterService";
import { productListAsync } from "../redux/Product/ProductListService";


function Brands () {
  const state = useSelector((state) => state);
  const dispatch = useDispatch();
  const CategorySlice = useSelector((state) => state.CategorySlice)
  const [checkedBrands, setCheckedBrands] = useState([]);
  const productListSlice= useSelector((state)=> state.productListSlice)
  const handleCheck = (e) => {
    if (e.target.checked) {
      if (!checkedBrands.includes(e.target.id)) {
        setCheckedBrands(e.target.id);
        }
    }
    if (!e.target.checked) {
      const filteredArr = [];
      setCheckedBrands(filteredArr);
    }
  };
  useEffect(() => {
    dispatch(productListAsync(checkedBrands));
    dispatch(categoryAsync());
    // dispatch(GetBrands(checkedBrands,2))
    // dispatch({ type: "changeCategoryFilter", payload: checkedBrands });
  }, [checkedBrands]);
  return (
    <div className="mb-5">
      <div className="card">
        <div className="card-header">PRODUCTS</div>
        <ul className="list-group list-group-flush">
    
          {CategorySlice.status=="succeeded" && productListSlice.status=="succeeded" ? (
            <>
              {CategorySlice.items[0].map((brand) => {
              
                const brandHasPhones = productListSlice.items.filter((phone) => {
                  if (phone.id === brand.id) {
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
                      {brand.name[0].toUpperCase() + brand.name.substring(1)} 
                  
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
export default Brands;