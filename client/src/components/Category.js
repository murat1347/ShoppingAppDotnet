import React, { useEffect, useState } from "react";
import { useSelector, useDispatch } from "react-redux";
import { categoryAsync } from "../redux/Category/CategoryService";
import { productListAsync } from "../redux/Product/ProductListService";
import { useSearchParams,useLocation,useParams,} from "react-router-dom";
import { useUrlSearchParams } from "use-url-search-params";

function Brands() {
  const [searchParams, setSearchParams] = useSearchParams();
  const [priceFilter, setPriceFilter] = useState("");
  const dispatch = useDispatch();
  const CategorySlice = useSelector((state) => state.CategorySlice)
  const [checkedBrands, setCheckedBrands] = useState([]);
  const productListSlice = useSelector((state) => state.productListSlice)
  const datam = useSelector((state) => state.productListSlice.veri)

  console.log(useLocation().search)
  const [params, setParams] = useUrlSearchParams({ CategoryId: "" ,sortBy:""});
  var CategoryId=searchParams.get('CategoryId')
  var sortBy = searchParams.get('sortBy')

  const handlePriceCheck = (e)=>{
    setPriceFilter(e.target.id); setParams({ sortBy: e.target.id })
  }
  
  const handleCheck = (e) => {
    if (e.target.checked) {
      if (!checkedBrands.includes(e.target.id)) {
        setCheckedBrands(e.target.id);
        setParams({ CategoryId: e.target.id })
      }
    }
    if (!e.target.checked) {
      const filteredArr = [];
      setCheckedBrands(filteredArr);
    }
  };
useEffect(()=>
{
  if(CategoryId && setCheckedBrands(CategoryId))
  if(sortBy && setPriceFilter(sortBy)){}
} ,[])
  useEffect(() => {

    const response = dispatch(productListAsync([checkedBrands, priceFilter == '' ? 0 : priceFilter, datam]));
    dispatch(categoryAsync());
  
  }, [checkedBrands, priceFilter, datam,params.CategoryId,CategoryId]);
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
                        }} />

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
                  document.getElementById("asc").checked = false;
                  document.getElementById("desc").checked = false;
                }}
              >
                Remove Filter
              </span>
            ) : null}
          </div>
          <ul
            className="list-group list-group-flush"
            onChange={(e) => handlePriceCheck(e)}
    
          >
            {CategorySlice && productListSlice ? (
              <>
                <li className="list-group-item">
                  <input
                    className="form-check-input"
                    type="radio"
                    name="flexRadioDefault"
                    id="asc" />
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
                    id="desc" />
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