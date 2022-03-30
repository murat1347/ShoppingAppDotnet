import React, { useEffect, useState } from "react";
import { useSelector, useDispatch } from "react-redux";
import { getPhones } from "../../redux/actions/phonesActions";
import { getBrands } from "../../redux/actions/brandActions";
const Brands = () => {
  const state = useSelector((state) => state);
  const dispatch = useDispatch();
  const brandsState = state.brandsReducer.brands;
  const phonesState = state.phonesReducer.phones;
  const [checkedBrands, setCheckedBrands] = useState([]);
  //console.log(checkedBrands);

  const handleCheck = (e) => {
    if (e.target.checked) {
      if (!checkedBrands.includes(e.target.id)) {
        setCheckedBrands([...checkedBrands, e.target.id]);
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
    dispatch(getBrands);
    dispatch(getPhones);
    dispatch({ type: "BRAND_FILTER_UPDATE", payload: checkedBrands });
  }, [checkedBrands]);

  return (
    <div className="mb-5">
      <div className="card">
        <div className="card-header">Brands</div>
        <ul className="list-group list-group-flush">
        
          {brandsState ? (
            <>
               
              {brandsState.map((brand) => {
               
               
                
                const brandHasPhones = phonesState.filter((phone) => 
                
              {
                 
                  if (phone.categoryId === 1) {
                
                     console.log(phone.name)
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
                      {brand.name.toUpperCase()} (
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

export default Brands;