import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {AppThunk, RootState} from '../../app/rootStore';
import {IUserResponse} from "../../apiModels/response/IUserResponse";
import UsersApi from "../../api/UsersApi";
import NotificationService from "../../services/NotificationService";

interface UsersState {
    users: IUserResponse[];
    taskIndex: number;
    loading: boolean
}

const initialState: UsersState = {
    users: [],
    taskIndex: 0,
    loading: false
};

export const usersSlice = createSlice({
    name: 'users',
    initialState,
    reducers: {
        setLoading: (state, action: PayloadAction<boolean>) => {
            state.loading = action.payload
        },  
        
        usersLoaded: (state, action: PayloadAction<IUserResponse[]>) => {
            state.users = action.payload
        },

     
    },
});

export const loadUsersAsync = (): AppThunk => async (dispatch, getState) => {

    dispatch(usersSlice.actions.setLoading(true));

    try {
        let serverRequest = await UsersApi.getAll();

        if (serverRequest.data.isSuccess) {
            dispatch(usersSlice.actions.usersLoaded(serverRequest.data.data));
        } else {
            NotificationService.onRequestFailed(serverRequest.data)
        }
    } catch (e) {
        NotificationService.onPromiseRejected(e);
    }
    dispatch(usersSlice.actions.setLoading(false));
}


// The function below is called a selector and allows us to select a value from
// the state. Selectors can also be defined inline where they're used instead of
// in the slice file. For example: `useSelector((state: RootState) => state.users.value)`
export const usersSelector = (state: RootState) => state.users.users;

export default usersSlice.reducer;
