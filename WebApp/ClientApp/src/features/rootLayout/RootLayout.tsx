import React, {PropsWithChildren, useEffect} from "react";
import {ToastContainer} from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
import SessionService from "../../services/SessionService";
import {useDispatch, useSelector} from "react-redux";
import {currentUserSelector, isLoadingSelector, loadUserAsync} from "./rootLayoutSlice";

interface IRootLayoutProps {
}

export function RootLayout(props: PropsWithChildren<IRootLayoutProps>) {

    const dispatch = useDispatch();
    const currentUser = useSelector(currentUserSelector);
    const isLoading = useSelector(isLoadingSelector);
    
    useEffect(() => {
        if (SessionService.isUserLoggedIn() && currentUser === undefined && isLoading === false) {
            dispatch(loadUserAsync());
        }
    }, []);

    return (
        <div>
            <ToastContainer/>
            {props.children}
        </div>
    );
}