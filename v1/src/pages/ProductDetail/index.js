import { useParams } from "react-router-dom";
import { useQuery } from "react-query";
import { fetchProduct } from "../../api";
import { Box, Text, Button, Image,Center} from "@chakra-ui/react";
import moment from "moment";
import ImageGallery from "react-image-gallery";
import { useBasket } from "../../contexts/BasketContext";
import { useEffect, useState } from "react";
import axios from "axios";


const ProductDetail = () =>{
	const { product_id } = useParams();
	const [loading, setLoading] = useState(true);
	const [char, setChar] = useState(null);
  
	useEffect(() => {
	  const response = axios
		.get(`http://localhost:4988/api/Product/${product_id}`)
		.then((res) => res.data)
		.then((data) => setChar(data))
		.finally(() => setLoading(false));
	}, [product_id]);
	return (
	  <div>
		{/* {loading && (
		  <div
			style={{
			  display: "flex",
			  justifyContent: "center",
			  alignItems: "center",
			}}
		  >
			<Loading />
		  </div>
		)} */}
		
		{char && (
		<Center>  <div>
			<h1>{char.name}</h1>
			<Image src={char.image} alt="" /><br/>
			<p>{char.description}</p> <br/>
			<p>{char.stock} in stock</p> <br/>
			<p>{char.price}₺</p> <br/>
		
		  </div></Center>
		)}
		
	  </div>
	);
  };
export default ProductDetail;
