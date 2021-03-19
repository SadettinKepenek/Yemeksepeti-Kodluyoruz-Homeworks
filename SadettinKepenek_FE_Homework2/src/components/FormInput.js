import React from "react";

function FormInput(props) {

  const {placeholder,setText} = props;
  return (
    <div>
      <input
        type="text"
        placeholder={placeholder}
        onChange={(e) => setText(e.target.value)}
      />
    </div>
  );
}
export default FormInput;
