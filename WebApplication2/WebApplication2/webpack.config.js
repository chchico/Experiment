/// <binding BeforeBuild='Run - Development' />
const webpack = require('webpack')
const VueLoaderPlugin = require('vue-loader/lib/plugin')
const MiniCssExtractPlugin = require('mini-css-extract-plugin') // css を別ファイルにする

// webpack v4

module.exports = {
  // モード値を production に設定すると最適化された状態で、
  // development に設定するとソースマップ有効でJSファイルが出力される
  mode: 'development',
  // メインとなるJavaScriptファイル（エントリーポイント）
  entry: './ClientApp/boot.js',
  // 元となるソースにソースマップが埋め込まれる
  devtool: 'inline-source-map', // source-map だとソースマップファイル(.map)を出力する
  // ファイルの出力設定
  output: {
    // 出力ファイルのディレクトリ名
    path: `${__dirname}/Scripts/app`,
    // 出力ファイル名
    filename: 'main.js'
  },
  // これを入れないと正しくコンパイルされない
  resolve: {
    alias: {
      vue$: 'vue/dist/vue.esm.js' // 'vue/dist/vue.common.js' webpack 1 用
    }
  },
  optimization: {
    splitChunks: {
      // cacheGroups内にバンドルの設定を複数記述できる
      cacheGroups: {
        // 今回はvendorだが、任意の名前で問題ない
        vendor: {
          // node_modules配下のモジュールをバンドル対象とする
          test: /node_modules/,
          name: 'vendor',
          chunks: 'initial',
          enforce: true
        }
      }
    }
  },
  module: {
    rules: [
      // require 等で参照されたファイルの拡張子が.vue だった場合は、vue-loader に処理を渡す
      {
        test: /\.(vue|vue.html)$/,
        loader: 'vue-loader'
      },
      // this will apply to both plain `.js` files AND `<script>` blocks in `.vue` files
      {
        test: /\.js$/,
        loader: 'babel-loader'
      },
      // this will apply to both plain `.css` files AND `<style>` blocks in `.vue` files
      {
        test: /\.css$/,
        use: [
          // 'vue-style-loader', // 次にスタイルシートをJSからlinkタグに展開する機能
          MiniCssExtractPlugin.loader,
          'css-loader' // 最初にCSSをバンドルするための機能が呼び出される
        ]
      },
      // 画像参照をバイナリとして埋め込む
      {
        test: /\.(jpg|png)$/,
        loader: 'url-loader'
      }
    ]
  },
  plugins: [
    // make sure to include the plugin!
    new webpack.ProvidePlugin({
      $: 'jquery'
    }),
    new VueLoaderPlugin(),
    new MiniCssExtractPlugin({
      filename: '[name].css'
    })
  ]
}
