import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import './App.css';
import Navbar from './components/Navbar';
import Signup from './pages/Auth/Signup';
import Signin from './pages/Auth/Signin';
import Products from './pages/Products';
import ProductDetail from './pages/ProductDetail';
import Error404 from './pages/Error404';
import Basket from './pages/Basket';
import Profile from './pages/Profile';
import ProtectedRoute from './pages/ProtectedRoute';
import Admin from './pages/Admin';

function App() {
  return (
    <Router>
      <div>
        <Navbar />

        <div id='content'>
          <Switch>
            <Route path='/' exact component={Products}></Route>
            <ProtectedRoute
              path='/profile'
              component={Profile}
            ></ProtectedRoute>
            <ProtectedRoute
              path='/admin'
              admin={true}
              component={Admin}
            ></ProtectedRoute>
            <Route path='/product/:productId' component={ProductDetail}></Route>
            <Route path='/signin' component={Signin}></Route>
            <Route path='/basket' component={Basket}></Route>
            <Route path='/signup' component={Signup}></Route>
            <Route path='*' component={Error404}></Route>
          </Switch>
        </div>
      </div>
    </Router>
  );
}

export default App;
