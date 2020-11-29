import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {RootState} from '../../app/rootStore';
import {ITaskResponse} from "../../apiModels/response/ITaskResponse";

interface UsersState {
    items: ITaskResponse[];
    taskIndex: number;
}

const initialState: UsersState = {
    items: [],
    taskIndex: 0
};

export const usersSlice = createSlice({
    name: 'users',
    initialState,
    reducers: {
        addItem: (state, action: PayloadAction<string>) => {
            const newTask: ITaskResponse = {
                text: action.payload,
                taskId: state.taskIndex.toString(),
                userId: "userId"

            };
            state.items = [...state.items, newTask];
            state.taskIndex = state.taskIndex + 1;
        },
        deleteItem: (state, action: PayloadAction<string>) => {
            state.items = state.items.filter(item => item.taskId !== action.payload);
        },
    },
});


// The function below is called a selector and allows us to select a value from
// the state. Selectors can also be defined inline where they're used instead of
// in the slice file. For example: `useSelector((state: RootState) => state.users.value)`
export const itemsSelector = (state: RootState) => state.users.items;

export default usersSlice.reducer;
