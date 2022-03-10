import React from "react";
import { Container } from "reactstrap";
import Navi from "../navi/Navi";
import Dashboard from "./Dashboard";
import { Route, Switch } from "react-router-dom";
import CartDetail from "../cart/CartDetail";
import AddOrUpdateProduct from "../products/AddOrUpdateProduct";
import NotFound from "../common/NotFound";
import Register from "../Register/Register";
import Profile from '../Profile/Index';
import Signin from '../Profile/Login/Index';
function App() {
  return (
    <Container>
      <Navi />
      <Switch>
        <Route path="/" exact component={Dashboard} />
        <Route path="/product"  component={Dashboard} />
        <Route path="/saveproduct/:productId" component={AddOrUpdateProduct} />
        <Route path="/saveproduct" component={AddOrUpdateProduct} />
        <Route path="/cart"  component={CartDetail} />
        <Route path="/register" component={Register} />
        <Route path="/profile" component={Profile} />
        <Route path="/login" component={Signin} />
        <Route component={NotFound} />
      </Switch>
     
    </Container>
  );
}

export default App;
