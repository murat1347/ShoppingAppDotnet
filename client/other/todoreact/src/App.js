import { useState } from 'react';
import { v4 as uuidv4 } from 'uuid';
import Header from './components/Header';
import Main from './components/Main';
import './App.css';

function App() {
  const [todos, setTodos] = useState([
    {
      id: uuidv4(),
      value: 'Okula git',
      done: false,
    },
    {
      id: uuidv4(),
      value: 'Ders çalış',
      done: false,
    },
    {
      id: uuidv4(),
      value: 'Temizlik yap',
      done: true,
    },
  ]);

  return (
    <>
      <section className='todoapp'>
        <Header todos={todos} addTodo={setTodos} />
        <Main todos={todos} setTodos={setTodos} />
      </section>
    </>
  );
}

export default App;
