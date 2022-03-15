import React from "react";
import { Container } from "reactstrap";
import Navi from "../navi/Navi";
import Dashboard from "./Dashboard";
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import CartDetail from "../cart/CartDetail";
import AddOrUpdateProduct from "../products/AddOrUpdateProduct";
import NotFound from "../common/NotFound";
import Register from "../Register/Register";
import Profile from '../Profile/Index';
import Signin from '../Profile/Login/Index';
import ProtectedRoute from "../../ProtectedRoute";
import { useAuth } from "../../contexts/AuthContext";
  

function App() {
  return (
    <Container><Router><div>
      <Navi />
        

      <Switch>
        <Route path="/" exact component={Dashboard} />
        <Route path="/product"  component={Dashboard} />
        <Route path="/saveproduct/:productId" component={AddOrUpdateProduct} />
        <Route path="/saveproduct" component={AddOrUpdateProduct} />
        <Route path="/cart"  component={CartDetail} />
        <Route path="/register" component={Register} />
        
        <Route path="/login" component={Signin} />
        <ProtectedRoute
             
              path='/profile'
              component={Profile}
            />
        <Route component={NotFound} />
      </Switch>
     </div>
     </Router>
    </Container>
  );
}

export default App;
