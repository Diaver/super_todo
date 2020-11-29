import React, {Suspense} from 'react';
import ReactDOM from 'react-dom';
import { store } from './app/rootStore';
import { Provider } from 'react-redux';
import {CssBaseline, createMuiTheme, MuiThemeProvider} from "@material-ui/core";
import {Loader} from "./app/Loader";
import HistoryBrowserRouter from "./app/HistoryBrowserRouter";
import {ErrorBoundary, FallbackProps} from 'react-error-boundary'
import {RootLayout} from "./features/rootLayout/RootLayout";
const theme = createMuiTheme();

function ErrorFallback(props: FallbackProps) {
    return (
        <div role="alert">
            <h2>Oops.. something went wrong and page crash :(</h2>
            <pre>Error: {props.error && props.error.message}</pre>
            <h6><a href="/">Go to Home page</a></h6>
        </div>
    )
}

ReactDOM.render(
    <ErrorBoundary FallbackComponent={ErrorFallback}>
        <Suspense fallback={<Loader/>}>
            <MuiThemeProvider theme={theme}>
                <CssBaseline/>
                <Provider store={store}>
                    <RootLayout>
                        <HistoryBrowserRouter/>
                    </RootLayout>
                </Provider>
            </MuiThemeProvider>
        </Suspense>
    </ErrorBoundary>,
    document.getElementById('root')
);