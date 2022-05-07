import "./App.css";

import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

import Navbar from "./components/Navbar/index";
import ProtectedRoute from "./pages/ProtectedRoute";
import ProductDetail from "./pages/ProductDetail";
import Signin from "./pages/Auth/Signin";
import Signup from "./pages/Auth/Signup";
import Profile from "./pages/Profile";
import Basket from "./pages/Basket";
import Admin from "./pages/Admin";
import Error404 from "./pages/Error404";
import Home from "./components/Home";
import Footer from "./components/Footer";
import AddProduct from "./components/addProduct"
import ProductList from "./components/ProductList";
import RoleList from "./pages/Role/RoleList"
function App() {
	return (
		<Router>
			<div>
				<Navbar />
				
				<div id="content">
					<Routes>
						<Route path="/" exact element={<Home />} />
						<Route path="/?CategoryId=:CategoryId&sortBy=:sortBy&page=:page&PAGE_SIZE=:PAGE_SIZE" element={<Home />} />
						<Route path="/product/:product_id" element={<ProductDetail />} />
						<Route path="/signin" element={<Signin />} />
						<Route path="/signup" element={<Signup />} />
						<Route path="/addproduct" element={<AddProduct />} />
						<Route path="/basket" element={<Basket />} />
						<Route path="/profile" element={<Profile />} />
						<Route path="/admin" element={<Admin />} admin={true} />
						<Route path="/rolelist" element={<RoleList/>} />
						<Route path="*" element={<Error404 />} />
					</Routes>
				</div>
				<Footer/>
			</div>
		</Router>
	);
}

export default App;
