import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {AppThunk, RootState} from '../../app/rootStore';
import {IFieldUpdateDto} from "../../models/IFieldUpdateDto";
import UsersApi from "../../api/UsersApi";
import NotificationService from "../../services/NotificationService";
import NavigationService from "../../services/NavigationService";
import {IUserCreateRequest} from "../../apiModels/usersApi/Request/IUserCreateRequest";

const getUserRequestInitialState: () => IUserCreateRequest = () => {
    return {
        name: "",
        email: "",
        dateOfBirth: "",
    };
}

interface UsersState {
    user: IUserCreateRequest,
    loading: boolean
}

const initialState: UsersState = {
    user: getUserRequestInitialState(),
    loading: false
};

export const usersAddSlice = createSlice({
    name: 'usersAdd',
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

export const addAsync = (): AppThunk => async (dispatch, getState) => {

    dispatch(usersAddSlice.actions.setLoading(true));
    try {
        let registerRequest = getState().usersAdd.user;

        const serverRequest = await UsersApi.add(registerRequest);

        if (serverRequest.data.isSuccess) {
            NotificationService.onSuccessMessage("User added");
            NavigationService.go("/users");
        } else {

            NotificationService.onRequestFailed(serverRequest.data, 5000)
        }
    } catch (e) {
        NotificationService.onPromiseRejected(e);
    }
    
    dispatch(usersAddSlice.actions.setLoading(false));
    dispatch(usersAddSlice.actions.finish());
};


export const userSelector = (state: RootState) => state.usersAdd.user;
export const loadingSelector = (state: RootState) => state.usersAdd.loading;

export default usersAddSlice.reducer;
