var Compornent = React.createClass({
    getInitialState: function () {
        return { dataFavorite: [] };
    },
    loadCommentsFromServer: function () {
        $.ajax({
            url: "/EstateFav/GetFavJson",
            dataType: 'json',
            cache: 'false',
            success: function (data) {
                this.setState({ dataFavorite: data.data });
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(this.props.url, status, err.toString());
            }.bind(this)
        });
    },
    componentDidMount: function () {
        this.loadCommentsFromServer();
    },
    handleFavoriteSubmit: function (val) {
        $.ajax({
            url: "/EstateFav/ToggleFavJson/" + val.estateID,
            dataType: 'json',
            cache: 'false',
            success: function (data) {
                this.setState({ dataFavorite: data.data });
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(this.props.url, status, err.toString());
            }.bind(this)
        });
    },
    render: function () {
        return (
            <div id="wrapContent">
                <div id="listEstate">
                    <h1>物件一覧</h1>
                    <Estate urlEstate="/EstateFav/GetEstateJson" dataFavorite={this.state.dataFavorite} onFavoriteSubmit={this.handleFavoriteSubmit} />
                </div>
                <div id="listFavorite">
                    <h1>お気に入り</h1>
                    <Favorite dataFavorite={this.state.dataFavorite} onFavoriteSubmit={this.handleFavoriteSubmit} />
                </div>
            </div>
        );
    }

});

var Estate = React.createClass({
    getInitialState: function () {
        return { dataEstate: [] };
    },
    loadCommentsFromServer: function () {
        $.ajax({
            url: this.props.urlEstate,
            dataType: 'json',
            cache: 'false',
            success: function (data) {
                this.setState({ dataEstate: data.data });
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
        this.props.onFavoriteSubmit({ estateID: e.target.value });
    },
    render: function () {
        var _this = this;
        return (
            <ul>
                <li className="wrapEstateItem">
                    <span className="item itemID">物件ID</span>
                    <span className="item itemType">種別</span>
                    <span className="item itemPrice">価格</span>
                    <span className="item itemAddress">住所</span>
                    <span className="item itemLand">土地面積</span>
                    <span className="item itemFloor">建物面積</span>
                    <span className="item itemFav"></span>
                </li>
                {this.state.dataEstate.map(function (result) {
                    var liked = _this.props.dataFavorite.map(function (f) { return f.EstateID; }).indexOf(result.EstateID) >= 0;
                    return (
                        <li key={result.EstateID} className="wrapEstateItem">
                            <span className="item itemID">{result.EstateID}</span>
                            <span className="item itemType">{result.Type}</span>
                            <span className="item itemPrice">{result.Price}</span>
                            <span className="item itemAddress">{result.Address}</span>
                            <span className="item itemLand">{result.Land}</span>
                            <span className="item itemFloor">{result.Floor}</span>
                            <span className="item itemFav">
                                <button onClick={_this.onClick} value={result.EstateID} className={liked ? 'favoriteOn' : 'favoriteOff'}>{liked ? '★' : '☆'}</button>
                            </span>
                        </li>
                    );
                })}
            </ul>
        );
    }
});

var Favorite = React.createClass({
    onClick: function (e) {
        this.props.onFavoriteSubmit({ estateID: e.target.value });
    },
    render: function () {
        var _this = this;
        return (
            <ul>
                {this.props.dataFavorite.map(function (result) {
                    return (
                        <li key={result.EstateID}>
                            {result.EstateID}
                            {result.Address}
                            <button onClick={_this.onClick} value={result.EstateID} className="favoriteOn">★</button>
                        </li>
                    );
                })}
            </ul>
        );
    }
});

ReactDOM.render(
    <Compornent />,
    document.getElementById('content')
);