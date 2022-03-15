import {
    Alert,
    Box,
    Button,
    Flex,
    FormControl,
    FormLabel,
    Heading,
    Input,
  } from '@chakra-ui/react';
  import React from 'react';
  import { useFormik } from 'formik';
  import { fetchLogin } from '../../../api';
  import { useAuth } from '../../../contexts/AuthContext';
  
  import validationSchema from './validations';
  
  function Signin({ history }) {
    const { login } = useAuth();
    const formik = useFormik({
      initialValues: {
        email: '',
        password: ''
      },
      validationSchema,
      onSubmit: async (values, bag) => {
        try {
          const loginResponse = await fetchLogin({
            email :values.email,
            password : values.password
          });
          console.log(values);
          login(loginResponse);
          history.push('/profile');
        } catch (e) {
          bag.setErrors({ general: e.response.data.message });
        }
      },
    });
    return (
      <div>
        <Flex align='center' w='full' justifyContent='center'>
          <Box pt={10}>
            <Box textAlign='center'>
              <Heading>Sign In</Heading>
            </Box>
            <Box my={5}>
              {formik.errors.general && (
                <Alert status='error'>{formik.errors.general}</Alert>
              )}
            </Box>
            <Box my={5} textAlign='left'>
              <form onSubmit={formik.handleSubmit}>
                <FormControl>
                  <FormLabel>E-mail</FormLabel>
                  <Input
                    name='email'
                    onChange={formik.handleChange}
                    onBlur={formik.handleBlur}
                    value={formik.values.email}
                    isInvalid={formik.touched.mail && formik.errors.email}
                  />
                </FormControl>
                <FormControl mt={4}>
                  <FormLabel>Password</FormLabel>
                  <Input
                    name='password'
                    type='password'
                    onChange={formik.handleChange}
                    onBlur={formik.handleBlur}
                    value={formik.values.password}
                    isInvalid={formik.touched.password && formik.errors.password}
                  />
                </FormControl>
                <Button mt={4} width='full' type='submit'>
                  Sign In
                </Button>
              </form>
            </Box>
          </Box>
        </Flex>
      </div>
    );
  }
  
  export default Signin;
  