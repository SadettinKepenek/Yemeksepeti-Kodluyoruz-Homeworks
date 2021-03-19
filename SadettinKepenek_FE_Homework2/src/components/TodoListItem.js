import Button from "./Button";
function deleteTodo(setTodos, todo) {

  setTodos((todos) =>{
    todos = todos.filter(t => t.id !== todo.id);
    return todos;
  });
}
function TodoListItem(props) {
  const { todo, setTodos } = props;
  return (
    <div class="todoListItem">
      <li>
        {todo.val}
        <Button
          text="Delete"
          className="deleteButton"
          click={() => deleteTodo(setTodos, todo)}
        />
      </li>
    </div>
  );
}
export default TodoListItem;
