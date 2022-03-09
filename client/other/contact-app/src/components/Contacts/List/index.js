import { useState } from 'react';

function List({ contacts }) {
  const [filterText, setFilterText] = useState('');

  const filteredContacts = contacts.filter((contact) => {
    return Object.keys(contact).some((key) =>
      contact[key].toString().toLowerCase().includes(filterText.toLowerCase())
    );
  });

  return (
    <>
      <input
        placeholder='Filter Contact'
        value={filterText}
        onChange={(e) => setFilterText(e.target.value)}
      ></input>
      <ul className='list'>
        {filteredContacts.map((contact, i) => (
          <li key={i}>
            <span>{contact.fullname}</span>
            <span>{contact.phone}</span>
          </li>
        ))}
      </ul>
      <p>Total Contacts ({filteredContacts.length})</p>
    </>
  );
}

export default List;
