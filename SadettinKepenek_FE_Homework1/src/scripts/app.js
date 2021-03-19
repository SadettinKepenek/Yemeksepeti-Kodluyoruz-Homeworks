let todoList = [];

window.onload = () => {
  var todos = JSON.parse(localStorage.getItem("todos"));
  todos.map((todo) => {
    todoList.push({ todo: todo.todo, id: todo.id });
    createElement(todo.todo, todo.id);
  });
};

function addTodo() {
  var todoInput = document.getElementById("todoInput");
  var todoVal = todoInput.value;
  var todoListId = todoList.length;
  todoList.push({ todo: todoVal, id: todoListId });
  createElement(todoVal, todoListId);
}

function createElement(todo, id) {
  var todoListElement = document.getElementById("todoList");

  var liElement = document.createElement("li");
  liElement.id = "li_" + id;
  liElement.innerText = todo;

  var buttonElement = document.createElement("button");
  buttonElement.innerText = "Delete";
  buttonElement.onclick = () => deleteTodo(id);
  buttonElement.className = "deleteButton";
  liElement.appendChild(buttonElement);
  todoListElement.appendChild(liElement);
  localStorage.setItem("todos", JSON.stringify(todoList));
}

function deleteTodo(id) {
  var liElementId = "li_" + id;
  var liElement = document.getElementById(liElementId);
  liElement.remove();
  todoList = todoList.filter((t) => t.id !== id);
  localStorage.setItem("todos", JSON.stringify(todoList));
}
