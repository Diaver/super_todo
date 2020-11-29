import React from 'react';
import {Router, Route, Switch, BrowserRouter} from 'react-router-dom';
import {MainLayout} from "../features/mainLayout/MainLayout";
import history from "../services/history";
import {PageNotFound} from "../features/pageNotFound/PageNotFound";
import {Tasks} from "../features/tasks/Tasks";
import {Users} from "../features/users/Users";

export default class HistoryBrowserRouter extends BrowserRouter {
    render() {
        return (
            <Router history={history}>
                <Switch>
                    <Route exact path="/">
                        <MainLayout showSearchBar={true}>
                            <Tasks/>
                        </MainLayout>
                    </Route>

                    <Route exact path="/tasks">
                        <MainLayout showSearchBar={true}>
                            <Tasks/>
                        </MainLayout>
                    </Route>
                 
                    <Route
                        exact path="/users">
                        <MainLayout showFooter={true}>
                            <Users/>
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