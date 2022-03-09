import { Button, Text } from '@chakra-ui/react';
import React from 'react';
import { useAuth } from '../../contexts/AuthContext';

function Profile({ history }) {
  const { user, logout } = useAuth();

  const handleLogout = async () => {
    logout(() => {
      history.push('/');
    });
  };

  return (
    <div>
      <Text fontSize={22}>Profile</Text>
      {JSON.stringify(user)}
      <br />
      <br />
      <Button colorScheme='pink' variant='solid' onClick={handleLogout}>
        Logout
      </Button>
    </div>
  );
}

export default Profile;
