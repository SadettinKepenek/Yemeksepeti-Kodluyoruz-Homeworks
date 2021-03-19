import "./App.css";
import Header from "./components/Header";
import { useState, useEffect } from "react";
import FormInput from "./components/FormInput";
import TodoList from "./components/TodoList";
import Button from "./components/Button";

function insertTodo(setTodos, inputVal, todos) {
  var todo = { val: inputVal, id: todos.length };
  if (!isTodoValid(todo, todos)) {
    return;
  }
  setTodos((todos) => [...todos, todo]);
}
function isTodoValid(todo, todos) {
  if (todo.length === 0) {
    return false;
  }
  if (todos.find((t) => t.val === todo.val)) {
    return false;
  }
  return true;
}

function setLocalStorage(todos) {
  var todosJson = JSON.stringify(todos);
  localStorage.setItem("todos", todosJson);
}
function getFromLocalStorage() {
  var todos = localStorage.getItem("todos");
  return JSON.parse(todos);
}
function App() {
  var todosFromStorage = getFromLocalStorage();
  var initialValue = todosFromStorage === null ? [] : todosFromStorage;
  const [todos, setTodos] = useState(initialValue);
  const [inputVal, setText] = useState();
  useEffect(() => setLocalStorage(todos), [todos]);
  return (
    <div class="container">
      <div class="todoApp">
        <Header text="Todo App" />
        <div class="inputGroup">
          <FormInput placeholder="Please enter todo" setText={setText} />
          <Button
            text="Add Todo"
            className="addButton"
            click={() => insertTodo(setTodos, inputVal, todos)}
          />
        </div>
        <TodoList setTodos={setTodos} todoList={todos} />
      </div>
    </div>
  );
}

export default App;
