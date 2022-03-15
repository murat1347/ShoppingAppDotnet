import { Flex, Spinner } from '@chakra-ui/react';
import React, { useState, useContext, createContext, useEffect } from 'react';
import { fetchLogout, fetchMe } from '../api';

const AuthContext = createContext();

const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [loggedIn, setLoggedIn] = useState(false);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    (async () => {
      
      try {
        const me = getCurrentUser;
        console.log(me)
        setLoggedIn(false);
        setUser(me);
        setLoading(false);
      } catch {
        setLoading(false);
      }
    })();
  }, []);

  const login = (data) => {
    setLoggedIn(true);
    setUser(data);
    localStorage.setItem('token', data.token);
    localStorage.setItem('refleshToken', data.refleshToken);
  };

  const logout = async (cb) => {
    setLoggedIn(false);
    setUser(null);
    //await fetchLogout();
    localStorage.removeItem('token');
    localStorage.removeItem('refleshToken');
    cb();
  };

  const getCurrentUser=(data)=>{
    localStorage.getItem('refleshToken');
  }
  const values = {
    loggedIn,
    user,
    login,
    logout,
  };

  if (loading)
    return (
      <Flex justifyContent='center' alignItems='center' height='100vh'>
        <Spinner
          thickness='4px'
          speed='0.65'
          emptyColor='gray.200'
          size='xl'
          color='red'
        />
      </Flex>
    );

  return <AuthContext.Provider value={values}>{children}</AuthContext.Provider>;
};

const useAuth = () => useContext(AuthContext);

export { useAuth, AuthProvider };
