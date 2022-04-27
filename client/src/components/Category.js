import React, { useEffect, useState } from "react";
import { useSelector, useDispatch } from "react-redux";
import { getCategory } from "../redux/actions/categoriesActions";
import { getProducts } from "../redux/actions/productActions";
import {GetBrands} from "../redux/actions/categoryFilterActions"
import CategorySlice from "../redux/Category/CategorySlice";
import productListSlice, { setItems ,veri} from "../redux/Product/productListSlice";
import { categoryAsync } from "../redux/Category/CategoryService";
import { productListAsync } from "../redux/Product/ProductListService";


function Brands () {
  const state = useSelector((state) => state);
  const [priceFilter, setPriceFilter] = useState("");
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
    let response = dispatch(productListAsync([checkedBrands,priceFilter=='' ? 0 : priceFilter]));
    dispatch(categoryAsync());
  }, [checkedBrands,priceFilter]);
  return (
    <><div className="mb-5">
      <div className="card">
        <div className="card-header">PRODUCTS</div>
        <ul className="list-group list-group-flush">

          {CategorySlice.status == "succeeded" && productListSlice.status == "succeeded" ? (
            <>
              {CategorySlice.items.map((brand) => {

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
                      } } />

                    <label
                      className="form-check-label"
                      htmlFor="flexCheckDefault"
                      style={{
                        display: "inline-block",
                        marginLeft: "1rem",
                      }}
                    >
                      {brand.name.toUpperCase()}

                    </label>
                  </li>
                );
              })}
            </>
          ) : null}
        </ul>
      </div>
    </div><div>
        <div className="card">
          <div
            className="card-header"
            style={{
              display: "flex",
              alignItems: "center",
              justifyContent: "space-between",
            }}
          >
            <span>Price Sort</span>{" "}
            {priceFilter != "" ? (
              <span
                style={{
                  fontSize: "12px",
                  textDecoration: "underline",
                  cursor: "pointer",
                }}
                onClick={() => {
                  setPriceFilter("");
                  document.getElementById(1).checked = false;
                  document.getElementById(2).checked = false;
                } }
              >
                Remove Filter
              </span>
            ) : null}
          </div>
          <ul
            className="list-group list-group-flush"
            onChange={(e) => setPriceFilter(e.target.id)}
          >
            {CategorySlice && productListSlice ? (
              <>
                <li className="list-group-item">
                  <input
                    className="form-check-input"
                    type="radio"
                    name="flexRadioDefault"
                    id="1" />
                  <label
                    className="form-check-label"
                    htmlFor="low"
                    style={{
                      display: "inline-block",
                      marginLeft: "1rem",
                    }}
                  >
                    Low to High
                  </label>
                </li>
                <li className="list-group-item">
                  <input
                    className="form-check-input"
                    type="radio"
                    name="flexRadioDefault"
                    id="2" />
                  <label
                    className="form-check-label"
                    htmlFor="high"
                    style={{
                      display: "inline-block",
                      marginLeft: "1rem",
                    }}
                  >
                    High to Low
                  </label>
                </li>
              </>
            ) : null}
          </ul>
        </div>
      </div></>
  );
  
};
export default Brands;