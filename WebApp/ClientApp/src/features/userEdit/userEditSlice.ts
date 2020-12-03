import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {AppThunk, RootState} from '../../app/rootStore';
import {IFieldUpdateDto} from "../../models/IFieldUpdateDto";
import UsersApi from "../../api/UsersApi";
import NotificationService from "../../services/NotificationService";
import NavigationService from "../../services/NavigationService";
import {IUserResponse} from "../../apiModels/usersApi/Response/IUserResponse";

const getUserRequestInitialState: () => IUserResponse = () => {
    return {
        userId: "",
        name: "",
        email: "",
        dateOfBirth: "",
    };
}

interface UsersState {
    user: IUserResponse,
    loading: boolean
}

const initialState: UsersState = {
    user: getUserRequestInitialState(),
    loading: false
};

export const userEditSlice = createSlice({
    name: 'userEdit',
    initialState,
    reducers: {

        setLoading: (state, action: PayloadAction<boolean>) => {
            state.loading = action.payload
        },

        userLoaded: (state, action: PayloadAction<IUserResponse>) => {
            state.user = action.payload
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

export const loadUserAsync = (userId: string): AppThunk => async (dispatch, getState) => {

    dispatch(userEditSlice.actions.setLoading(true));

    try {
        let serverRequest = await UsersApi.getById(userId);

        if (serverRequest.data.isSuccess) {
            dispatch(userEditSlice.actions.userLoaded(serverRequest.data.data));
        } else {
            NotificationService.onRequestFailed(serverRequest.data)
        }
    } catch (e) {
        NotificationService.onPromiseRejected(e);
    }
    dispatch(userEditSlice.actions.setLoading(false));
}

export const saveAsync = (): AppThunk => async (dispatch, getState) => {

    dispatch(userEditSlice.actions.setLoading(true));
    try {
        const user = getState().userEdit.user;
        const serverRequest = await UsersApi.update(user);

        if (serverRequest.data.isSuccess) {
            NotificationService.onSuccessMessage("Changes saved");
            NavigationService.go("/users");
        } else {

            NotificationService.onRequestFailed(serverRequest.data, 5000)
        }
    } catch (e) {
        NotificationService.onPromiseRejected(e);
    }

    dispatch(userEditSlice.actions.setLoading(false));
    dispatch(userEditSlice.actions.finish());
};

export const deleteAsync = (): AppThunk => async (dispatch, getState) => {

    dispatch(userEditSlice.actions.setLoading(true));
    try {

        const userId = getState().userEdit.user?.userId as string;

        const serverRequest = await UsersApi.delete({userId});

        if (serverRequest.data.isSuccess) {
            NotificationService.onSuccessMessage("User deleted");
            NavigationService.go("/users");
        } else {

            NotificationService.onRequestFailed(serverRequest.data, 5000)
        }
    } catch (e) {
        NotificationService.onPromiseRejected(e);
    }

    dispatch(userEditSlice.actions.setLoading(false));
    dispatch(userEditSlice.actions.finish());
};


export const userSelector = (state: RootState) => state.userEdit.user;
export const loadingSelector = (state: RootState) => state.userEdit.loading;

export default userEditSlice.reducer;
