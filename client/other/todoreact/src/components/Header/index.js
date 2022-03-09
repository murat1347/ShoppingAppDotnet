import { useState } from 'react';

function Header({ addTodo, todos }) {
  const [input, setInput] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    addTodo([
      ...todos,
      {
        value: input,
        done: false,
      },
    ]);
  };

  return (
    <header className='header'>
      <h1>todos</h1>
      <form onSubmit={handleSubmit}>
        <input
          className='new-todo'
          placeholder='What needs to be done?'
          autoFocus
          value={input}
          onChange={(e) => setInput(e.target.value)}
        />
      </form>
    </header>
  );
}

export default Header;
