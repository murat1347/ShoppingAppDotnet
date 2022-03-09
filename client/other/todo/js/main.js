const task = document.getElementById('task');
const listContainer = document.getElementById('list');

let tasks = JSON.parse(localStorage.getItem('todo')) || [];

listContainer.addEventListener(
  'click',
  function (e) {
    if (e.target.tagName === 'LI') {
      doneTodo(e.target.innerText);
      e.target.classList.toggle('checked');
    }
  },
  false
);

function init() {
  listContainer.innerHTML = '';
  tasks.map((task) => {
    let li = document.createElement('li');
    li.setAttribute('class', task.done == 1 ? 'checked' : '');
    li.innerHTML = `${task.todo} <span onclick="deleteTodo(event)" class="close"></span>`;
    listContainer.appendChild(li);
  });
  lists = document.getElementsByTagName('li');
}

function addTodo() {
  if (!task.value) {
    $('.error').toast('show');
  } else if (tasks.find((todo) => todo.todo === task.value)) {
    $('.warning').toast('show');
  } else {
    $('.success').toast('show');
    tasks.push({
      todo: task.value,
      done: 0,
    });
    localStorage.setItem('todo', JSON.stringify(tasks));
    init();
  }
  task.value = '';
}

function deleteTodo(task) {
  task.preventDefault();
  task.stopPropagation();
  let todo = task.path[1].innerText;
  tasks = tasks.filter((e) => e.todo !== todo);
  localStorage.setItem('todo', JSON.stringify(tasks));
  init();
}

function doneTodo(todo) {
  tasks.forEach((task) => {
    if (task.todo === todo) {
      task.done == 1 ? (task.done = 0) : (task.done = 1);
    }
  });

  localStorage.setItem('todo', JSON.stringify(tasks));
}
init();
