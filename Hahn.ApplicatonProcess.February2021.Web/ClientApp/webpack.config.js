const { AureliaPlugin } = require('aurelia-webpack-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const { resolve } = require('path');

const CopyPlugin = require("copy-webpack-plugin");

module.exports = function (mode) {
    return {
        mode: mode || 'development',
        resolve: {
            extensions: ['.ts', '.js'],
            modules: [
                resolve(__dirname, 'src'),
                resolve(__dirname, 'node_modules')
            ]
        },
        entry: {
            app: ['aurelia-bootstrapper']
        },
        output: {
            filename: '[name].js',
            path: resolve(__dirname, '../wwwroot')
        },
        watch: mode === 'development',
        devtool: mode === 'development' ? 'inline-source-map' : 'source-map',
        devServer: {
            contentBase: '../wwwroot'
        },
        module: {
            rules: [
                { test: /\.html$/i, loader: 'html-loader' },
                { test: /\.ts$/i, exclude: /node_modules/, loader: 'ts-loader' },
                { test: /\.css$/i,  use: ['style-loader'], issuer: { test: /\.[tj]s$/i, }, },
                { test: /\.css$/i, use: ['css-loader'], },
                { test: /\.(svg|eot|woff|woff2|ttf)$/, use: ['file-loader'] }
            ]
        },
        plugins: [
            new AureliaPlugin(),
            new HtmlWebpackPlugin({
                template: 'index.ejs',
                metadata: { dev: mode !== 'production' }
            }),
            new CopyPlugin({
                patterns: [
                    { from: 'src/locales/', to: 'locales/' }
                ]
            })
        ]
    };
};