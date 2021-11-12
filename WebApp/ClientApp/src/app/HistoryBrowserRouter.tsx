import React from 'react';
import {Router, Route, Switch, BrowserRouter} from 'react-router-dom';
import {MainLayout} from "../features/mainLayout/MainLayout";
import history from "../services/history";
import {PageNotFound} from "../features/pageNotFound/PageNotFound";
import {Tasks} from "../features/tasks/Tasks";
import {Users} from "../features/users/Users";
import {UserAdd} from "../features/userAdd/UserAdd";
import {UserEdit} from "../features/userEdit/UserEdit";
import {UserLogin} from "../features/userLogin/UserLogin";
import {Chat} from "../features/chat/Chat";
import {Signup} from "../features/signup/Signup"

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

                    <Route exact path="/users">
                        <MainLayout showFooter={true}>
                            <Users/>
                        </MainLayout>
                    </Route>

                    <Route exact path="/users/add">
                        <MainLayout showFooter={true}>
                            <UserAdd/>
                        </MainLayout>
                    </Route>

                    <Route exact path="/user/edit/:userId">
                        <MainLayout showSearchBar={true}>
                            <UserEdit/>
                        </MainLayout>
                    </Route>
                    <Route
                        exact path="/login">
                        <MainLayout showFooter={true}>
                            <UserLogin/>
                        </MainLayout>
                    </Route>
                    <Route
                        exact path="/chat">
                        <MainLayout showFooter={true}>
                            <Chat/>
                        </MainLayout>
                    </Route>
                    <Route
                        exact path="/signup">
                        <MainLayout showFooter={true}>
                            <Signup/>
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