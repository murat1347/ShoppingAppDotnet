import React from "react";
import {
	Flex,
	Box,
	Heading,
	FormControl,
	FormLabel,
	Input,
	Button,
	Alert,
} from "@chakra-ui/react";

import { useFormik } from "formik";
import validationSchema from "./validations";

import { fetchRegister } from "../../../api";

import { useAuth } from "../../../contexts/AuthContext";

function Signup({ history }) {
	const { login } = useAuth();

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
		<div>
			<Flex align="center" width="full" justifyContent="center">
				<Box pt={10}>
					<Box textAlign="center">
						<Heading>Sign Up</Heading>
					</Box>
					<Box my={5}>
						{formik.errors.general && (
							<Alert status="error">{formik.errors.general}</Alert>
						)}
					</Box>
					<Box my={5} textAlign="left">
						<form onSubmit={formik.handleSubmit}>
							<FormControl>
								<FormLabel>E-mail</FormLabel>
								<Input
									name="email"
									onChange={formik.handleChange}
									onBlur={formik.handleBlur}
									value={formik.values.email}
									isInvalid={formik.touched.email && formik.errors.email}
								/>
							</FormControl>

							<FormControl mt="4">
								<FormLabel>Password</FormLabel>
								<Input
									name="password"
									type="password"
									onChange={formik.handleChange}
									onBlur={formik.handleBlur}
									value={formik.values.password}
									isInvalid={formik.touched.password && formik.errors.password}
								/>
							</FormControl>

							<FormControl mt="4">
								<FormLabel>Password Confirm</FormLabel>
								<Input
									name="rePassword"
									type="password"
									onChange={formik.handleChange}
									onBlur={formik.handleBlur}
									value={formik.values.rePassword}
									isInvalid={
										formik.touched.rePassword &&
										formik.errors.rePassword
									}
								/>
							</FormControl>

							<FormControl mt="4">
								<FormLabel>FirstName</FormLabel>
								<Input
									name="firstName"
									type="text"
									onChange={formik.handleChange}
									onBlur={formik.handleBlur}
									value={formik.values.firstName}
									// isInvalid={
									// 	formik.touched.firstName &&
									// 	formik.errors.firstName
									// }
								/>
							</FormControl>

							<FormControl mt="4">
								<FormLabel>LastName</FormLabel>
								<Input
									name="lastName"
									type="text"
									onChange={formik.handleChange}
									onBlur={formik.handleBlur}
									value={formik.values.lastName}
									// isInvalid={
									// 	formik.touched.lastName &&
									// 	formik.errors.lastName
									// }
								/>
							</FormControl>

							<FormControl mt="4">
								<FormLabel>UserName</FormLabel>
								<Input
									name="userName"
									type="text"
									onChange={formik.handleChange}
									onBlur={formik.handleBlur}
									value={formik.values.userName}
									// isInvalid={
									// 	formik.touched.userName &&
									// 	formik.errors.userName
									// }
								/>
							</FormControl>
							<Button mt="4" width="full" type="submit">
								Sign Up
							</Button>
						</form>
					</Box>
				</Box>
			</Flex>
		</div>
	);
}

export default Signup;
