import { BrowserRouter, Route, Navigate, Routes } from "react-router-dom";
import React, { Fragment } from "react";
import { useAuth } from "../contexts/AuthContext";

function ProtectedRoute({ component: Component, admin, ...rest }) {
	const { loggedIn, user } = useAuth();

	return (<BrowserRouter>     <Routes>
		<Route
			{...rest}
			render={(props) => {
				// if (admin && user.role !== "admin") {
				// 	return <Redirect to={{ pathname: "/" }} />;
				// }


				if (loggedIn) {
					return <Component {...props} />;
				}

				return <Navigate to={{ pathname: "/" }} />;
			}}
		/> </Routes></BrowserRouter>
	);
}

export default ProtectedRoute;
