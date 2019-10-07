var CompornentEstate = React.createClass({
    getInitialState: function () {
        return { data: [] };
    },
    loadCommentsFromServer: function () {
        $.ajax({
            url: this.props.url,
            dataType: 'json',
            cache: 'false',
            success: function (data) {
                this.setState({ data: data.data });
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
        $.ajax({
            url: "/EstateFav/ToggleFavJson/" + e.target.value,
            dataType: 'json',
            cache: 'false',
            success: function (data) {
                this.setState({ data: data.data });
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(this.props.url, status, err.toString());
            }.bind(this)
        });
    },
    render: function () {
        var _this = this;
        return (
            <ul>
                {this.state.data.map(function (result) {
                    return (
                        <li key={result.EstateID}>
                            {result.EstateID}
                            {result.Address}
                            <button onClick={_this.onClick} value={result.EstateID}>CLICK</button>
                        </li>
                    );
                })}
            </ul>
        );
    }
});
ReactDOM.render(
    <CompornentEstate url="/EstateFav/GetEstateJson" />,
    document.getElementById('listEstate')
);