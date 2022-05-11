import React, { useState, useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { getProducts } from "../redux/actions/productActions";
import { Grid, Box } from "@chakra-ui/react";
import Card from "./Card";
import { GetBrands } from "../redux/actions/categoryFilterActions";
import productListSlice from "../redux/Product/productListSlice";
import { productListAsync } from "../redux/Product/ProductListService";
import { CategoryFilterSlice } from "../redux/Category/CategoryFilterReducer";
import { categoryFilterAsync } from "../redux/Category/CategoryFilterService";
import CategorySlice from "../redux/Category/CategorySlice";

const ProductList = () => {
  const state = useSelector((state) => state);
  //const productsState = useSelector((state) => state.product);
  const categoryFilterState= useSelector((state)=>state.CategoryFilterSlice)
  const productsState = useSelector((state) => state.productListSlice);
  const filteredCategory = useSelector((state) => state.filteredCategory);
  const priceFilter = useSelector((state) => state.priceFilter);
  const productListSlice = useSelector((state) => state.productListSlice)
  const dispatch = useDispatch();
  const [columnCount, setColumnCount] = useState(3);

  const layoutClassName = `row row-cols-${columnCount}`;
  let phones;
  if (state.productListSlice.items[1]) {
    phones = state.productListSlice.items.filter((phone) => {
      console.log(state.productListSlice.items[0])
  if (state.productListSlice.items[0].id==(phone.id)) {
    return true;

  }
    });
  } else {
  
   
 phones = productListSlice.items[0];
  }
  // if (priceFilter.priceFilter == "low") {
  //   phones = phones.sort((a, b) => (a.price > b.price ? 1 : -1));
  // } else if (priceFilter.priceFilter == "high") {
  //   phones = phones.sort((a, b) => (b.price > a.price ? 1 : -1));
  // }
  const color = { color: "#1E90FF" };
  useEffect(() => {
  }, [dispatch,productListSlice]);
  const [currentPage, setCurrentPage] = useState(1);
  const phonesPerPage = 20;
  const indexOfLastPhone = currentPage * phonesPerPage;
  phones = productListSlice;
  const indexOfFirstPhone = indexOfLastPhone - phonesPerPage;
  //const currentPhones = phones.slice(indexOfFirstPhone, indexOfLastPhone);
   const pageCount = Math.ceil(phones.length / phonesPerPage);

  //I must do something like splice

  
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
  return (
    <div style={{ width: "75%" }}>
     
      <div>
        <div className={layoutClassName}>
          {productListSlice.status == "succeeded" ? (
          <>
         <Grid templateColumns="repeat(4, 1fr)" gap={4}>
			
					<React.Fragment>
       
						{productListSlice.items[0].map((item) => (
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
              currentPage == pageCount ? "page-item disabled" : "page-item "
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
