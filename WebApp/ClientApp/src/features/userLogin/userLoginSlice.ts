import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {AppThunk, RootState} from '../../app/rootStore';
import {IFieldUpdateDto} from "../../models/IFieldUpdateDto";
import NotificationService from "../../services/NotificationService";
import NavigationService from "../../services/NavigationService";
import {ILoginRequest} from "../../apiModels/auth/request/ILoginRequest";
import AuthApi from "../../api/AuthApi";
import SessionService from "../../services/SessionService";
import {rootLayoutSlice} from "../rootLayout/rootLayoutSlice";

const getUserRequestInitialState: () => ILoginRequest = () => {
    return {
        email: "",
        password: "",
    };
}

interface UsersState {
    user: ILoginRequest,
    loading: boolean
}

const initialState: UsersState = {
    user: getUserRequestInitialState(),
    loading: false
};

export const userLoginSlice = createSlice({
    name: 'userLogin',
    initialState,
    reducers: {

        setLoading: (state, action: PayloadAction<boolean>) => {
            state.loading = action.payload
        },

        updateFieldValue: (state, action: PayloadAction<IFieldUpdateDto>) => {
            state.user = {
                ...state.user,
                [action.payload.field]: action.payload.value
            };
        },
        finish: (state) => {
            state.user = getUserRequestInitialState();
        },
    },
});

export const loginAsync = (): AppThunk => async (dispatch, getState) => {
    dispatch(userLoginSlice.actions.setLoading(true));
    try {
        let registerRequest = getState().userLogin.user;

        const serverRequest = await AuthApi.login(registerRequest);

        if (serverRequest.data.isSuccess) {
            dispatch(rootLayoutSlice.actions.setCurrentUser(serverRequest.data.data));
            await SessionService.setSessionGuid(serverRequest.data.data.sessionToken, serverRequest.data.data.expirationDate);
            NavigationService.go("/");
        } else {

            NotificationService.onRequestFailed(serverRequest.data, 5000)
        }
    } catch (e) {
        NotificationService.onPromiseRejected(e);
    }
    
    dispatch(userLoginSlice.actions.setLoading(false));
    dispatch(userLoginSlice.actions.finish());
};

export const userSelector = (state: RootState) => state.userLogin.user;
export const loadingSelector = (state: RootState) => state.userLogin.loading;

export default userLoginSlice.reducer;
