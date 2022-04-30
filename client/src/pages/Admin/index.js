import React from "react";
import "./styles.css";

import { Box } from "@chakra-ui/react";
import { Link, Switch,Routes, Route, useMatch} from "react-router-dom";

import Home from "./Home";
import Products from "./Products";
import Orders from "./Orders";

function Admin() {
	const { path, url } = useMatch();

	return (
		<div>
			<nav>
				<ul className="admin-menu">
					<li>
						<Link to={url}>Home</Link>
					</li>
					<li>
						<Link to={`${url}/orders`}>Orders</Link>
					</li>
					<li>
						<Link to={`${url}/products`}>Products</Link>
					</li>
				</ul>
			</nav>

			<Box mt="10">
				<Routes>
					<Route exact path={path} component={Home} />
					<Route path={`${path}/orders`} component={Orders} />
					<Route path={`${path}/products`} component={Products} />
				</Routes>
			</Box>
		</div>
	);
}

export default Admin;
