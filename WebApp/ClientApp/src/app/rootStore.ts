import { configureStore, ThunkAction, Action } from '@reduxjs/toolkit';
import tasksReducer from '../features/tasks/tasksSlice';
import usersReducer from '../features/users/usersSlice';
import usersAddReducer from '../features/userAdd/usersAddSlice';

export const store = configureStore({
  reducer: {
    tasks: tasksReducer,
    users: usersReducer,
    usersAdd: usersAddReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  Action<string>
>;
