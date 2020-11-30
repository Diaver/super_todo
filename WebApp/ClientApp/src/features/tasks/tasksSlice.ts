import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {AppThunk, RootState} from '../../app/rootStore';
import {ITodoTaskResponse} from "../../apiModels/todoTasksApi/Response/ITodoTaskResponse";
import {ITodoTaskUserResponse} from "../../apiModels/todoTasksApi/Response/ITodoTaskUserResponse";
import NotificationService from "../../services/NotificationService";
import TodoTasksApi from "../../api/TodoTasksApi";

interface TasksState {
    items: ITodoTaskResponse[];
    users: ITodoTaskUserResponse[];
    loading: boolean;
}

const initialState: TasksState = {
    items: [],
    users: [],
    loading: false
};

export const tasksSlice = createSlice({
    name: 'tasks',
    initialState,
    reducers: {

        usersLoaded: (state, action: PayloadAction<ITodoTaskUserResponse[]>) => {
            state.users = action.payload
        },
        setLoading: (state, action: PayloadAction<boolean>) => {
            state.loading = action.payload
        },
    },
});


export const loadUsersAsync = (): AppThunk => async (dispatch, getState) => {

    dispatch(tasksSlice.actions.setLoading(true));

    try {
        let serverRequest = await TodoTasksApi.getAllUsers();

        if (serverRequest.data.isSuccess) {
            dispatch(tasksSlice.actions.usersLoaded(serverRequest.data.data));
        } else {
            NotificationService.onRequestFailed(serverRequest.data)
        }
    } catch (e) {
        NotificationService.onPromiseRejected(e);
    }
    dispatch(tasksSlice.actions.setLoading(false));
}


// The function below is called a selector and allows us to select a value from
// the state. Selectors can also be defined inline where they're used instead of
// in the slice file. For example: `useSelector((state: RootState) => state.tasks.value)`
export const itemsSelector = (state: RootState) => state.tasks.items;
export const usersSelector = (state: RootState) => state.tasks.users;

export default tasksSlice.reducer;
