import React, {PropsWithChildren} from "react";
import {ToastContainer} from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';

interface IRootLayoutProps {
}

export function RootLayout(props: PropsWithChildren<IRootLayoutProps>) {

    return (
        <div>
            <ToastContainer/>
            {props.children}
        </div>
    );
}