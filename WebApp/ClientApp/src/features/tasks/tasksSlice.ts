import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from '../../app/rootStore';

interface TasksState {
  items: string[];
}

const initialState: TasksState = {
  items: [],
};

export const tasksSlice = createSlice({
  name: 'tasks',
  initialState,
  reducers: {
    addItem: (state, action: PayloadAction<string>) => {
      state.items = [...state.items, action.payload];
    },
    deleteItem: (state, action: PayloadAction<string>) => {
      state.items = state.items.filter(item => item !== action.payload);
    },
  },
});


// The function below is called a selector and allows us to select a value from
// the state. Selectors can also be defined inline where they're used instead of
// in the slice file. For example: `useSelector((state: RootState) => state.tasks.value)`
export const itemsSelector = (state: RootState) => state.tasks.items;

export default tasksSlice.reducer;
