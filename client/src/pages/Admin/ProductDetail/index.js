import React from 'react';
import { useQuery } from 'react-query';
import { message } from 'antd';

import { useParams } from 'react-router-dom';
import { fetchProduct, updateProduct } from '../../../api';

import validationSchema from './validations';

import { FieldArray, Formik } from 'formik';
import {
  Box,
  Button,
  FormControl,
  FormLabel,
  Input,
  Text,
  Textarea,
} from '@chakra-ui/react';

function ProductDetail() {
  const { productId } = useParams();
  const { isLoading, isError, data, error } = useQuery(
    ['admin:product', productId],
    () => fetchProduct(productId)
  );
  if (isLoading) return <div>loading...</div>;

  if (isError) return <div>error : {error.message}</div>;

  const handleSubmit = async (values, bag) => {
    message.loading({ content: 'Loading...', key: 'productUpdate' });
    try {
      await updateProduct(values, productId);
      message.success({
        content: 'The product succesfully updated ',
        key: 'productUpdate',
        duration: 2,
      });
    } catch (e) {
      message.error({
        content: 'The product does not updated !',
      });
    }
  };

  return (
    <div>
      <Text fontSize='2xl'>Edit</Text>
      <Formik
        initialValues={{
          title: data.title,
          description: data.description,
          price: data.price,
          photos: data.photos,
        }}
        validationSchema={validationSchema}
        onSubmit={handleSubmit}
      >
        {({
          handleSubmit,
          errors,
          touched,
          handleChange,
          handleBlur,
          values,
          isSubmitting,
        }) => (
          <>
            <Box>
              <Box my='5' textAlign='left'>
                <form onSubmit={handleSubmit}>
                  <FormControl>
                    <FormLabel>Title</FormLabel>
                    <Input
                      name='title'
                      onChange={handleChange}
                      onBlur={handleBlur}
                      value={values.title}
                      disabled={isSubmitting}
                      isInvalid={touched.title && errors.title}
                    />
                    {touched.title && errors.title && (
                      <Text color='red'>{errors.title}</Text>
                    )}
                  </FormControl>
                  <FormControl mt='4'>
                    <FormLabel>Description</FormLabel>
                    <Textarea
                      name='description'
                      onChange={handleChange}
                      onBlur={handleBlur}
                      value={values.description}
                      disabled={isSubmitting}
                      isInvalid={touched.description && errors.description}
                    />
                    {touched.description && errors.description && (
                      <Text color='red'>{errors.description}</Text>
                    )}
                  </FormControl>
                  <FormControl mt='4'>
                    <FormLabel>Price</FormLabel>
                    <Input
                      name='price'
                      type='number'
                      onChange={handleChange}
                      onBlur={handleBlur}
                      value={values.price}
                      disabled={isSubmitting}
                      isInvalid={touched.price && errors.price}
                    />
                    {touched.price && errors.price && (
                      <Text color='red'>{errors.price}</Text>
                    )}
                  </FormControl>
                  <FormControl mt='4'>
                    <FormLabel>Photos</FormLabel>
                    <FieldArray
                      name='photos'
                      render={(arrayHelpers) => (
                        <div>
                          {values.photos &&
                            values.photos.map((photo, i) => (
                              <div key={i}>
                                <Input
                                  name={`photos.${i}`}
                                  value={photo}
                                  disabled={isSubmitting}
                                  onChange={handleChange}
                                  width='3xl'
                                />
                                <Button
                                  ml='4'
                                  type='button'
                                  colorScheme='red'
                                  onClick={() => arrayHelpers.remove(i)}
                                >
                                  Remove
                                </Button>
                              </div>
                            ))}
                          <Button mt='5' onClick={() => arrayHelpers.push('')}>
                            Add a photo
                          </Button>
                        </div>
                      )}
                    />
                  </FormControl>
                  <Button
                    mt='4'
                    width='full'
                    type='submit'
                    isLoading={isSubmitting}
                  >
                    Update
                  </Button>
                </form>{' '}
              </Box>
            </Box>
          </>
        )}
      </Formik>
    </div>
  );
}

export default ProductDetail;
