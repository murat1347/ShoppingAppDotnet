import React, { useState, useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { Grid, Box } from "@chakra-ui/react";
import Card from "./Card";
import { veri } from "../redux/Product/productListSlice";
import Loading from "./Loading";

const ProductList = () => {
  const state = useSelector((state) => state);
  const productsState = useSelector((state) => state.productListSlice);
  const filteredCategory = useSelector((state) => state.filteredCategory);
  const priceFilter = useSelector((state) => state.priceFilter);
  const productListSlice = useSelector((state) => state.productListSlice)
  const dispatch = useDispatch();
  const [columnCount, setColumnCount] = useState(3);

  const layoutClassName = `row row-cols-${columnCount}`;
  const [currentPage, setCurrentPage] = useState(1);
  const phonesPerPage = 11;
  const indexOfLastPhone = currentPage * phonesPerPage;
  const indexOfFirstPhone = indexOfLastPhone - phonesPerPage;
  //const currentPhones = phones.slice(indexOfFirstPhone, indexOfLastPhone);
  const pageCount = Math.ceil(productListSlice.items.length / phonesPerPage);

  let pageNumberArray = [];
  for (let i = 0; i < pageCount; i++) {
    pageNumberArray[i] = (
      <li className={currentPage == i + 1 ? "page-item active" : "page-item"}>
        <button className="page-link" onClick={() => setCurrentPage(i + 1)}>
          {i + 1}
        </button>
      </li>
    );
  }
  useEffect(() => {
    dispatch(veri(currentPage));
  }
  );


  if(productListSlice.status =="loading"){
    <Loading/>
  }
  return (
    <div style={{ width: "75%" }}>

      <div>
        <div className={layoutClassName}>
          {productListSlice.status == "succeeded" ? (
            <>
              <Grid templateColumns="repeat(4, 1fr)" gap={4}>

                <React.Fragment>

                  {productListSlice.items.map((item) => (
                    <Box w="100%" key={item.id}>
                      <Card item={item} />
                    </Box>
                  ))}
                </React.Fragment>

              </Grid>
            </>
          ) : null}
        </div>
      </div>
      <nav aria-label="Page navigation example" style={{ marginTop: "50px" }}>
        <ul className="pagination justify-content-end">
          <li
            className={currentPage == 1 ? "page-item disabled" : "page-item "}
          >
            <button
              className="page-link"
              tabIndex="-1"
              onClick={() => setCurrentPage(currentPage - 1)}
            >
              Previous
            </button>
          </li>
          {pageNumberArray.map((li) => li)}
          <li
            className={
              currentPage > pageCount || productListSlice.items.length <11? "page-item disabled" : "page-item "
            }
          >
            <button
              className="page-link"
              onClick={() => setCurrentPage(currentPage + 1)}
            >
              Next
            </button>
          </li>
        </ul>
      </nav>
    </div>
  );
};

export default ProductList;
