import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {AppThunk, RootState} from '../../app/rootStore';
import {ITodoTaskResponse} from "../../apiModels/todoTasksApi/Response/ITodoTaskResponse";
import {ITodoTaskUserResponse} from "../../apiModels/todoTasksApi/Response/ITodoTaskUserResponse";
import NotificationService from "../../services/NotificationService";
import TodoTasksApi from "../../api/TodoTasksApi";
import {TodoTaskStatus} from "../../apiModels/common/TodoTaskStatus";

interface TasksState {
    tasks: ITodoTaskResponse[];
    users: ITodoTaskUserResponse[];
    selectedUser: ITodoTaskUserResponse;
    loading: boolean;
    text: string
}

const initialState: TasksState = {
    tasks: [],
    users: [],
    selectedUser: {userId: "", name: ""},
    loading: false,
    text: ""
};

export const tasksSlice = createSlice({
    name: 'tasks',
    initialState,
    reducers: {

        usersLoaded: (state, action: PayloadAction<ITodoTaskUserResponse[]>) => {
            state.users = action.payload
        },
        tasksLoaded: (state, action: PayloadAction<ITodoTaskResponse[]>) => {
            state.tasks = action.payload
        },
        setLoading: (state, action: PayloadAction<boolean>) => {
            state.loading = action.payload
        },
        setSelectedUser: (state, action: PayloadAction<ITodoTaskUserResponse>) => {
            state.selectedUser = action.payload
        },
        setText: (state, action: PayloadAction<string>) => {
            state.text = action.payload
        },
        addTask: (state, action: PayloadAction<ITodoTaskResponse>) => {
            state.tasks = [...state.tasks, action.payload];
        },
        deleteTask: (state, action: PayloadAction<string>) => {
            state.tasks = state.tasks.filter(task => task.todoTaskId != action.payload);
        },
        completeTask: (state, action: PayloadAction<string>) => {
            state.tasks = state.tasks.map((task) => 
                task.todoTaskId === action.payload
                    ? {...task, status: TodoTaskStatus.completed}
                    : task)
        },
        finish: (state) => {
            state.text = "";
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

export const loadTasksByUserIdAsync = (): AppThunk => async (dispatch, getState) => {

    dispatch(tasksSlice.actions.setLoading(true));

    try {
        const userId = getState().tasks.selectedUser?.userId as string;

        if (!userId) {
            return;
        }
        let serverRequest = await TodoTasksApi.getByUserId(userId);

        if (serverRequest.data.isSuccess) {
            dispatch(tasksSlice.actions.tasksLoaded(serverRequest.data.data));
        } else {
            NotificationService.onRequestFailed(serverRequest.data)
        }
    } catch (e) {
        NotificationService.onPromiseRejected(e);
    }
    dispatch(tasksSlice.actions.setLoading(false));
}

export const addAsync = (): AppThunk => async (dispatch, getState) => {
    dispatch(tasksSlice.actions.setLoading(true));
    try {
        const userId = getState().tasks.selectedUser?.userId as string;
        const text = getState().tasks.text;

        const serverRequest = await TodoTasksApi.add({
            userId,
            text
        });

        if (serverRequest.data.isSuccess) {
            dispatch(tasksSlice.actions.addTask(serverRequest.data.data));
            NotificationService.onSuccessMessage("Task added");
        } else {
            NotificationService.onRequestFailed(serverRequest.data, 5000)
        }
    } catch (e) {
        NotificationService.onPromiseRejected(e);
    }

    dispatch(tasksSlice.actions.setLoading(false));
    dispatch(tasksSlice.actions.finish());
};

export const deleteAsync = (todoTaskId: string): AppThunk => async (dispatch, getState) => {
    dispatch(tasksSlice.actions.setLoading(true));
    try {

        const serverRequest = await TodoTasksApi.delete({
            todoTaskId,
        });

        if (serverRequest.data.isSuccess) {
            dispatch(tasksSlice.actions.deleteTask(todoTaskId));
            NotificationService.onSuccessMessage("Task deleted");
        } else {
            NotificationService.onRequestFailed(serverRequest.data, 5000)
        }
    } catch (e) {
        NotificationService.onPromiseRejected(e);
    }

    dispatch(tasksSlice.actions.setLoading(false));
};

export const completeAsync = (todoTaskId: string): AppThunk => async (dispatch, getState) => {
    dispatch(tasksSlice.actions.setLoading(true));
    try {

        const serverRequest = await TodoTasksApi.complete({
            todoTaskId,
        });

        if (serverRequest.data.isSuccess) {
            dispatch(tasksSlice.actions.completeTask(todoTaskId));
            NotificationService.onSuccessMessage("Task deleted");
        } else {
            NotificationService.onRequestFailed(serverRequest.data, 5000)
        }
    } catch (e) {
        NotificationService.onPromiseRejected(e);
    }

    dispatch(tasksSlice.actions.setLoading(false));
};


export const tasksSelector = (state: RootState) => state.tasks.tasks;
export const textSelector = (state: RootState) => state.tasks.text;
export const usersSelector = (state: RootState) => state.tasks.users;
export const selectedUserSelector = (state: RootState) => state.tasks.selectedUser;

export default tasksSlice.reducer;
