import {
  Table,
  Thead,
  Tbody,
  Text,
  Tr,
  Th,
  Td,
  Flex,
  Spinner,
} from '@chakra-ui/react';
import React from 'react';

import { useQuery } from 'react-query';
import { fetchOrders } from '../../../api';

function Orders() {
  const { isLoading, isError, data, error } = useQuery(
    'admin:orders',
    fetchOrders
  );

  if (isLoading)
    return (
      <Flex justifyContent='center' alignItems='center' height='100vh'>
        <Spinner
          thickness='4px'
          speed='0.65'
          emptyColor='gray.200'
          size='xl'
          color='red'
        />
      </Flex>
    );

  if (isError) return <div>Error {error.message} </div>;

  return (
    <div>
      <Text fontSize='2xl' p={5}>
        Orders
      </Text>

      <Table variant='simple'>
        <Thead>
          <Tr>
            <Th>User</Th>
            <Th>Address</Th>
            <Th isNumeric>Items</Th>
          </Tr>
        </Thead>
        <Tbody>
          {data.map((item) => (
            <Tr key={item._id}>
              <Td>{item.user.email}</Td>
              <Td>{item.adress}</Td>
              <Td isNumeric> {item.items.length}</Td>
            </Tr>
          ))}
        </Tbody>
      </Table>
    </div>
  );
}

export default Orders;
