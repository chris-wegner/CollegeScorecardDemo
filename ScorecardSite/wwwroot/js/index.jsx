var HelloWorld = React.createClass({
  render: function () {
    return (
      <div>
        Hello, world!
      </div>
    );
  }
});
ReactDOM.render(
  <HelloWorld />,
  document.getElementById('content')
);