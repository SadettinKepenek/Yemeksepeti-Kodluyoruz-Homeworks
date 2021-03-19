
export default function Button(props) {
    const {text,click,className} = props;
  return <button class={className} onClick={click}>{text}</button>

}
