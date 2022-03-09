import { Box, Image, Button } from '@chakra-ui/react';
import { Link } from 'react-router-dom';
import moment from 'moment';
import { useBasket } from '../../contexts/BasketContext';
import { useAuth } from '../../contexts/AuthContext';

function Card({ item }) {
  const { addToBasket, items } = useBasket();
  const { loggedIn } = useAuth();

  const findBasketItem = items.find(
    (basketItem) => basketItem._id === item._id
  );
  return (
    <Box borderWidth='1px' borderRadius='lg' overflow='hidden' p='3'>
      <Link to={`/product/${item._id}`}>
        <Image
          boxSize='300px'
          objectFit='cover'
          src={item.photos[0]}
          m='auto'
          alt='product'
          loading='lazy'
        />
        <Box
          p='6'
          d='flex'
          alignItems='center'
          justifyContent='center'
          flexDirection='column'
          baseline
        >
          <Box>{moment(item.createdAt).format('DD/MM/YYYY')}</Box>
          <Box mt='1' fontWeight='semibold' as='h4' lineHeight='tight'>
            {item.title}
          </Box>
          <Box>{item.price} TL</Box>
        </Box>
      </Link>
      <Button
        colorScheme={findBasketItem ? 'pink' : 'green'}
        variant='solid'
        width='full'
        disabled={!loggedIn}
        onClick={() => addToBasket(item, findBasketItem)}
      >
        {findBasketItem ? 'Remove from basket' : 'Add to basket'}
      </Button>
    </Box>
  );
}

export default Card;
