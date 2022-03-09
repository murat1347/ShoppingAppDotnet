import { useQuery } from 'react-query';
import { useParams } from 'react-router-dom';
import moment from 'moment';
import ImageGallery from 'react-image-gallery';
import { Box, Text, Button } from '@chakra-ui/react';
import { useBasket } from '../../contexts/BasketContext';
import { fetchProduct } from '../../api';

function ProductDetail() {
  const { productId } = useParams();
  const { addToBasket, items } = useBasket();

  const { isLoading, error, data } = useQuery(['product', productId], () =>
    fetchProduct(productId)
  );

  if (isLoading) return 'Loading...';

  if (error) return 'An error has occurred: ' + error.message;

  const findBasketItem = items.find((item) => item._id === productId);

  const images = data.photos.map((url) => ({ original: url }));

  return (
    <div>
      <Button
        colorScheme={findBasketItem ? 'pink' : 'green'}
        variant='solid'
        onClick={() => addToBasket(data, findBasketItem)}
      >
        {findBasketItem ? 'Remove from basket' : 'Add to basket'}
      </Button>
      <Text as='h2' fontSize='2xl'>
        {data.title}
      </Text>
      <Text>{moment(data.createdAt).format('DD/MM/YYYY')}</Text>
      <p>{data.description}</p>
      <Box margin='10'>
        <ImageGallery items={images}></ImageGallery>
      </Box>
    </div>
  );
}

export default ProductDetail;
