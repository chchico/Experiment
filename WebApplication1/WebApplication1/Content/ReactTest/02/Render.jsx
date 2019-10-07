
var Compornent = React.createClass({
    getInitialState: function () {
        return { data: [] };
    },
    loadCommentsFromServer: function () {
        $.ajax({
            url: this.props.url,
            dataType: 'json',
            cache: 'false',
            success: function (data) {
                this.setState({ data: data });
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(this.props.url, status, err.toString());
            }.bind(this)
        });
    },
    componentDidMount: function () {
        this.loadCommentsFromServer();
    },
    onClick: function (e) {
        console.log(e.target.value);
        var number = this.state.data.length + 1;
        var str = { 'id': (number - 1), 'str': 'list0' + number };
        this.state.data.push(str);
        this.setState({ data: this.state.data });
    },
    render: function () {
        var _this = this;
        return (
            <div>
                <ul>
                    {this.state.data.map(function (result) {
                        return (
                            <li key={result.id}>
                                {result.str}
                                <button onClick={_this.onClick} value={result.id}>CLICK</button>
                            </li>
                        );
                    })}
                </ul>
            </div>
        );
    }
});
ReactDOM.render(
    <Compornent url="/Content/ReactTest/02/list.json" />,
    document.getElementById('content')
);