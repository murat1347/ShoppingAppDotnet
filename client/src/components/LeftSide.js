import React from "react";
import Category from "./Category";
import PriceFilter from "./PriceFilter";

const LeftSide = () => {
  return (
    <div style={{ width: "20%", backgroundColor:"#E2E8F0" }}>
      <Category />
      {/* <PriceFilter /> */}
    </div>
  );
};

export default LeftSide;
