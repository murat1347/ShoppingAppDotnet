import { useState } from 'react';

function TodoItem({
  todo,
  handleDoneTodo,
  handleChangeTodos,
  handleDeleteTodo,
}) {
  const [isActive, setIsActive] = useState(false);
  const [changeTodo, setChangeTodo] = useState(todo.value);

  const handleChangeTodo = (id) => {
    handleChangeTodos(id, changeTodo);
    setIsActive(false);
  };

  return (
    <li className={todo.done ? 'completed' : ''}>
      <div className='view'>
        <input
          className='toggle'
          type='checkbox'
          checked={todo.done}
          onChange={() => handleDoneTodo(todo.id)}
        />
        {isActive ? (
          <input
            className='focusLabel'
            value={changeTodo}
            onChange={(e) => setChangeTodo(e.target.value)}
            onBlur={() => handleChangeTodo(todo.id)}
            autoFocus
          />
        ) : (
          <label onClick={() => setIsActive(true)}>{todo.value}</label>
        )}
        <button
          className='destroy'
          onClick={() => handleDeleteTodo(todo.id)}
        ></button>
      </div>
    </li>
  );
}

export default TodoItem;
