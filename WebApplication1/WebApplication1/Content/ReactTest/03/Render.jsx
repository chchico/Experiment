// 親コンポーネント
var CommentBox = React.createClass({
    loadCommentsFromServer: function () {
        $.ajax({
            url: this.props.url,
            dataType: 'json',
            cache: false,
            success: function (data) {
                this.setState({ data: data });
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(this.props.url, status, err.toString());
            }.bind(this)
        });
    },
    // 子コンポーネントから実行されるメソッド
    handleCommentSubmit: function (comment) { // … 1
        // 自身の状態を変更する
        // 良好な操作感のためにサーバに送信する前に先行して変更してしまう。
        var comments = this.state.data;
        var newComments = comments.concat([comment]);
        this.setState({ data: newComments });
        // サーバにコメント送信する
        $.ajax({
            url: this.props.url,
            dataType: 'json',
            type: 'POST',
            data: comment,
            success: function (data) {
                this.setState({ data: data });
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(this.props.url, status, err.toString());
            }.bind(this)
        });
    },
    getInitialState: function () {
        return { data: [] };
    },
    componentDidMount: function () {
        this.loadCommentsFromServer();
        setInterval(this.loadCommentsFromServer, this.props.pollInterval);
    },
    render: function () {
        return (
            <div className="commentBox">
                <h1>Comments</h1>
                <CommentList data={this.state.data} />
                // 子コンポーネントからonCommentSubmitを実行された時にhandleCommentSubmitを実行する
                <CommentForm onCommentSubmit={this.handleCommentSubmit} /> // … 2
            </div>
        );
    }
});

// tutorial2.js
var CommentList = React.createClass({
    render: function () {
        var commentNodes = this.props.data.map(function (comment) {
            return (
                <Comment author={comment.author}>
                    {comment.text}
                </Comment>
            );
        });
        return (
            <div className="commentList">
                {commentNodes}
            </div>
        );
    }
});

// 子コンポーネント
var CommentForm = React.createClass({
    // フォームをサブミットした時に実行される
    handleSubmit: function (e) { // … 3
        e.preventDefault();
        var author = ReactDOM.findDOMNode(this.refs.author).value.trim();
        var text = ReactDOM.findDOMNode(this.refs.text).value.trim();
        if (!text || !author) {
            return;
        }
        // 親コンポーネントのonCommentSubmitを実行する
        this.props.onCommentSubmit({ author: author, text: text }); // … 4
        ReactDOM.findDOMNode(this.refs.author).value = '';
        ReactDOM.findDOMNode(this.refs.text).value = '';
        return;
    },
    render: function () {
        return (
            // フォームサブミット時にhandleSubmitを実行する
            <form className="commentForm" onSubmit={this.handleSubmit}> // … 5
                <input type="text" placeholder="Your name" ref="author" />
                <input type="text" placeholder="Say something..." ref="text" />
                <input type="submit" value="Post" />
            </form>
        );
    }
});
// tutorial4.js
var Comment = React.createClass({
    render: function () {
        return (
            <div className="comment">
                <h2 className="commentAuthor">
                    {this.props.author}
                </h2>
                {this.props.children}
            </div>
        );
    }
});
ReactDOM.render(
    <CommentBox url="/Content/ReactTest/03/list.json"  pollInterval={2000} />,
    document.getElementById('content')
);