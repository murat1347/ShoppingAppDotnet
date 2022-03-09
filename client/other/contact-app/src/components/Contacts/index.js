import { useState, useEffect } from 'react';
import Form from './Form';
import List from './List';
import './styles.css';

function Contacts() {
  const [contacts, setContacts] = useState([
    {
      fullname: 'Melih',
      phone: 123,
    },
    {
      fullname: 'Ahmet',
      phone: 456,
    },
    {
      fullname: 'Egemen',
      phone: 789,
    },
  ]);

  useEffect(() => {
    console.log(contacts);
  }, [contacts]);

  return (
    <div id='container'>
      <h1> Contacts App</h1>
      <List contacts={contacts} />
      <Form addContact={setContacts} contacts={contacts} />
    </div>
  );
}

export default Contacts;
