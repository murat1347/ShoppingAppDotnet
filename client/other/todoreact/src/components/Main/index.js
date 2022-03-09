import { useState } from 'react';
import TodoItem from './Todoİtem';

let filteredTodos;

function Main({ todos, setTodos }) {
  const [allComplated, setAllComplated] = useState(false);
  const [category, setCategory] = useState('all');

  const leftTodos = todos.filter((todo) => todo.done === false).length;

  switch (category) {
    case 'all':
      filteredTodos = todos;
      break;
    case 'active':
      filteredTodos = todos.filter((todo) => todo.done === false);
      break;
    case 'completed':
      filteredTodos = todos.filter((todo) => todo.done === true);
      break;
    default:
      break;
  }
  const handleAllTodosDone = () => {
    setTodos(
      todos.map((todo) => ({
        ...todo,
        done: !allComplated,
      }))
    );
    setAllComplated(!allComplated);
  };

  const handleDoneTodo = (id) => {
    setTodos(
      todos.map((todo) =>
        todo.id === id
          ? {
              ...todo,
              done: !todo.done,
            }
          : todo
      )
    );
  };

  const handleDeleteTodo = (id) => {
    setTodos(todos.filter((todo) => todo.id !== id));
  };

  const handleClearCompleted = () => {
    setTodos(todos.filter((todo) => todo.done === false));
  };

  const handleChangeTodos = (id, value) => {
    setTodos(todos.map((todo) => (todo.id === id ? { ...todo, value } : todo)));
  };

  return (
    <>
      <section className='main'>
        {todos.length > 0 && (
          <>
            <input className='toggle-all' type='checkbox' />
            <label htmlFor='toggle-all' onClick={handleAllTodosDone}>
              Mark all as complete
            </label>
          </>
        )}
        <ul className='todo-list'>
          {filteredTodos.map((todo, i) => (
            <TodoItem
              key={i}
              todo={todo}
              handleDoneTodo={handleDoneTodo}
              handleDeleteTodo={handleDeleteTodo}
              handleChangeTodos={handleChangeTodos}
            />
          ))}
        </ul>
      </section>
      {todos.length > 0 && (
        <footer className='footer'>
          <span className='todo-count'>
            <strong>{leftTodos} </strong>
            items left
          </span>

          <ul className='filters'>
            <li>
              <a
                href='/'
                className={category === 'all' ? 'selected' : null}
                onClick={(e) => {
                  e.preventDefault();
                  setCategory('all');
                }}
              >
                All
              </a>
            </li>
            <li>
              <a
                href='/'
                className={category === 'active' ? 'selected' : null}
                onClick={(e) => {
                  e.preventDefault();
                  setCategory('active');
                }}
              >
                Active
              </a>
            </li>
            <li>
              <a
                href='/'
                className={category === 'completed' ? 'selected' : null}
                onClick={(e) => {
                  e.preventDefault();
                  setCategory('completed');
                }}
              >
                Completed
              </a>
            </li>
          </ul>

          {leftTodos !== todos.length ? (
            <button className='clear-completed' onClick={handleClearCompleted}>
              Clear completed
            </button>
          ) : null}
        </footer>
      )}
    </>
  );
}

export default Main;
