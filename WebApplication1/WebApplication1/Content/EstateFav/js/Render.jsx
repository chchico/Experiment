const urlGetFavJson = '/EstateFav/GetFavJson';
const urlGetEstateJson = '/EstateFav/GetEstateJson';

var Compornent = React.createClass({
    getInitialState: function () {
        return { dataFavorite: { Title: "", Titles: [], Estates: [] } };
    },
    loadCommentsFromServer: function () {
        $.ajax({
            url: urlGetFavJson,
            data: { favoriteTitle: "" },
            dataType: 'json',
            cache: 'false',
            success: function (json) {
                this.setState({ dataFavorite: json.data });
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
            url: "/EstateFav/ToggleFavoriteEstate/",
            data: { id: val.EstateID },
            dataType: 'json',
            cache: 'false',
            success: function (json) {
                this.setState({ dataFavorite: json.data });
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(this.props.url, status, err.toString());
            }.bind(this)
        });
    },
    handleFavoriteSwitch: function (val) {
        $.ajax({
            url: urlGetFavJson,
            data: { favoriteTitle: val.FavoriteTitle },
            dataType: 'json',
            cache: 'false',
            success: function (json) {
                this.setState({ dataFavorite: json.data });
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(this.props.url, status, err.toString());
            }.bind(this)
        });
    },
    handleDeleteFavoriteTitle: function (val) {
        $.ajax({
            url: "/EstateFav/DeleteFavoriteTitle",
            data: { favoriteTitle: val.FavoriteTitle },
            dataType: 'json',
            cache: 'false',
            success: function (json) {
                this.setState({ dataFavorite: json.data });
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
                    <Estate dataFavorite={this.state.dataFavorite} onFavoriteSubmit={this.handleFavoriteSubmit} />
                </div>
                <div id="listFavorite">
                    <h1>お気に入り</h1>
                    <Favorite dataFavorite={this.state.dataFavorite} onFavoriteSubmit={this.handleFavoriteSubmit} onFavoriteSwitch={this.handleFavoriteSwitch} onDeleteFavoriteTitle={this.handleDeleteFavoriteTitle}/>
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
            url: urlGetEstateJson,
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
        this.props.onFavoriteSubmit({ EstateID: e.target.value });
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
                    var liked = _this.props.dataFavorite.Estates.map(function (f) { return f.EstateID; }).indexOf(result.EstateID) >= 0;
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
    onSubmitFavoriteSwitch: function (e) {
        e.preventDefault();
        var text = ReactDOM.findDOMNode(this.refs.favoriteTitle).value.trim();
        if (!text) {
            return;
        }
        // 親コンポーネントのonCommentSubmitを実行する
        this.props.onFavoriteSwitch({ FavoriteTitle: text });
        ReactDOM.findDOMNode(this.refs.favoriteTitle).value = '';
        return;
    },
    onClickFavoriteSubmit: function (e) {
        this.props.onFavoriteSubmit({ EstateID: e.target.value });
    },
    onClickDeleteFavoriteTitle: function (e) {
        this.props.onDeleteFavoriteTitle({ FavoriteTitle: e.target.value });
    },
    render: function () {
        var _this = this;
        return (
            <div>
                <h2>{this.props.dataFavorite.Title}
                    <button onClick={_this.onClickDeleteFavoriteTitle} value={this.props.dataFavorite.Title}>×</button></h2>
                <ul>
                    {this.props.dataFavorite.Estates.map(function (result) {
                        return (
                            <li key={result.EstateID}>
                                {result.EstateID}
                                {result.Address}
                                <button onClick={_this.onClickFavoriteSubmit} value={result.EstateID} className="favoriteOn">★</button>
                            </li>
                        );
                    })}
                </ul>
                <form onSubmit={this.onSubmitFavoriteSwitch}>
                    <input type="text" list="keywords" ref="favoriteTitle" placeholder="フォルダ選択" />

                    <datalist id="keywords">
                        {this.props.dataFavorite.Titles.map(function (result) {
                            return (
                                <option key={result} value={result} />
                            );
                        })}
                    </datalist>
                    <button type="submit">〇</button>
                </form>
            </div>
        );
    }
});

ReactDOM.render(
    <Compornent />,
    document.getElementById('content')
);