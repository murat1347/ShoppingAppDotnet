import React from "react";
import LeftSide from "./LeftSide";
import ProductList from "./ProductList";
const Home = () => {
  return (
    <div
      className="container"
      style={{
        display: "flex",
        justifyContent: "space-between",
        marginTop: "10px",
      }}
    >
      <LeftSide />
      <ProductList />
    </div>
  );
};

export default Home;
