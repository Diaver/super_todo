import { configureStore, ThunkAction, Action } from '@reduxjs/toolkit';
import tasksReducer from '../features/tasks/tasksSlice';
import usersReducer from '../features/users/usersSlice';
import usersAddReducer from '../features/userAdd/usersAddSlice';
import userEditReducer from '../features/userEdit/userEditSlice';
import userLoginReducer from '../features/userLogin/userLoginSlice';
import rootLayoutReducer from "../features/rootLayout/rootLayoutSlice";
import chatReducer from "../features/chat/chatSlice";

export const store = configureStore({
  reducer: {
    tasks: tasksReducer,
    users: usersReducer,
    usersAdd: usersAddReducer,
    userEdit: userEditReducer,
    userLogin: userLoginReducer,
    rootLayout: rootLayoutReducer,
    chat: chatReducer
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  Action<string>
>;
