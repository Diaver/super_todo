import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {RootState} from '../../app/rootStore';
import {ILoginResponse} from "../../apiModels/auth/response/ILoginResponse";

interface RootLayoutState {
    isLoading: boolean;
    currentUser?: ILoginResponse;
}

const initialState: RootLayoutState = {
    isLoading: false,
    currentUser: undefined
};

export const rootLayoutSlice = createSlice({
    name: 'root',
    initialState,
    reducers: {

        loading: (state, action: PayloadAction<boolean>) => {
            state.isLoading = action.payload;
        },

        setCurrentUser: (state, action: PayloadAction<ILoginResponse>) => {
            state.currentUser = action.payload;
        },
    },
});



export const isLoadingSelector = (state: RootState) => state.rootLayout.isLoading;
export const currentUserSelector = (state: RootState) => state.rootLayout.currentUser;

export default rootLayoutSlice.reducer;
