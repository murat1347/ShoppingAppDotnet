import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import { applyMiddleware, createStore } from 'redux';
import "./reset.css";
import "antd/dist/antd.css";
import {Provider} from "react-redux"
import { QueryClient, QueryClientProvider } from "react-query";
import { ReactQueryDevtools } from "react-query/devtools";
import thunk from "redux-thunk";
import rootReducer from "./redux/store"
// contexts
import { AuthProvider } from "./contexts/AuthContext";
import { BasketProvider } from "./contexts/BasketContext";
import { ChakraProvider } from "@chakra-ui/react";
import App from "./App";
import reportWebVitals from "./reportWebVitals";
const store = createStore(rootReducer, applyMiddleware(thunk));

const queryClient = new QueryClient({
	defaultOptions: {
		queries: {
			refetchOnMount: false,
			refetchOnWindowFocus: false,
		},
	},
});

ReactDOM.render(
	<QueryClientProvider client={queryClient}>
		<ChakraProvider>
			<AuthProvider>
				<BasketProvider>
					<Provider store={store}>
					<App /></Provider> 
				</BasketProvider>
			</AuthProvider>
		</ChakraProvider>

		<ReactQueryDevtools initialIsOpen={false} />
	</QueryClientProvider>,
	document.getElementById("root")
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
