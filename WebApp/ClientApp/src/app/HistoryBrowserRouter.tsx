import React from 'react';
import {Router, Route, Switch, BrowserRouter} from 'react-router-dom';
import {MainLayout} from "../features/mainLayout/MainLayout";
import history from "../services/history";
import {PageNotFound} from "../features/pageNotFound/PageNotFound";
import {Loader} from "./Loader";

export default class HistoryBrowserRouter extends BrowserRouter {
    render() {
        return (
            <Router history={history}>
                <Switch>
                    <Route exact path="/">
                        <MainLayout showSearchBar={true}>
                            <Loader/>
                        </MainLayout>
                    </Route>
                 
                    <Route
                        exact path="/login">
                        <MainLayout showFooter={true}>
                            <Loader/>
                        </MainLayout>
                    </Route>
                 
                    <Route>
                        <MainLayout showFooter={true}>
                            <PageNotFound/>
                        </MainLayout>
                    </Route>
                </Switch>
            </Router>
        )
    }
}