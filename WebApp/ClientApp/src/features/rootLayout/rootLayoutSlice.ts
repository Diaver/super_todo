import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {AppThunk, RootState} from '../../app/rootStore';
import AuthApi from "../../api/AuthApi";
import NotificationService from "../../services/NotificationService";
import {userLoginSlice} from "../userLogin/userLoginSlice";
import {ICurrentUserResponse} from "../../apiModels/auth/response/ICurrentUserResponse";

interface RootLayoutState {
    isLoading: boolean;
    currentUser?: ICurrentUserResponse;
}

const initialState: RootLayoutState = {
    isLoading: false,
    currentUser: undefined
};

export const rootLayoutSlice = createSlice({
    name: 'root',
    initialState,
    reducers: {

        loading: (state, action: PayloadAction<boolean>) => {
            state.isLoading = action.payload;
        },

        setCurrentUser: (state, action: PayloadAction<ICurrentUserResponse>) => {
            state.currentUser = action.payload;
        },

        logout: (state) => {
            state.currentUser = undefined;
        },
    },
});

export const loadUserAsync = (): AppThunk => async (dispatch, getState) => {
    dispatch(userLoginSlice.actions.setLoading(true));
    try {

        const serverRequest = await AuthApi.getCurrentUser();

        if (serverRequest.data.isSuccess) {
            dispatch(rootLayoutSlice.actions.setCurrentUser(serverRequest.data.data));
        } else {
            NotificationService.onRequestFailed(serverRequest.data, 5000)
        }
    } catch (e) {
        NotificationService.onPromiseRejected(e);
    }

    dispatch(userLoginSlice.actions.setLoading(false));
    dispatch(userLoginSlice.actions.finish());
};

export const isLoadingSelector = (state: RootState) => state.rootLayout.isLoading;
export const currentUserSelector = (state: RootState) => state.rootLayout.currentUser;

export default rootLayoutSlice.reducer;
