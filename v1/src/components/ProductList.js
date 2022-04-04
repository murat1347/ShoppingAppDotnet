import React, { useState, useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { getProducts } from "../redux/actions/productActions";
import { Grid, Box } from "@chakra-ui/react";
import Card from "./Card";
import { GetBrands } from "../redux/actions/categoryFilterActions";


const ProductList = () => {
  
  //const productsState = useSelector((state) => state.product);
  const productsState = useSelector((state) => state.filteredCategory);
  const filteredCategory = useSelector((state) => state.filteredCategory);
  const priceFilter = useSelector((state) => state.priceFilter);
  const dispatch = useDispatch();
  const [columnCount, setColumnCount] = useState(3);
  const layoutClassName = `row row-cols-${columnCount}`;
  let phones;

  if (filteredCategory.filteredCategory.length > 0) {
   
    phones = productsState.filteredCategory.products.filter((phone) => {
    
      if (filteredCategory.filteredCategory.includes(phone.id.toString())) {
        return true;
      }
    });
  } else {
  
   
    phones = productsState.filteredCategory.products;
  }
  if (priceFilter.priceFilter == "low") {
    phones = phones.sort((a, b) => (a.price > b.price ? 1 : -1));
  } else if (priceFilter.priceFilter == "high") {
    phones = phones.sort((a, b) => (b.price > a.price ? 1 : -1));
  }
  const color = { color: "#1E90FF" };
  useEffect(() => {
    dispatch(GetBrands());
  }, [GetBrands]);
  const [currentPage, setCurrentPage] = useState(1);
  const phonesPerPage = 20;
  const indexOfLastPhone = currentPage * phonesPerPage;
  phones = productsState.filteredCategory;
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
          {productsState.success? (
          <>
         <Grid templateColumns="repeat(4, 1fr)" gap={4}>
			
					<React.Fragment>
         
						{productsState.filteredCategory.products.map((item) => (
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
