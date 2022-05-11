import "./App.css";

import { BrowserRouter as Router, Switch, Route } from "react-router-dom";

import Navbar from "./components/Navbar/index";
import ProtectedRoute from "./pages/ProtectedRoute";
import ProductDetail from "./pages/ProductDetail";
import Signin from "./pages/Auth/Signin";
import Signup from "./pages/Auth/Signup";
import Profile from "./pages/Profile";
import Basket from "./pages/Basket";
import Admin from "./pages/Admin";
import Error404 from "./pages/Error404";
import Search from "./pages/Products/search";
import Home from "./components/Home";
import Footer from "./components/Footer";
import AddProduct from "./components/addProduct"
function App() {
	return (
		<Router>
			<div>
				<Navbar />

				<div id="content">
					<Switch>
						<Route path="/" exact component={Home} />
					
						<Route path="/product/:product_id" component={ProductDetail} />
						<Route path="/signin" component={Signin} />
						<Route path="/signup" component={Signup} />
						<Route path="/addproduct" component={AddProduct} />
						<Route path="/basket" component={Basket} />
						<ProtectedRoute path="/profile" component={Profile} />
						<ProtectedRoute path="/admin" component={Admin} admin={true} />
						<Route path="*" component={Error404} />
					</Switch>
				</div>
				<Footer/>
			</div>
		</Router>
	);
}

export default App;
