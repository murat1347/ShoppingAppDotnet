import axios from "axios";


axios.interceptors.request.use(
	function (config) {
		const { origin } = new URL(config.url);

		const allowedOrigins = ["http://localhost:4988"];
		const token = localStorage.getItem("token");

		// if (allowedOrigins.includes(origin)) {
		// 	config.headers.Authorization = token;
		// }

		return config;
	},
	function (error) {
		// Do something with request error
		return Promise.reject(error);
	}
);

export const fetchProductList = async () => {
	const { data } = await axios.get(
		`http://localhost:4988/api/Product`
	);

	return data;
};

export const fetchProduct = async (id) => {
	const { data } = await axios.get(
		`https://localhost:5001/api/v1/Product/${id}`,{params:{version:1,id:id}}
	);

	return data;
};

export const fetchRegister = async (input) => {
	const { data } = await axios.post(
		`http://localhost:4988/api/register`,
		input
	);

	return data;
};

export const fetchLogin = async (input) => {
	const { data } = await axios.post(
		'http://localhost:4988/api/account',
		input
	);

	return data;
};

export const fetchMe = async () => {
	const { data } = await axios.get(
		`http://localhost:4988/api/account`
	);
	return data;
};

export const fetchLogout = async () => {
	const { data } = await axios.post(
		`http://localhost:4988/api/logout`,
		{
			refresh_token: localStorage.getItem("refresh-token"),
		}
	);

	return data;
};

export const postOrder = async (input) => {
	const { data } = await axios.post(
		`${process.env.REACT_APP_BASE_ENDPOINT}/order`,
		input
	);

	return data;
};

export const fetchOrders = async () => {
	const { data } = await axios.get(
		`${process.env.REACT_APP_BASE_ENDPOINT}/order`
	);

	return data;
};

export const deleteProduct = async (product_id) => {
	const { data } = await axios.delete(
		`http://localhost:4988/api/product/${product_id}`
	);

	return data;
};
