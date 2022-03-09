import React from 'react';

import validationSchema from './validations';

import { useQueryClient, useMutation } from 'react-query';

import { message } from 'antd';
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
import { postProduct } from '../../../api';

function NewProduct() {
  const queryClient = useQueryClient();

  const NewProductMutation = useMutation(postProduct, {
    onSuccess: () =>
      queryClient.invalidateQueries(['admin:products', 'products']),
  });

  const handleSubmit = async (values, bag) => {
    message.loading({ content: 'Loading...', key: 'productPost' });

    const newValues = {
      ...values,
      photos: JSON.stringify(values.photos),
    };
    NewProductMutation.mutate(newValues, {
      onSuccess: () =>
        message.success({
          content: 'The product succesfully postted ',
          key: 'productPost',
          duration: 2,
        }),
    });
  };

  return (
    <div>
      <Text fontSize='2xl'>New Product</Text>
      <Formik
        initialValues={{
          title: 'Test',
          description: 'lorem ipsum',
          price: '1000',
          photos: [],
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
                    Save
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

export default NewProduct;
