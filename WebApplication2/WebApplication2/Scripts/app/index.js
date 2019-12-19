// フィルターを追加。レンダリング時に見た目を変更するのに使用する
Vue.filter('typeFormatter',
    function (val) {
        var typeCds = [{ id: '1', name: '新品' }, { id: '2', name: '中古' }];
        var newVal = '';

        typeCds.forEach(function (el) {
            if (val.toString() === el.id) {
                newVal = el.name;
            }
        });

        return newVal;
    });

// コンポーネントを追加
Vue.component('my-component', {
    props: ['message'],
    template: '#my-component'
});

// 機能の再利用
var countMixin = {
    data: {
        count: 0
    },
    methods: {
        addCount: function () {
            this.count++;
        },
        delCount: function () {
            this.count--;
        }
    }
};

var Milk = new Vue({
    // 再利用したいコンポーネントを指定
    mixins: [countMixin],
    data: {
        items: []
    },
    methods: {
        getData: function () {
            // ログイン直後は持ってないので取りに行く必要がある
            var token = sessionStorage.getItem('accessToken');

            var options = {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            };

            axios.get('/api/Items/', options)
                .then(response => {
                    this.items = response.data;
                });
        }
    }
});

var Foo = {
    template: '<div>foo</div>'
};

var Bar = {
    template: `
<div>bar</div>`
};

var routes = [
    { path: '/foo', component: Foo },
    { path: '/bar', component: Bar }
];

var router = new VueRouter({
    routes // `routes: routes` の短縮表記
});

var vm = new Vue({
    el: '#app',
    router: router,
    // 再利用したいコンポーネントを指定
    mixins: [countMixin],
    beforeCreate() {
        if (sessionStorage.getItem('accessToken')) {
            return;
        }

        if (window.location.hash.indexOf('access_token') === 2) {
            var queryString = window.location.hash.substr(1);

            var param = queryString.substring(1).split('&').map((p) => p.split('='))
                .reduce((obj, e) => ({ ...obj, [e[0]]: e[1] }), {});

            sessionStorage.setItem('accessToken', param['access_token']);
        } else {
            // アクセストークンを取りに行く
            window.location = '/Account/Authorize?client_id=web&response_type=token&state=' +
                encodeURIComponent(window.location.hash);
        }
    },
    data: {
        items: []
    },
    methods: {
        getData: function () {
            // ログイン直後は持ってないので取りに行く必要がある
            var token = sessionStorage.getItem('accessToken');

            var options = {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            };

            axios.get('/api/Items/', options)
                .then(response => {
                    this.items = response.data;
                });
        }
    }
});

/*
 // この書き方はダメ！this が違うものを指している
axios.get('/api/Items/', options)
    .then(function (response) {
        this.items = response.data;
    });*/
