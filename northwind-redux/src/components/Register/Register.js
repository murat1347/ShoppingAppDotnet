import React from 'react';
import { useFormik } from 'formik';
import { Alert, Box } from '@chakra-ui/react';
import { getRegister } from '../../redux/actions/registerActions'
import { fetchRegister } from '../../api';
const Register = () => {
  const formik = useFormik({
    initialValues: {
      firstName: '',
      lastName: '',
      email: '',
      password: '',
      rePassword: '',
      userName: ''
    },
    onSubmit: (values, bag) => {
      try {
        fetchRegister({
          email: values.email,
          firstName : values.firstName,
          lastName :values.lastName,
          password : values.password,
          rePassword : values.rePassword,
          userName :values.userName}
        );
        console.log(values);
      }
      catch (e) {
        bag.setErrors({ general: e.response.data.message });
      }
    },
  });
  return (
      
    <form onSubmit={formik.handleSubmit}>
      <label htmlFor="firstName">First Name</label>
      <input class="form-control"
        id="firstName"
        name="firstName"
        type="text"
        onChange={formik.handleChange}
        value={formik.values.firstName}
      />
      <label htmlFor="lastName">Last Name</label>
      <input class="form-control"
        id="lastName"
        name="lastName"
        type="text"
        onChange={formik.handleChange}
        value={formik.values.lastName}
      />
      <label htmlFor="email">Email Address</label>
      <input class="form-control"
        id="email"
        name="email"
        type="email"
        onChange={formik.handleChange}
        value={formik.values.email}
      />
      <label htmlFor="email">Username</label>
      <input class="form-control"
        id="userName"
        name="userName"
        type="text"
        onChange={formik.handleChange}
        value={formik.values.userName}
      />

      <label htmlFor="password">password</label>
      <input class="form-control"
        id="password"
        name="password"
        type="password"
        onChange={formik.handleChange}
        value={formik.values.password}
      />

      <label htmlFor="email">Confirm password</label>
      <input class="form-control"
        id="rePassword"
        name="rePassword"
        type="password"
        onChange={formik.handleChange}
        value={formik.values.rePassword}
      />
      <button class="btn btn-success" type="submit">Submit</button>
    </form>
  );
};
export default Register;