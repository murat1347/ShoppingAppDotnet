import {
  Alert,
  Box,
  Button,
  Image,
  Link,
  List,
  ListItem,
  Text,
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalFooter,
  ModalBody,
  ModalCloseButton,
  useDisclosure,
  FormControl,
  FormLabel,
  Textarea,
} from '@chakra-ui/react';
import { useRef, useState } from 'react';
import { postOrder } from '../../api';
import { useBasket } from '../../contexts/BasketContext';

function Basket() {
  const { isOpen, onOpen, onClose } = useDisclosure();
  const initialRef = useRef();
  const finalRef = useRef();

  const [address, setAddress] = useState('');

  const { items, removeFromBasket, emptyBasket } = useBasket();

  const total = items.reduce((acc, item) => acc + item.price, 0);

  const handleSubmit = async () => {
    const itemsIds = items.map((item) => item._id);
    const input = {
      address,
      items: JSON.stringify(itemsIds),
    };

    await postOrder(input);

    emptyBasket();
    onClose();
    setAddress('');
  };

  return (
    <Box p={5}>
      {items.length < 1 && (
        <Alert status='warning'>You have not any items in your basket</Alert>
      )}
      {items.length > 0 && (
        <>
          <List d='flex'>
            {items.map((item) => (
              <ListItem key={item._id} mr={25}>
                <Link to={`/product/${item._id}`}>
                  <Text fontSize={18} mb='3'>
                    {item.title} - {item.price} TL
                  </Text>
                  <Image
                    boxSize='200px'
                    objectFit='cover'
                    m='auto'
                    loading='lazy'
                    src={item.photos[0]}
                    alt='basketitem'
                  />
                </Link>
                <Button
                  mt='4'
                  size='sm'
                  width='full'
                  colorScheme='pink'
                  onClick={() => removeFromBasket(item._id)}
                >
                  Remove from basket
                </Button>
              </ListItem>
            ))}
          </List>
          <Box mt={10}>
            <Text fontSize={22}>Total {total} TL</Text>
          </Box>
          <Button mt='2' size='sm' colorScheme='green' onClick={onOpen}>
            Order
          </Button>
          <Modal
            initialFocusRef={initialRef}
            finalFocusRef={finalRef}
            isOpen={isOpen}
            onClose={onClose}
          >
            <ModalOverlay />
            <ModalContent>
              <ModalHeader>Order</ModalHeader>
              <ModalCloseButton />
              <ModalBody pb={6}>
                <FormControl>
                  <FormLabel>Address</FormLabel>
                  <Textarea
                    ref={initialRef}
                    placeholder='address'
                    value={address}
                    onChange={({ target }) => setAddress(target.value)}
                  />
                </FormControl>
              </ModalBody>

              <ModalFooter>
                <Button colorScheme='blue' mr={3} onClick={handleSubmit}>
                  Save
                </Button>
                <Button onClick={onClose}>Cancel</Button>
              </ModalFooter>
            </ModalContent>
          </Modal>
        </>
      )}
    </Box>
  );
}

export default Basket;
