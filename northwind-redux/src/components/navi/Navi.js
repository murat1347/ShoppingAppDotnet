import React from "react";
import { Button } from "@chakra-ui/react";
import { Link } from "react-router-dom";
import { useState, useEffect } from "react";
import { useAuth } from "../../contexts/AuthContext";
import AuthService from "../../redux/auth-service";
import { getCurrentUser, logout } from "../../api"
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink
} from "reactstrap";
import CartSummary from "../cart/CartSummary";

const Navi = () => {

  const { loggedIn, user } = useAuth();
  console.log(loggedIn)
  return (
    
    <div>
      <Navbar color="light" light expand="md">
        <NavbarBrand><Link to="/">KodPlus</Link></NavbarBrand>
        <NavbarToggler />
        <Collapse navbar>
          <Nav className="ml-auto" navbar>

            
            {loggedIn===false && (
              <><NavItem>
                <NavLink>
                  <Link to="/login">Login</Link>
                </NavLink></NavItem>
                <NavItem>
                  <NavLink>
                    <Link to="/Register">Register</Link>
                  </NavLink>
                </NavItem>
              </>
            )}
            {loggedIn && (
              <>
                <NavItem>
                  <NavLink>
                    <Link to="/saveproduct">Ürün ekle</Link>
                  </NavLink>
                </NavItem>
                <NavLink>
                  <Link to="/Profile">Profile</Link>
                </NavLink>
              </>
            )}
            <CartSummary />
          </Nav>
        </Collapse>
      </Navbar>
    </div>
  );

}

export default Navi;