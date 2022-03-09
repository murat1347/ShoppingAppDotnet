import {
  Alert,
  AlertDescription,
  AlertIcon,
  AlertTitle,
} from '@chakra-ui/react';
import React from 'react';

function Error404() {
  return (
    <Alert status='error'>
      <AlertIcon />
      <AlertTitle mr={2}>Error 404</AlertTitle>
      <AlertDescription>This page was not found!</AlertDescription>
    </Alert>
  );
}

export default Error404;
