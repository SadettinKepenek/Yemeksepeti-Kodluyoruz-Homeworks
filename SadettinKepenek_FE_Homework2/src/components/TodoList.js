import React from "react";
import TodoListItem from "./TodoListItem";
class TodoList extends React.Component {

  render() {
    const { todoList, setTodos } = this.props;
    return (
      <ul>
        {todoList.map((todo) => {
          return <TodoListItem setTodos={setTodos} todo={todo} />;
        })}
      </ul>
    );
  }
}
export default TodoList;
