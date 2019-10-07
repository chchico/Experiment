var CompornentFav = React.createClass({
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
    render: function () {
        return (
            <ul>
                {this.state.data.map(function (result) {
                    return (
                        <li key={result.EstateID}>
                            {result.EstateID}
                        </li>
                    );
                })}
            </ul>
        );
    }
});
ReactDOM.render(
    <CompornentFav url="/EstateFav/GetFavJson" />,
    document.getElementById('listFav')
);