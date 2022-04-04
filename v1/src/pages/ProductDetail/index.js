import { useParams } from "react-router-dom";
import { useQuery } from "react-query";
import { fetchProduct } from "../../api";
import { Box, Text, Button, Image,Center} from "@chakra-ui/react";
import moment from "moment";
import ImageGallery from "react-image-gallery";
import { useBasket } from "../../contexts/BasketContext";

function ProductDetail() {
	const { product_id } = useParams();
	const { addToBasket, items } = useBasket();

	const { isLoading, isError, data } = useQuery(["product", product_id], () =>
		fetchProduct(product_id)
	);

	if (isLoading) {
		return <div>Loading...</div>;
	}

	if (isError) {
		return <div>Error.</div>;
	}

	const findBasketItem = items.find((item) => item.id === product_id);
	// const images = data.image((image) => ({ original: image }));
	
	return (
		<Center>
		<div>
		
		<Center>
			<Text as="h1" fontSize="2xl">
				{data.name}
			</Text>
			</Center>
			<Box margin="10">
				{/* <ImageGallery items={images} showThumbnails={false} /> */}
				<Image src={data.imageUrl} alt="" />
			</Box>
			<p>{data.description}</p>	<Center>
			<Button
				colorScheme={findBasketItem ? "pink" : "green"}
				onClick={() => addToBasket(data, findBasketItem)}
			>
				{findBasketItem ? "Remove from basket" : "Add to basket"}
			</Button> 	</Center>
			{/* <Text>{moment(data.createdAt).format("DD/MM/YYYY")}</Text> */}
		</div></Center>
	);
}

export default ProductDetail;
