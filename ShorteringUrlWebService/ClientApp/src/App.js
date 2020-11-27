import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { List } from './components/List';
import Redirect from './components/Redirect';

import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Switch>
                    <Route exact path='/' component={Home} />
                    <Route exact path='/list' component={List} />
                    <Route path='/:shortenUrlId' component={Redirect} />
                </Switch>
            </Layout>
        );
    }
}